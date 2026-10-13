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
            statusLabel.ForeColor = IsStaleOpenShift(last) ? Color.Red : SystemColors.ControlText;
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
            else if (IsStaleOpenShift(shift))
                return $"Still Clocked in from  {shift.In!.Value:ddd h:mm tt}. Forgot to clock out?";
            else if (shift.IsOpen)
                return $"Clocked in since {shift.In!.Value:h:mm tt}";
            else if (shift.Duration is { } worked)
                return $"Last shift ended {shift.Out!.Value:ddd h:mm tt} ({worked.TotalHours:0.00} hrs)";
            else
                return $"Last shift ended {shift.Out!.Value:ddd h:mm tt}";
        }

        private static bool IsStaleOpenShift(Shift? shift) => shift is not null && shift.IsOpen && shift.In!.Value.Date < DateTime.Today;

        private async void clockButton_Click(object sender, EventArgs e)
        {
            clockButton.Enabled = false;
            try
            {
                var last = GetLastShift(_store.ReadAll());
                var type = last?.IsOpen == true ? PunchType.Out : PunchType.In;

                if (type == PunchType.Out && IsStaleOpenShift(last))
                {
                    var hours = (DateTimeOffset.Now - last!.In!.Value).TotalHours;
                    var answer = MessageBox.Show(
                        $"You've been clocked in since {last.In.Value:ddd M/d h:mm tt}.\n\n" +
                        $"Clocking out now records a {hours:0.0}-hour shift.\n\n" +
                        "If you forgot to clock out, fix that day first instead.\n\nClock out anyway?",
                        "ShopClock", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (answer != DialogResult.Yes)
                        return;
                }

                var punch = Punch.Now(type);
                if (!PunchSaver.TrySave(_store, punch))
                    return;

                RefreshStatus();
            }
            finally
            {
                await Task.Delay(ClickCooldownMs);
                clockButton.Enabled = true;
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            using var report = new ReportForm(_store);
            report.ShowDialog(this);
            RefreshStatus();
        }
    }
}
