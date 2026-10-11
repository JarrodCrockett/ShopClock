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
            this.clockButton = new Button();
            statusLabel = new Label();
            SuspendLayout();
            // 
            // clockButton
            // 
            this.clockButton.Location = new Point(224, 171);
            this.clockButton.Name = "clockButton";
            this.clockButton.Size = new Size(183, 78);
            this.clockButton.TabIndex = 0;
            this.clockButton.Text = "Clock In";
            this.clockButton.UseVisualStyleBackColor = true;
            this.clockButton.Click += this.clockButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(309, 131);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Status";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusLabel);
            Controls.Add(this.clockButton);
            Name = "MainForm";
            Text = "ShopClock";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button clockButton;
        private Label statusLabel;
    }
}
