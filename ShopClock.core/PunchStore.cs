using System;
using System.Collections.Generic;
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
            var data = $"{punch.Id},{punch.Timestamp:O},{punch.Type},{punch.Source},{punch.Note},{punch.Voids}";
            var folder = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(folder))
                Directory.CreateDirectory(folder);

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, Header + Environment.NewLine);

            File.AppendAllText(_filePath, data + Environment.NewLine);
            // Implement logic to save the punch to a file or database
        }
    }
}
