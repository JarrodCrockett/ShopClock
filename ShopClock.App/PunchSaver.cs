using ShopClock.Core;

namespace ShopClock.App;

internal static class PunchSaver
{
    public static bool TrySave(PunchStore store, Punch punch) => TrySaveRange(store, [punch]);

    public static bool TrySaveRange(PunchStore store, IReadOnlyList<Punch> punches)
    {
        try
        {
            store.AppendRange(punches);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(
                "Changes NOT saved. Is punches.csv open in Excel? Close it and try again.\n\n" + ex.Message,
                "ShopClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}