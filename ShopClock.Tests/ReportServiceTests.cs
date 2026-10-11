using ShopClock.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopClock.Tests
{

    public class ReportServiceTests
    {
        // P(PunchType.In, 5, 7, 42) = Oct 5, 2026 at 7:42 AM Central
        private static Punch P(PunchType type, int day, int hour, int minute) =>
            new(Guid.NewGuid(),
                new DateTimeOffset(2026, 10, day, hour, minute, 0, TimeSpan.FromHours(-5)),
                type, PunchSource.Clock);

        private static readonly DateOnly Oct5 = new(2026, 10, 5);
        private static readonly DateOnly Oct11 = new(2026, 10, 11);

        [Fact]
        public void Known_week_totals_exact_minutes_then_rounds_once()
        {
            // Arrange
            // Mon  7:42 -> 17:12 = 9h 30m = 570 min
            // Tue  8:05 -> 16:58 = 8h 53m = 533 min
            // Wed  7:30 -> 12:17 = 4h 47m = 287 min
            //                      total = 1390 min = 23.1667 h -> 23.17
            // (Rounding each shift first gives 9.50 + 8.88 + 4.78 = 23.16, one cent of an hour short.)
            var punches = new List<Punch>
            {
                P(PunchType.In, 5, 7, 42),  P(PunchType.Out, 5, 17, 12),
                P(PunchType.In, 6, 8, 5),   P(PunchType.Out, 6, 16, 58),
                P(PunchType.In, 7, 7, 30),  P(PunchType.Out, 7, 12, 17),
            };

            // Act
            var report = ReportService.BuildReport(punches, Oct5, Oct11);

            // Assert
            Assert.Equal(3, report.Rows.Count);
            Assert.Equal(23.17, report.TotalHours, precision: 2);
        }

        [Fact]
        public void Range_includes_shifts_on_both_end_dates()
        {
            var punches = new List<Punch>
            {
                P(PunchType.In, 5, 8, 0),   P(PunchType.Out, 5, 12, 0),    // on 'from'
                P(PunchType.In, 11, 8, 0),  P(PunchType.Out, 11, 10, 0),   // on 'to'
            };

            var report = ReportService.BuildReport(punches, Oct5, Oct11);

            Assert.Equal(2, report.Rows.Count);
            Assert.Equal(Oct5, report.Rows[0].Date);
            Assert.Equal(Oct11, report.Rows[1].Date);
            Assert.Equal(6, report.TotalHours, precision: 2);
        }

        [Fact]
        public void Shift_before_range_is_excluded()
        {
            var punches = new List<Punch>
            {
                P(PunchType.In, 4, 8, 0),   P(PunchType.Out, 4, 16, 0),    // day before 'from'
                P(PunchType.In, 5, 8, 0),   P(PunchType.Out, 5, 12, 0),
            };

            var report = ReportService.BuildReport(punches, Oct5, Oct11);

            var row = Assert.Single(report.Rows);
            Assert.Equal(Oct5, row.Date);
            Assert.Equal(4, report.TotalHours, precision: 2);
        }

        [Fact]
        public void Open_shift_shows_as_row_but_is_not_in_total()
        {
            var punches = new List<Punch>
            {
                P(PunchType.In, 5, 8, 0),   P(PunchType.Out, 5, 16, 0),    // 8 hours
                P(PunchType.In, 6, 8, 0),                                  // still clocked in
            };

            var report = ReportService.BuildReport(punches, Oct5, Oct11);

            Assert.Equal(2, report.Rows.Count);
            Assert.Null(report.Rows[1].Hours);
            Assert.Null(report.Rows[1].Out);
            Assert.Equal(8, report.TotalHours, precision: 2);
        }

        [Fact]
        public void Range_with_no_shifts_returns_empty_report()
        {
            var punches = new List<Punch>
            {
                P(PunchType.In, 5, 7, 42),  P(PunchType.Out, 5, 17, 12),
            };

            var report = ReportService.BuildReport(punches,
                new DateOnly(2026, 11, 2), new DateOnly(2026, 11, 8));

            Assert.Empty(report.Rows);
            Assert.Equal(0, report.TotalHours);
        }
    }
}