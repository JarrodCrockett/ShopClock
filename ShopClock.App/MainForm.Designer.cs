namespace ShopClock.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            clockButton = new Button();
            statusLabel = new Label();
            todayLabel = new Label();
            reportButton = new Button();
            SuspendLayout();
            // 
            // clockButton
            // 
            clockButton.Location = new Point(224, 171);
            clockButton.Name = "clockButton";
            clockButton.Size = new Size(183, 78);
            clockButton.TabIndex = 0;
            clockButton.Text = "Clock In";
            clockButton.UseVisualStyleBackColor = true;
            clockButton.Click += clockButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(224, 127);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Status";
            // 
            // todayLabel
            // 
            todayLabel.AutoSize = true;
            todayLabel.Location = new Point(224, 279);
            todayLabel.Name = "todayLabel";
            todayLabel.Size = new Size(85, 15);
            todayLabel.TabIndex = 2;
            todayLabel.Text = "Today: 0.00 hrs";
            // 
            // reportButton
            // 
            reportButton.Location = new Point(679, 408);
            reportButton.Name = "reportButton";
            reportButton.Size = new Size(109, 30);
            reportButton.TabIndex = 3;
            reportButton.Text = "Hours/Report";
            reportButton.UseVisualStyleBackColor = true;
            reportButton.Click += reportButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(reportButton);
            Controls.Add(todayLabel);
            Controls.Add(statusLabel);
            Controls.Add(clockButton);
            Name = "MainForm";
            Text = "ShopClock";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button clockButton;
        private Label statusLabel;
        private Label todayLabel;
        private Button reportButton;
    }
}
