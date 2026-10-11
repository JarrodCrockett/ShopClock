using ShopClock.Core;

namespace ShopClock.App
{
    public partial class MainForm : Form
    {
        private readonly PunchStore _store;
        public MainForm(PunchStore store)
        {
            InitializeComponent();
            _store = store;
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            var last = GetLastShift();
            clockButton.Text = GetClockButtonText(last);
            statusLabel.Text = GetStatusLabelText(last);
        }

        private Shift? GetLastShift() => ShiftBuilder.Build(_store.ReadAll()).LastOrDefault();

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
            else
                return $"Last shift ended {shift.Out!.Value:ddd h:mm tt}";
        }

        private void clockButton_Click(object sender, EventArgs e)
        {
            var last = GetLastShift();
            var type = last?.IsOpen == true ? PunchType.Out : PunchType.In;

            var punch = Punch.Now(type);
            _store.Append(punch);
            RefreshStatus();
        }
    }
}
