using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ShopClock.Core
{
    public class PunchStore(string filePath)
    {
        private readonly string _filePath = filePath;
        private const string Header = "Id,Timestamp,Type,Source,Note,Voids";

        public void Append(Punch punch)
        {
            var data = $"{punch.Id},{punch.Timestamp:O},{punch.Type},{punch.Source},{Escape(punch.Note)},{punch.Voids}";
            var folder = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(folder))
                Directory.CreateDirectory(folder);

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, Header + Environment.NewLine);

            File.AppendAllText(_filePath, data + Environment.NewLine);
        }

        public IReadOnlyList<Punch> ReadAll()
        {
            var punches = new List<Punch>();

            if (!File.Exists(_filePath))
                return punches;

            var lines = new List<string>();
            using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line);
            }

            for (int i = 1; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var parts = ParseLine(lines[i]);
                if (parts.Length != 6)
                    throw new FormatException(
                        $"punches.csv line {i + 1} has {parts.Length} columns, expected 6: {lines[i]}");

                var id = Guid.Parse(parts[0]);
                var timestamp = DateTimeOffset.Parse(parts[1], CultureInfo.InvariantCulture);
                var type = Enum.Parse<PunchType>(parts[2]);
                var source = Enum.Parse<PunchSource>(parts[3]);
                string? note = string.IsNullOrEmpty(parts[4]) ? null : parts[4];
                Guid? voids = string.IsNullOrEmpty(parts[5]) ? null : Guid.Parse(parts[5]);

                punches.Add(new Punch(id, timestamp, type, source, note, voids));
            }

            return punches;
        }
        private static string Escape(string? field) =>
            "\"" + (field ?? "").Replace("\"", "\"\"") + "\"";

        private static string[] ParseLine(string line)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    // end of field
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }

            }

            fields.Add(current.ToString());
            return fields.ToArray();
        }   
    }
}
