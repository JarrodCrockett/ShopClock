using ShopClock.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShopClock.App
{
    public partial class AddPunchForm : Form
    {
        public Punch? Result { get; private set; }

        public AddPunchForm(DateTime? initialDate = null)
        {
            InitializeComponent();
            datePicker.Value = initialDate ?? DateTime.Today;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var note = noteField.Text.Trim();
            if (note.Length == 0)
            {
                MessageBox.Show("A note is required, e.g. \"Forgot to clock in\".", "ShopClock");
                return;
            }

            var local = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            var timestamp = new DateTimeOffset(local);          // uses this PC's offset for that date
            if (timestamp > DateTimeOffset.Now)
            {
                MessageBox.Show("That time is in the future.", "ShopClock");
                return;
            }

            var type = inRadioBtn.Checked ? PunchType.In : PunchType.Out;
            Result = new Punch(Guid.NewGuid(), timestamp, type, PunchSource.Manual, note);
            DialogResult = DialogResult.OK;
        }
    }
}
