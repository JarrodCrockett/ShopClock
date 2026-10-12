namespace ShopClock.App
{
    partial class ReportForm
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
            fromPicker = new DateTimePicker();
            toPicker = new DateTimePicker();
            loadButton = new Button();
            reportGrid = new DataGridView();
            totalLabel = new Label();
            fromLabel = new Label();
            toLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)reportGrid).BeginInit();
            SuspendLayout();
            // 
            // fromPicker
            // 
            fromPicker.Format = DateTimePickerFormat.Short;
            fromPicker.Location = new Point(216, 66);
            fromPicker.Name = "fromPicker";
            fromPicker.Size = new Size(200, 23);
            fromPicker.TabIndex = 0;
            // 
            // toPicker
            // 
            toPicker.Format = DateTimePickerFormat.Short;
            toPicker.Location = new Point(503, 66);
            toPicker.Name = "toPicker";
            toPicker.Size = new Size(200, 23);
            toPicker.TabIndex = 1;
            // 
            // loadButton
            // 
            loadButton.Location = new Point(764, 66);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(75, 23);
            loadButton.TabIndex = 2;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // reportGrid
            // 
            reportGrid.AllowUserToAddRows = false;
            reportGrid.AllowUserToDeleteRows = false;
            reportGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            reportGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            reportGrid.BackgroundColor = Color.White;
            reportGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            reportGrid.Location = new Point(103, 128);
            reportGrid.Name = "reportGrid";
            reportGrid.ReadOnly = true;
            reportGrid.RowHeadersVisible = false;
            reportGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            reportGrid.Size = new Size(810, 424);
            reportGrid.TabIndex = 3;
            reportGrid.DataBindingComplete += reportGrid_DataBindingComplete;
            // 
            // totalLabel
            // 
            totalLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            totalLabel.AutoSize = true;
            totalLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            totalLabel.Location = new Point(103, 581);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(126, 25);
            totalLabel.TabIndex = 4;
            totalLabel.Text = "Total: 0.00 hrs";
            // 
            // fromLabel
            // 
            fromLabel.AutoSize = true;
            fromLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fromLabel.Location = new Point(167, 66);
            fromLabel.Name = "fromLabel";
            fromLabel.Size = new Size(43, 20);
            fromLabel.TabIndex = 5;
            fromLabel.Text = "From";
            // 
            // toLabel
            // 
            toLabel.AutoSize = true;
            toLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toLabel.Location = new Point(472, 66);
            toLabel.Name = "toLabel";
            toLabel.Size = new Size(28, 20);
            toLabel.TabIndex = 6;
            toLabel.Text = "To:";
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 627);
            Controls.Add(toLabel);
            Controls.Add(fromLabel);
            Controls.Add(totalLabel);
            Controls.Add(reportGrid);
            Controls.Add(loadButton);
            Controls.Add(toPicker);
            Controls.Add(fromPicker);
            Name = "ReportForm";
            Text = "ShopClock - Hours";
            ((System.ComponentModel.ISupportInitialize)reportGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker fromPicker;
        private DateTimePicker toPicker;
        private Button loadButton;
        private DataGridView reportGrid;
        private Label totalLabel;
        private Label fromLabel;
        private Label toLabel;
    }
}