namespace ShopClock.App
{
    partial class AddPunchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            datePicker = new DateTimePicker();
            inRadioBtn = new RadioButton();
            outRadioBtn = new RadioButton();
            radioLabel = new Label();
            textboxLabel = new Label();
            noteField = new TextBox();
            timePicker = new DateTimePicker();
            saveButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // datePicker
            // 
            datePicker.Location = new Point(96, 68);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(200, 23);
            datePicker.TabIndex = 0;
            // 
            // inRadioBtn
            // 
            inRadioBtn.AutoSize = true;
            inRadioBtn.Checked = true;
            inRadioBtn.Location = new Point(189, 185);
            inRadioBtn.Name = "inRadioBtn";
            inRadioBtn.Size = new Size(35, 19);
            inRadioBtn.TabIndex = 1;
            inRadioBtn.TabStop = true;
            inRadioBtn.Text = "In";
            inRadioBtn.UseVisualStyleBackColor = true;
            // 
            // outRadioBtn
            // 
            outRadioBtn.AutoSize = true;
            outRadioBtn.Location = new Point(251, 183);
            outRadioBtn.Name = "outRadioBtn";
            outRadioBtn.Size = new Size(45, 19);
            outRadioBtn.TabIndex = 2;
            outRadioBtn.Text = "Out";
            outRadioBtn.UseVisualStyleBackColor = true;
            // 
            // radioLabel
            // 
            radioLabel.AutoSize = true;
            radioLabel.Location = new Point(96, 187);
            radioLabel.Name = "radioLabel";
            radioLabel.Size = new Size(72, 15);
            radioLabel.TabIndex = 3;
            radioLabel.Text = "Punch Type:";
            // 
            // textboxLabel
            // 
            textboxLabel.AutoSize = true;
            textboxLabel.Location = new Point(96, 244);
            textboxLabel.Name = "textboxLabel";
            textboxLabel.Size = new Size(36, 15);
            textboxLabel.TabIndex = 5;
            textboxLabel.Text = "Note:";
            // 
            // noteField
            // 
            noteField.Location = new Point(96, 262);
            noteField.Name = "noteField";
            noteField.PlaceholderText = "Reason for missed punch here.";
            noteField.Size = new Size(200, 23);
            noteField.TabIndex = 6;
            // 
            // timePicker
            // 
            timePicker.CustomFormat = "h:mm tt";
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.Location = new Point(96, 123);
            timePicker.Name = "timePicker";
            timePicker.ShowUpDown = true;
            timePicker.Size = new Size(200, 23);
            timePicker.TabIndex = 7;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(96, 339);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 8;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(221, 339);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(96, 50);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 10;
            label1.Text = "Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(96, 105);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 11;
            label2.Text = "Time:";
            // 
            // AddPunchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(402, 413);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Controls.Add(timePicker);
            Controls.Add(noteField);
            Controls.Add(textboxLabel);
            Controls.Add(radioLabel);
            Controls.Add(outRadioBtn);
            Controls.Add(inRadioBtn);
            Controls.Add(datePicker);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddPunchForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Punch Time";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker datePicker;
        private RadioButton inRadioBtn;
        private RadioButton outRadioBtn;
        private Label radioLabel;
        private Label textboxLabel;
        private TextBox noteField;
        private DateTimePicker timePicker;
        private Button saveButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
    }
}