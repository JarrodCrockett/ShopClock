using ShopClock.Core;
using Xunit;

namespace ShopClock.Tests;

public class PunchStoreTests : IDisposable
{
    // xUnit makes a new instance of this class for every test,
    // so every test gets its own file.
    private readonly string _path =
        Path.Combine(Path.GetTempPath(), $"shopclock-test-{Guid.NewGuid()}.csv");

    public void Dispose()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }

    private static DateTimeOffset Central(int day, int hour, int minute) =>
        new(2026, 10, day, hour, minute, 0, TimeSpan.FromHours(-5));

    [Fact]
    public void Append_then_ReadAll_round_trips_every_field()
    {
        var store = new PunchStore(_path);
        var first = new Punch(Guid.NewGuid(), Central(5, 7, 42), PunchType.In, PunchSource.Clock);
        var second = new Punch(Guid.NewGuid(), Central(5, 17, 12), PunchType.Out, PunchSource.Manual, "Forgot to punch out");
        var third = new Punch(Guid.NewGuid(), Central(6, 8, 0), PunchType.In, PunchSource.Manual, "Void wrong punch", first.Id);

        store.Append(first);
        store.Append(second);
        store.Append(third);

        var readBack = store.ReadAll();

        Assert.Equal(3, readBack.Count);
        Assert.Equal(first, readBack[0]);   // records compare by value
        Assert.Equal(second, readBack[1]);
        Assert.Equal(third, readBack[2]);
    }

    [Fact]
    public void Timestamp_offset_survives_round_trip()
    {
        var store = new PunchStore(_path);
        store.Append(new Punch(Guid.NewGuid(), Central(5, 7, 42), PunchType.In, PunchSource.Clock));

        var readBack = store.ReadAll().Single();

        // DateTimeOffset equality only compares the instant, so check the offset separately.
        Assert.Equal(TimeSpan.FromHours(-5), readBack.Timestamp.Offset);
    }

    [Fact]
    public void New_file_gets_exactly_one_header()
    {
        var store = new PunchStore(_path);
        store.Append(new Punch(Guid.NewGuid(), Central(5, 7, 0), PunchType.In, PunchSource.Clock));
        store.Append(new Punch(Guid.NewGuid(), Central(5, 17, 0), PunchType.Out, PunchSource.Clock));

        var lines = File.ReadAllLines(_path);
        const string header = "Id,Timestamp,Type,Source,Note,Voids";

        Assert.Equal(header, lines[0]);
        Assert.Single(lines, l => l == header);
        Assert.Equal(3, lines.Length);   // header + 2 punches
    }

    [Fact]
    public void ReadAll_on_missing_file_returns_empty()
    {
        var store = new PunchStore(_path);
        Assert.Empty(store.ReadAll());
    }

    // EXPECTED TO FAIL until comma escaping is added (Sprint 1, step 3).
    // Right now ReadAll throws FormatException because the note splits into extra columns.
    [Fact]
    public void Note_with_comma_round_trips()
    {
        var store = new PunchStore(_path);
        var punch = new Punch(Guid.NewGuid(), Central(5, 17, 15), PunchType.Out, PunchSource.Manual,
            "Forgot to punch, left 5:15");

        store.Append(punch);

        Assert.Equal(punch, store.ReadAll().Single());
    }
}