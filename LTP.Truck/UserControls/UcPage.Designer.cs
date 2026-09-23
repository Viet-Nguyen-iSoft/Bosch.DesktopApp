namespace LTP.Truck.UserControls
{
  partial class UcPage
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      tableLayoutPanel1 = new TableLayoutPanel();
      tableLayoutPanel3 = new TableLayoutPanel();
      lbTotal = new Label();
      label2 = new Label();
      lbInforPage = new Label();
      btnPrevious = new LTP.Truck.Custom.RJButton();
      tableLayoutPanel2 = new TableLayoutPanel();
      label1 = new Label();
      cbbNumberRecord = new ComboBox();
      btnNext = new LTP.Truck.Custom.RJButton();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = SystemColors.GradientActiveCaption;
      tableLayoutPanel1.ColumnCount = 5;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 4, 0);
      tableLayoutPanel1.Controls.Add(lbInforPage, 2, 0);
      tableLayoutPanel1.Controls.Add(btnPrevious, 1, 0);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
      tableLayoutPanel1.Controls.Add(btnNext, 3, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(1321, 54);
      tableLayoutPanel1.TabIndex = 0;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.ColumnCount = 2;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel3.Controls.Add(lbTotal, 1, 0);
      tableLayoutPanel3.Controls.Add(label2, 0, 0);
      tableLayoutPanel3.Location = new Point(885, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 1;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Size = new Size(436, 54);
      tableLayoutPanel3.TabIndex = 31;
      // 
      // lbTotal
      // 
      lbTotal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTotal.AutoSize = true;
      lbTotal.BackColor = Color.Transparent;
      lbTotal.Font = new Font("Roboto", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTotal.Location = new Point(336, 0);
      lbTotal.Margin = new Padding(0);
      lbTotal.Name = "lbTotal";
      lbTotal.Size = new Size(100, 54);
      lbTotal.TabIndex = 9;
      lbTotal.Text = "00";
      lbTotal.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(336, 54);
      label2.TabIndex = 8;
      label2.Text = "Tổng số dữ liệu:";
      label2.TextAlign = ContentAlignment.MiddleRight;
      // 
      // lbInforPage
      // 
      lbInforPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbInforPage.AutoSize = true;
      lbInforPage.BackColor = Color.Transparent;
      lbInforPage.Font = new Font("Roboto", 14F);
      lbInforPage.Location = new Point(585, 0);
      lbInforPage.Margin = new Padding(0);
      lbInforPage.Name = "lbInforPage";
      lbInforPage.Size = new Size(150, 54);
      lbInforPage.TabIndex = 30;
      lbInforPage.Text = "1/1";
      lbInforPage.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // btnPrevious
      // 
      btnPrevious.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnPrevious.BackColor = Color.FromArgb(64, 107, 177);
      btnPrevious.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnPrevious.BorderColor = Color.White;
      btnPrevious.BorderRadius = 5;
      btnPrevious.BorderSize = 0;
      btnPrevious.FlatAppearance.BorderColor = Color.White;
      btnPrevious.FlatAppearance.BorderSize = 0;
      btnPrevious.FlatStyle = FlatStyle.Flat;
      btnPrevious.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnPrevious.ForeColor = Color.White;
      btnPrevious.ImageAlign = ContentAlignment.MiddleLeft;
      btnPrevious.Location = new Point(438, 3);
      btnPrevious.Name = "btnPrevious";
      btnPrevious.Padding = new Padding(15, 0, 0, 0);
      btnPrevious.Size = new Size(144, 48);
      btnPrevious.TabIndex = 28;
      btnPrevious.Text = "<<";
      btnPrevious.TextColor = Color.White;
      btnPrevious.UseVisualStyleBackColor = false;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 2;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Controls.Add(label1, 0, 0);
      tableLayoutPanel2.Controls.Add(cbbNumberRecord, 1, 0);
      tableLayoutPanel2.Location = new Point(0, 0);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(435, 54);
      tableLayoutPanel2.TabIndex = 0;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto", 14F);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(205, 54);
      label1.TabIndex = 8;
      label1.Text = "Số dữ liệu mỗi trang";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbNumberRecord
      // 
      cbbNumberRecord.Anchor = AnchorStyles.Left;
      cbbNumberRecord.Font = new Font("Roboto", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      cbbNumberRecord.FormattingEnabled = true;
      cbbNumberRecord.Items.AddRange(new object[] { "5", "10", "20", "50", "100", "200", "500" });
      cbbNumberRecord.Location = new Point(208, 10);
      cbbNumberRecord.Name = "cbbNumberRecord";
      cbbNumberRecord.Size = new Size(170, 33);
      cbbNumberRecord.TabIndex = 9;
      // 
      // btnNext
      // 
      btnNext.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnNext.BackColor = Color.FromArgb(64, 107, 177);
      btnNext.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnNext.BorderColor = Color.White;
      btnNext.BorderRadius = 5;
      btnNext.BorderSize = 0;
      btnNext.FlatAppearance.BorderColor = Color.White;
      btnNext.FlatAppearance.BorderSize = 0;
      btnNext.FlatStyle = FlatStyle.Flat;
      btnNext.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnNext.ForeColor = Color.White;
      btnNext.ImageAlign = ContentAlignment.MiddleLeft;
      btnNext.Location = new Point(738, 3);
      btnNext.Name = "btnNext";
      btnNext.Padding = new Padding(15, 0, 0, 0);
      btnNext.Size = new Size(144, 48);
      btnNext.TabIndex = 29;
      btnNext.Text = ">>";
      btnNext.TextColor = Color.White;
      btnNext.UseVisualStyleBackColor = false;
      // 
      // UcPage
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tableLayoutPanel1);
      Name = "UcPage";
      Size = new Size(1321, 54);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel2.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private Label label1;
    private ComboBox cbbNumberRecord;
    private Custom.RJButton btnPrevious;
    private Custom.RJButton btnNext;
    private Label lbInforPage;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbTotal;
    private Label label2;
  }
}
