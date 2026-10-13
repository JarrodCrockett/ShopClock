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
    public partial class ReportForm : Form
    {
        private readonly PunchStore _store;
        private IReadOnlyList<ReportRow> _currentRows = [];

        public ReportForm(PunchStore store)
        {
            InitializeComponent();
            _store = store;
            toPicker.Value = DateTime.Today;
            fromPicker.Value = DateTime.Today.AddDays(-6);
            LoadReport();
        }

        private void LoadReport()
        {
            var from = DateOnly.FromDateTime(fromPicker.Value);
            var to = DateOnly.FromDateTime(toPicker.Value);

            if (to < from)
            {
                MessageBox.Show("The 'To' date must be the same or later than the 'From' date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var report = ReportService.BuildReport(_store.ReadAll(), from, to);
            _currentRows = report.Rows;
            reportGrid.DataSource = report.Rows.Select(r => new
            {
                Date = r.Date.ToString("d"),
                In = r.In?.ToString("h:mm tt") ?? "",
                Out = r.Out?.ToString("h:mm tt") ?? "",
                Hours = r.Hours?.ToString("0.00") ?? "",
                Notes = r.Notes ?? "",
            }).ToList();

            totalLabel.Text = $"Total: {report.TotalHours:0.00} hrs";
        }

        private void loadButton_Click(object sender, EventArgs e) => LoadReport();

        private void reportGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            reportGrid.ClearSelection();
            for (int i = 0; i < reportGrid.Rows.Count && i < _currentRows.Count; i++)   // NEW
            {
                var data = _currentRows[i];
                var style = reportGrid.Rows[i].DefaultCellStyle;

                if (data.Problem != ShiftProblem.None)
                    style.BackColor = Color.MistyRose;
                else if (data.HasManualPunch)
                    style.BackColor = Color.LightYellow;
            }
        }

        private void addPunchButton_Click(object sender, EventArgs e)
        {
            using var dialog = new AddPunchForm();
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.Result is { } punch)
            {
                if (PunchSaver.TrySave(_store, punch))
                    LoadReport();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (reportGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row to delete first.", "ShopClock");
                return;
            }

            var row = _currentRows[reportGrid.SelectedRows[0].Index];

            var reason = Microsoft.VisualBasic.Interaction.InputBox(
                $"Delete the shift on {row.Date:ddd M/d} " +
                $"({row.In?.ToString("h:mm tt") ?? "?"} – {row.Out?.ToString("h:mm tt") ?? "?"})?\n\n" +
                "Enter a reason:", "Delete shift").Trim();

            if (reason.Length == 0)
                return;   // Cancel, or no reason given: do nothing

            var now = DateTimeOffset.Now;
            var voids = new[] { row.InPunchId, row.OutPunchId }
                .Where(id => id is not null)
                .Select(id => new Punch(Guid.NewGuid(), now, PunchType.In, PunchSource.Manual,
                                        $"Deleted {now:M/d h:mm tt}: {reason}", id))
                .ToList();

            if (PunchSaver.TrySaveRange(_store, voids))
                LoadReport();
        }
    }
}
