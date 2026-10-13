namespace ShopClock.Core;

public enum ShiftProblem
{
    None,        // a normal shift, or the current open shift
    MissingOut,  // clocked in, then clocked in again with no clock-out between
    MissingIn    // clocked out with no clock-in before it
}

/// <summary>
/// One worked shift built from punches. In or Out can be null when a punch is missing.
/// </summary>
public record Shift(
    DateTimeOffset? In,
    DateTimeOffset? Out,
    ShiftProblem Problem,
    bool HasManualPunch,
    string? Notes,
    Guid? InPunchId,
    Guid? OutPunchId)
{
    /// <summary>The shift belongs to the local date you clocked in (or out, if the In is missing).</summary>
    public DateOnly Date => DateOnly.FromDateTime((In ?? Out)!.Value.DateTime);

    /// <summary>Currently clocked in: has an In, no Out yet, and nothing wrong with it.</summary>
    public bool IsOpen => In is not null && Out is null && Problem == ShiftProblem.None;

    /// <summary>Exact time worked, or null if either end is missing.</summary>
    public TimeSpan? Duration => In is { } start && Out is { } end ? end - start : null;

    internal static Shift From(Punch? inPunch, Punch? outPunch, ShiftProblem problem)
    {
        var notes = new[] { inPunch?.Note, outPunch?.Note }
            .Where(n => !string.IsNullOrEmpty(n));
        var joined = string.Join("; ", notes);

        return new Shift(
            inPunch?.Timestamp,
            outPunch?.Timestamp,
            problem,
            inPunch?.Source == PunchSource.Manual || outPunch?.Source == PunchSource.Manual,
            joined.Length == 0 ? null : joined,
            inPunch?.Id,
            outPunch?.Id);
    }
}