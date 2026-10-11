using ShopClock.Core;

namespace ShopClock.App
{
    public partial class MainForm : Form
    {
        private readonly PunchStore _store;
        private const int ClickCooldownMs = 1000;
        public MainForm(PunchStore store)
        {
            InitializeComponent();
            _store = store;
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            var punches = _store.ReadAll();
            var last = GetLastShift(punches);
            var today = DateOnly.FromDateTime(DateTime.Now);
            var todayHours = ReportService.BuildReport(punches, today, today).TotalHours;

            clockButton.Text = GetClockButtonText(last);
            statusLabel.Text = GetStatusLabelText(last);
            todayLabel.Text = $"Today: {todayHours:0.00} hrs";
        }

        private Shift? GetLastShift(IReadOnlyList<Punch> punches) => ShiftBuilder.Build(punches).LastOrDefault();

        private string GetClockButtonText(Shift? shift)
        {
            if (shift is null || !shift.IsOpen)
                return "Clock In";
            else
                return "Clock Out";
        }

        private string GetStatusLabelText(Shift? shift)
        {
            if (shift is null)
                return "Not clocked in.";
            else if (shift.IsOpen)
                return $"Clocked in since {shift.In!.Value:h:mm tt}";
            else if (shift.Duration is { } worked)
                return $"Last shift ended {shift.Out!.Value:ddd h:mm tt} ({worked.TotalHours:0.00} hrs)";
            else
                return $"Last shift ended {shift.Out!.Value:ddd h:mm tt}";
        }

        private async void clockButton_Click(object sender, EventArgs e)
        {
            clockButton.Enabled = false;
            try
            {
                var last = GetLastShift(_store.ReadAll());
                var type = last?.IsOpen == true ? PunchType.Out : PunchType.In;

                var punch = Punch.Now(type);
                if (!TrySavePunch(punch))
                    return;

                RefreshStatus();
            }
            finally
            {
                await Task.Delay(ClickCooldownMs);
                clockButton.Enabled = true;
            }
        }

        private bool TrySavePunch(Punch punch)
        {
            try
            {
                _store.Append(punch);
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Punch NOT saved. Is punches.csv open in Excel? Close it and try again.\n\n" + ex.Message,
                    "ShopClock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
