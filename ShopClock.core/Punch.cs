namespace ShopClock.Core
{
    public enum PunchType { In, Out }
    public enum PunchSource { Clock, Manual }
    public record Punch(Guid Id, DateTimeOffset Timestamp, PunchType Type, PunchSource Source, string? Note = null, Guid? Voids = null)
    {
        public static Punch Now(PunchType type) => new(Guid.NewGuid(), DateTimeOffset.Now, type, PunchSource.Clock);
    }
}
