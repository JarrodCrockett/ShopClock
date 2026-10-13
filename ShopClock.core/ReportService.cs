using System;
using System.Collections.Generic;
using System.Text;

namespace ShopClock.Core
{
    public record ReportRow(DateOnly Date, DateTimeOffset? In, DateTimeOffset? Out,
                          double? Hours, string? Notes, ShiftProblem Problem, bool HasManualPunch, Guid? InPunchId, Guid? OutPunchId);

    public record Report(IReadOnlyList<ReportRow> Rows, double TotalHours);

    public static class ReportService
    {
        public static Report BuildReport(IEnumerable<Punch> punches, DateOnly from, DateOnly to)
        {
            var shifts = ShiftBuilder.Build(punches);
            var rows = shifts
                .Where(s => s.Date >= from && s.Date <= to)
                .Select(s => new ReportRow(s.Date, s.In, s.Out, s.Duration?.TotalHours, s.Notes, s.Problem, s.HasManualPunch, s.InPunchId, s.OutPunchId))
                .ToList();
            var total = rows.Sum(r => r.Hours) ?? 0;
            return new Report(rows, Math.Round(total, 2));
        }
    }
}
