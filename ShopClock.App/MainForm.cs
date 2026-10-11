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
            var punches = _store.ReadAll();
            var shifts = ShiftBuilder.Build(punches);
            var last = shifts.LastOrDefault();
            if (last is null)
            {
                clockButton.Text = "Clock In";
                statusLabel.Text = "Not clocked in.";
            }
            else if (last.IsOpen)
            {
                clockButton.Text = "Clock Out";
                statusLabel.Text = $"Clocked in since {last.In!.Value:h:mm tt}";
            }
            else
            {
                clockButton.Text = "Clock In";
                statusLabel.Text = $"Last shift ended {last.Out!.Value:ddd h:mm tt}";
            }
        }

        private void clockButton_Click(object sender, EventArgs e)
        {

        }
    }
}
