using System;
using System.Collections.Generic;
using System.Text;

namespace ShopClock.Core
{
    public static class ShiftBuilder
    {
        public static IReadOnlyList<Shift> Build(IEnumerable<Punch> punches)
        {
            var all = punches.ToList();

            // A line with a Voids value is an instruction ("cancel that punch"),
            // not a punch itself. Collect what it cancels, then drop both.
            var voidedIds = all
                .Where(p => p.Voids is not null)
                .Select(p => p.Voids!.Value)
                .ToHashSet();

            // Manual punches are appended later than they happened,
            // so sort by when they happened, not file order.
            var active = all
                .Where(p => p.Voids is null && !voidedIds.Contains(p.Id))
                .OrderBy(p => p.Timestamp)
                .ToList();

            var shifts = new List<Shift>();
            Punch? openIn = null;

            foreach (var punch in active)
            {
                if (punch.Type == PunchType.In)
                {
                    // In followed by another In: the first one never got its Out.
                    if (openIn is not null)
                        shifts.Add(Shift.From(openIn, null, ShiftProblem.MissingOut));

                    openIn = punch;
                }
                else // Out
                {
                    if (openIn is null)
                    {
                        // Out with nothing to close.
                        shifts.Add(Shift.From(null, punch, ShiftProblem.MissingIn));
                    }
                    else
                    {
                        shifts.Add(Shift.From(openIn, punch, ShiftProblem.None));
                        openIn = null;
                    }
                }
            }

            // A trailing In with no Out is the shift you're on right now.
            if (openIn is not null)
                shifts.Add(Shift.From(openIn, null, ShiftProblem.None));

            return shifts;
        }
    }
}
