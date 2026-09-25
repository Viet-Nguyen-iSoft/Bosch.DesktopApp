namespace LTP.Truck.Forms
{
  partial class FrmReportGoods
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
      DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
      tableLayoutPanel9 = new TableLayoutPanel();
      dgv = new DataGridView();
      tableLayoutPanel11 = new TableLayoutPanel();
      label9 = new Label();
      txtSearchKey = new LTP.Truck.Custom.RJTextBox();
      label17 = new Label();
      label18 = new Label();
      ucTimeSearchFrom = new LTP.Truck.UserControls.UcTimeSearch();
      ucTimeSearchTo = new LTP.Truck.UserControls.UcTimeSearch();
      btnSearchHistorical = new LTP.Truck.Custom.RJButton();
      btnExport = new LTP.Truck.Custom.RJButton();
      cbbType = new ComboBox();
      label27 = new Label();
      ucPage1 = new LTP.Truck.UserControls.UcPage();
      btnTracking = new LTP.Truck.Custom.RJButton();
      tableLayoutPanel9.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
      tableLayoutPanel11.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel9
      // 
      tableLayoutPanel9.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel9.ColumnCount = 1;
      tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel9.Controls.Add(dgv, 0, 2);
      tableLayoutPanel9.Controls.Add(tableLayoutPanel11, 0, 1);
      tableLayoutPanel9.Controls.Add(label27, 0, 0);
      tableLayoutPanel9.Controls.Add(ucPage1, 0, 3);
      tableLayoutPanel9.Dock = DockStyle.Fill;
      tableLayoutPanel9.Location = new Point(0, 0);
      tableLayoutPanel9.Margin = new Padding(0);
      tableLayoutPanel9.Name = "tableLayoutPanel9";
      tableLayoutPanel9.RowCount = 4;
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel9.Size = new Size(1925, 607);
      tableLayoutPanel9.TabIndex = 4;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = SystemColors.Control;
      dataGridViewCellStyle4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = SystemColors.Window;
      dataGridViewCellStyle5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
      dgv.DefaultCellStyle = dataGridViewCellStyle5;
      dgv.EnableHeadersVisualStyles = false;
      dgv.Location = new Point(3, 115);
      dgv.Name = "dgv";
      dgv.ReadOnly = true;
      dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = SystemColors.Control;
      dataGridViewCellStyle6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1919, 429);
      dgv.TabIndex = 23;
      // 
      // tableLayoutPanel11
      // 
      tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel11.ColumnCount = 13;
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel11.Controls.Add(label9, 2, 0);
      tableLayoutPanel11.Controls.Add(txtSearchKey, 3, 0);
      tableLayoutPanel11.Controls.Add(label17, 5, 0);
      tableLayoutPanel11.Controls.Add(label18, 7, 0);
      tableLayoutPanel11.Controls.Add(ucTimeSearchFrom, 6, 0);
      tableLayoutPanel11.Controls.Add(ucTimeSearchTo, 8, 0);
      tableLayoutPanel11.Controls.Add(btnSearchHistorical, 10, 0);
      tableLayoutPanel11.Controls.Add(btnExport, 11, 0);
      tableLayoutPanel11.Controls.Add(cbbType, 0, 0);
      tableLayoutPanel11.Controls.Add(btnTracking, 12, 0);
      tableLayoutPanel11.Location = new Point(0, 50);
      tableLayoutPanel11.Margin = new Padding(0);
      tableLayoutPanel11.Name = "tableLayoutPanel11";
      tableLayoutPanel11.RowCount = 1;
      tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.Size = new Size(1925, 62);
      tableLayoutPanel11.TabIndex = 22;
      // 
      // label9
      // 
      label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label9.AutoSize = true;
      label9.BackColor = Color.Transparent;
      label9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label9.Location = new Point(260, 0);
      label9.Margin = new Padding(0);
      label9.Name = "label9";
      label9.Size = new Size(95, 62);
      label9.TabIndex = 17;
      label9.Text = "Tìm kiếm:";
      label9.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtSearchKey
      // 
      txtSearchKey.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtSearchKey.BackColor = SystemColors.Window;
      txtSearchKey.BorderColor = Color.Black;
      txtSearchKey.BorderFocusColor = Color.HotPink;
      txtSearchKey.BorderRadius = 5;
      txtSearchKey.BorderSize = 2;
      txtSearchKey.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtSearchKey.ForeColor = Color.FromArgb(64, 64, 64);
      txtSearchKey.Location = new Point(359, 12);
      txtSearchKey.Margin = new Padding(4);
      txtSearchKey.Multiline = false;
      txtSearchKey.Name = "txtSearchKey";
      txtSearchKey.Padding = new Padding(10, 7, 10, 7);
      txtSearchKey.PasswordChar = false;
      txtSearchKey.PlaceholderColor = Color.DarkGray;
      txtSearchKey.PlaceholderText = "";
      txtSearchKey.Size = new Size(228, 38);
      txtSearchKey.TabIndex = 18;
      txtSearchKey.Texts = "";
      txtSearchKey.UnderlinedStyle = false;
      // 
      // label17
      // 
      label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label17.AutoSize = true;
      label17.BackColor = Color.Transparent;
      label17.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label17.Location = new Point(611, 0);
      label17.Margin = new Padding(0);
      label17.Name = "label17";
      label17.Size = new Size(32, 62);
      label17.TabIndex = 21;
      label17.Text = "Từ";
      label17.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label18
      // 
      label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label18.AutoSize = true;
      label18.BackColor = Color.Transparent;
      label18.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label18.Location = new Point(963, 0);
      label18.Margin = new Padding(0);
      label18.Name = "label18";
      label18.Size = new Size(42, 62);
      label18.TabIndex = 22;
      label18.Text = "đến";
      label18.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucTimeSearchFrom
      // 
      ucTimeSearchFrom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchFrom.Location = new Point(646, 3);
      ucTimeSearchFrom.Name = "ucTimeSearchFrom";
      ucTimeSearchFrom.Size = new Size(314, 56);
      ucTimeSearchFrom.TabIndex = 28;
      // 
      // ucTimeSearchTo
      // 
      ucTimeSearchTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchTo.Location = new Point(1008, 3);
      ucTimeSearchTo.Name = "ucTimeSearchTo";
      ucTimeSearchTo.Size = new Size(314, 56);
      ucTimeSearchTo.TabIndex = 29;
      // 
      // btnSearchHistorical
      // 
      btnSearchHistorical.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnSearchHistorical.BackColor = Color.FromArgb(64, 107, 177);
      btnSearchHistorical.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnSearchHistorical.BorderColor = Color.White;
      btnSearchHistorical.BorderRadius = 5;
      btnSearchHistorical.BorderSize = 0;
      btnSearchHistorical.FlatAppearance.BorderColor = Color.White;
      btnSearchHistorical.FlatAppearance.BorderSize = 0;
      btnSearchHistorical.FlatStyle = FlatStyle.Flat;
      btnSearchHistorical.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnSearchHistorical.ForeColor = Color.White;
      btnSearchHistorical.Image = Properties.Resources.icon_search;
      btnSearchHistorical.ImageAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.Location = new Point(1348, 3);
      btnSearchHistorical.Name = "btnSearchHistorical";
      btnSearchHistorical.Padding = new Padding(15, 0, 0, 0);
      btnSearchHistorical.Size = new Size(174, 55);
      btnSearchHistorical.TabIndex = 27;
      btnSearchHistorical.Text = "        Tìm kiếm";
      btnSearchHistorical.TextAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.TextColor = Color.White;
      btnSearchHistorical.UseVisualStyleBackColor = false;
      // 
      // btnExport
      // 
      btnExport.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnExport.BackColor = Color.Green;
      btnExport.BackgroundColor = Color.Green;
      btnExport.BorderColor = Color.PaleVioletRed;
      btnExport.BorderRadius = 5;
      btnExport.BorderSize = 0;
      btnExport.FlatAppearance.BorderSize = 0;
      btnExport.FlatStyle = FlatStyle.Flat;
      btnExport.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnExport.ForeColor = Color.White;
      btnExport.Image = Properties.Resources.icon_excel;
      btnExport.ImageAlign = ContentAlignment.MiddleLeft;
      btnExport.Location = new Point(1528, 4);
      btnExport.Margin = new Padding(3, 3, 6, 3);
      btnExport.Name = "btnExport";
      btnExport.Padding = new Padding(10, 0, 0, 0);
      btnExport.Size = new Size(191, 54);
      btnExport.TabIndex = 30;
      btnExport.Text = "        Xuất excel";
      btnExport.TextAlign = ContentAlignment.MiddleLeft;
      btnExport.TextColor = Color.White;
      btnExport.UseVisualStyleBackColor = false;
      btnExport.Click += btnExport_Click;
      // 
      // cbbType
      // 
      cbbType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbType.Font = new Font("Roboto", 16.25F, FontStyle.Bold);
      cbbType.FormattingEnabled = true;
      cbbType.Items.AddRange(new object[] { "Chi tiết", "Nhóm theo biển số" });
      cbbType.Location = new Point(3, 13);
      cbbType.Name = "cbbType";
      cbbType.Size = new Size(244, 35);
      cbbType.TabIndex = 32;
      // 
      // label27
      // 
      label27.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label27.AutoSize = true;
      label27.BackColor = Color.FromArgb(199, 199, 199);
      label27.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label27.Location = new Point(0, 0);
      label27.Margin = new Padding(0);
      label27.Name = "label27";
      label27.Size = new Size(1925, 50);
      label27.TabIndex = 0;
      label27.Text = "Lịch sử cân";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucPage1
      // 
      ucPage1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucPage1.Location = new Point(3, 550);
      ucPage1.Name = "ucPage1";
      ucPage1.Size = new Size(1919, 54);
      ucPage1.TabIndex = 24;
      // 
      // btnTracking
      // 
      btnTracking.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnTracking.BackColor = Color.Green;
      btnTracking.BackgroundColor = Color.Green;
      btnTracking.BorderColor = Color.PaleVioletRed;
      btnTracking.BorderRadius = 5;
      btnTracking.BorderSize = 0;
      btnTracking.FlatAppearance.BorderSize = 0;
      btnTracking.FlatStyle = FlatStyle.Flat;
      btnTracking.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnTracking.ForeColor = Color.White;
      btnTracking.Image = Properties.Resources.icon_excel;
      btnTracking.ImageAlign = ContentAlignment.MiddleLeft;
      btnTracking.Location = new Point(1728, 4);
      btnTracking.Margin = new Padding(3, 3, 6, 3);
      btnTracking.Name = "btnTracking";
      btnTracking.Padding = new Padding(10, 0, 0, 0);
      btnTracking.Size = new Size(191, 54);
      btnTracking.TabIndex = 33;
      btnTracking.Text = "        Xuất tracking";
      btnTracking.TextAlign = ContentAlignment.MiddleLeft;
      btnTracking.TextColor = Color.White;
      btnTracking.UseVisualStyleBackColor = false;
      btnTracking.Click += btnTracking_Click;
      // 
      // FrmReportGoods
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1925, 607);
      Controls.Add(tableLayoutPanel9);
      Name = "FrmReportGoods";
      Text = "FrmReportGoods";
      tableLayoutPanel9.ResumeLayout(false);
      tableLayoutPanel9.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      tableLayoutPanel11.ResumeLayout(false);
      tableLayoutPanel11.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel9;
    private DataGridView dgv;
    private TableLayoutPanel tableLayoutPanel11;
    private Label label9;
    private Custom.RJTextBox txtSearchKey;
    private Label label17;
    private Label label18;
    private UserControls.UcTimeSearch ucTimeSearchFrom;
    private UserControls.UcTimeSearch ucTimeSearchTo;
    private Custom.RJButton btnSearchHistorical;
    private Custom.RJButton btnExport;
    private Label label27;
    private UserControls.UcPage ucPage1;
    private ComboBox cbbType;
    private Custom.RJButton btnTracking;
  }
}