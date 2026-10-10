using ShopClock.Core;
using Xunit;

namespace ShopClock.Tests;

public class ShiftBuilderTests
{
    // Short helper: P(PunchType.In, 5, 7, 30) = clock-in Oct 5 at 7:30 AM Central.
    private static Punch P(PunchType type, int day, int hour, int minute,
        PunchSource source = PunchSource.Clock, string? note = null, Guid? voids = null) =>
        new(Guid.NewGuid(),
            new DateTimeOffset(2026, 10, day, hour, minute, 0, TimeSpan.FromHours(-5)),
            type, source, note, voids);

    [Fact]
    public void In_then_Out_makes_one_shift()
    {
        var shifts = ShiftBuilder.Build([P(PunchType.In, 5, 7, 30), P(PunchType.Out, 5, 17, 0)]);

        var shift = Assert.Single(shifts);
        Assert.Equal(ShiftProblem.None, shift.Problem);
        Assert.Equal(TimeSpan.FromHours(9.5), shift.Duration);
        Assert.False(shift.IsOpen);
    }

    [Fact]
    public void In_with_no_Out_yet_is_open()
    {
        var shift = Assert.Single(ShiftBuilder.Build([P(PunchType.In, 5, 7, 30)]));

        Assert.True(shift.IsOpen);
        Assert.Null(shift.Out);
        Assert.Null(shift.Duration);
        Assert.Equal(ShiftProblem.None, shift.Problem);
    }

    [Fact]
    public void Two_Ins_in_a_row_flags_the_first_as_missing_its_Out()
    {
        var shifts = ShiftBuilder.Build([
            P(PunchType.In, 5, 7, 30),   // forgot to clock out Friday
            P(PunchType.In, 8, 7, 30),
            P(PunchType.Out, 8, 17, 0),
        ]);

        Assert.Equal(2, shifts.Count);
        Assert.Equal(ShiftProblem.MissingOut, shifts[0].Problem);
        Assert.Null(shifts[0].Out);
        Assert.False(shifts[0].IsOpen);   // a problem, not "currently clocked in"
        Assert.Equal(ShiftProblem.None, shifts[1].Problem);
    }

    [Fact]
    public void Out_with_no_In_is_flagged()
    {
        var shift = Assert.Single(ShiftBuilder.Build([P(PunchType.Out, 5, 17, 0)]));

        Assert.Equal(ShiftProblem.MissingIn, shift.Problem);
        Assert.Null(shift.In);
        Assert.Equal(new DateOnly(2026, 10, 5), shift.Date);
    }

    [Fact]
    public void Shift_belongs_to_clock_in_date()
    {
        var shift = Assert.Single(ShiftBuilder.Build([
            P(PunchType.In, 5, 22, 0),
            P(PunchType.Out, 6, 2, 0),
        ]));

        Assert.Equal(new DateOnly(2026, 10, 5), shift.Date);
        Assert.Equal(TimeSpan.FromHours(4), shift.Duration);
    }

    [Fact]
    public void Voided_punch_is_ignored()
    {
        var wrongIn = P(PunchType.In, 5, 6, 0);
        var shifts = ShiftBuilder.Build([
            wrongIn,
            P(PunchType.In, 5, 7, 30),
            P(PunchType.Out, 5, 17, 0),
            P(PunchType.In, 5, 17, 5, PunchSource.Manual, "Punched in by mistake", wrongIn.Id),
        ]);

        var shift = Assert.Single(shifts);
        Assert.Equal(ShiftProblem.None, shift.Problem);
        Assert.Equal(TimeSpan.FromHours(9.5), shift.Duration);
    }

    [Fact]
    public void Punches_are_sorted_before_pairing()
    {
        // A missed clock-in added later sits at the end of the file.
        var shift = Assert.Single(ShiftBuilder.Build([
            P(PunchType.Out, 5, 17, 0),
            P(PunchType.In, 5, 7, 30, PunchSource.Manual, "Forgot to punch in"),
        ]));

        Assert.Equal(ShiftProblem.None, shift.Problem);
        Assert.True(shift.HasManualPunch);
        Assert.Equal("Forgot to punch in", shift.Notes);
    }

    [Fact]
    public void No_punches_means_no_shifts()
    {
        Assert.Empty(ShiftBuilder.Build([]));
    }
}