namespace LTP.Truck.Forms
{
  partial class FrmReportTruck
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
      tableLayoutPanel7 = new TableLayoutPanel();
      dgv = new DataGridView();
      tableLayoutPanel10 = new TableLayoutPanel();
      label4 = new Label();
      txtSearchKey = new LTP.Truck.Custom.RJTextBox();
      label17 = new Label();
      label18 = new Label();
      ucTimeSearchFrom = new LTP.Truck.UserControls.UcTimeSearch();
      ucTimeSearchTo = new LTP.Truck.UserControls.UcTimeSearch();
      btnFilter = new Common.Custom.RJButton();
      btnExport = new LTP.Truck.Custom.RJButton();
      btnSearchHistorical = new LTP.Truck.Custom.RJButton();
      label27 = new Label();
      tableLayoutPanel7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
      tableLayoutPanel10.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel7
      // 
      tableLayoutPanel7.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel7.ColumnCount = 1;
      tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Controls.Add(dgv, 0, 2);
      tableLayoutPanel7.Controls.Add(tableLayoutPanel10, 0, 1);
      tableLayoutPanel7.Controls.Add(label27, 0, 0);
      tableLayoutPanel7.Dock = DockStyle.Fill;
      tableLayoutPanel7.Location = new Point(0, 0);
      tableLayoutPanel7.Margin = new Padding(0);
      tableLayoutPanel7.Name = "tableLayoutPanel7";
      tableLayoutPanel7.RowCount = 3;
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Size = new Size(1551, 550);
      tableLayoutPanel7.TabIndex = 3;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = SystemColors.Control;
      dataGridViewCellStyle4.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = SystemColors.Window;
      dataGridViewCellStyle5.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
      dataGridViewCellStyle6.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1545, 432);
      dgv.TabIndex = 23;
      // 
      // tableLayoutPanel10
      // 
      tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel10.ColumnCount = 11;
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.Controls.Add(label4, 0, 0);
      tableLayoutPanel10.Controls.Add(txtSearchKey, 1, 0);
      tableLayoutPanel10.Controls.Add(label17, 3, 0);
      tableLayoutPanel10.Controls.Add(label18, 5, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchFrom, 4, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchTo, 6, 0);
      tableLayoutPanel10.Controls.Add(btnFilter, 7, 0);
      tableLayoutPanel10.Controls.Add(btnExport, 10, 0);
      tableLayoutPanel10.Controls.Add(btnSearchHistorical, 9, 0);
      tableLayoutPanel10.Location = new Point(0, 50);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1551, 62);
      tableLayoutPanel10.TabIndex = 22;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label4.Location = new Point(0, 0);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(106, 62);
      label4.TabIndex = 17;
      label4.Text = "Tìm kiếm:";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtSearchKey
      // 
      txtSearchKey.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtSearchKey.BackColor = SystemColors.Window;
      txtSearchKey.BorderColor = Color.Black;
      txtSearchKey.BorderFocusColor = Color.HotPink;
      txtSearchKey.BorderRadius = 5;
      txtSearchKey.BorderSize = 2;
      txtSearchKey.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtSearchKey.ForeColor = Color.FromArgb(64, 64, 64);
      txtSearchKey.Location = new Point(110, 10);
      txtSearchKey.Margin = new Padding(4);
      txtSearchKey.Multiline = false;
      txtSearchKey.Name = "txtSearchKey";
      txtSearchKey.Padding = new Padding(10, 7, 10, 7);
      txtSearchKey.PasswordChar = false;
      txtSearchKey.PlaceholderColor = Color.DarkGray;
      txtSearchKey.PlaceholderText = "";
      txtSearchKey.Size = new Size(235, 42);
      txtSearchKey.TabIndex = 18;
      txtSearchKey.Texts = "";
      txtSearchKey.UnderlinedStyle = false;
      // 
      // label17
      // 
      label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label17.AutoSize = true;
      label17.BackColor = Color.Transparent;
      label17.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label17.Location = new Point(399, 0);
      label17.Margin = new Padding(0);
      label17.Name = "label17";
      label17.Size = new Size(38, 62);
      label17.TabIndex = 21;
      label17.Text = "Từ";
      label17.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label18
      // 
      label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label18.AutoSize = true;
      label18.BackColor = Color.Transparent;
      label18.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label18.Location = new Point(757, 0);
      label18.Margin = new Padding(0);
      label18.Name = "label18";
      label18.Size = new Size(49, 62);
      label18.TabIndex = 22;
      label18.Text = "đến";
      label18.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucTimeSearchFrom
      // 
      ucTimeSearchFrom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchFrom.Location = new Point(440, 3);
      ucTimeSearchFrom.Name = "ucTimeSearchFrom";
      ucTimeSearchFrom.Size = new Size(314, 56);
      ucTimeSearchFrom.TabIndex = 30;
      // 
      // ucTimeSearchTo
      // 
      ucTimeSearchTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchTo.Location = new Point(809, 3);
      ucTimeSearchTo.Name = "ucTimeSearchTo";
      ucTimeSearchTo.Size = new Size(314, 56);
      ucTimeSearchTo.TabIndex = 31;
      // 
      // btnFilter
      // 
      btnFilter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnFilter.BackColor = Color.FromArgb(64, 107, 177);
      btnFilter.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnFilter.BorderColor = Color.PaleVioletRed;
      btnFilter.BorderRadius = 5;
      btnFilter.BorderSize = 0;
      btnFilter.FlatAppearance.BorderSize = 0;
      btnFilter.FlatStyle = FlatStyle.Flat;
      btnFilter.ForeColor = Color.White;
      btnFilter.Image = Properties.Resources.icon_filter;
      btnFilter.Location = new Point(1129, 3);
      btnFilter.Name = "btnFilter";
      btnFilter.Size = new Size(54, 55);
      btnFilter.TabIndex = 32;
      btnFilter.TextColor = Color.White;
      btnFilter.UseVisualStyleBackColor = false;
      // 
      // btnExport
      // 
      btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnExport.BackColor = Color.Green;
      btnExport.BackgroundColor = Color.Green;
      btnExport.BorderColor = Color.White;
      btnExport.BorderRadius = 5;
      btnExport.BorderSize = 0;
      btnExport.FlatAppearance.BorderColor = Color.White;
      btnExport.FlatAppearance.BorderSize = 0;
      btnExport.FlatStyle = FlatStyle.Flat;
      btnExport.Font = new Font("Roboto", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnExport.ForeColor = Color.White;
      btnExport.Image = Properties.Resources.icon_excel;
      btnExport.ImageAlign = ContentAlignment.MiddleLeft;
      btnExport.Location = new Point(1374, 3);
      btnExport.Name = "btnExport";
      btnExport.Padding = new Padding(10, 0, 0, 0);
      btnExport.Size = new Size(174, 56);
      btnExport.TabIndex = 20;
      btnExport.Text = "       Xuất excel";
      btnExport.TextAlign = ContentAlignment.MiddleLeft;
      btnExport.TextColor = Color.White;
      btnExport.UseVisualStyleBackColor = false;
      btnExport.Click += btnExport_Click;
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
      btnSearchHistorical.Font = new Font("Roboto", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnSearchHistorical.ForeColor = Color.White;
      btnSearchHistorical.Image = Properties.Resources.icon_search;
      btnSearchHistorical.ImageAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.Location = new Point(1194, 3);
      btnSearchHistorical.Name = "btnSearchHistorical";
      btnSearchHistorical.Padding = new Padding(15, 0, 0, 0);
      btnSearchHistorical.Size = new Size(174, 55);
      btnSearchHistorical.TabIndex = 27;
      btnSearchHistorical.Text = "       Tìm kiếm";
      btnSearchHistorical.TextAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.TextColor = Color.White;
      btnSearchHistorical.UseVisualStyleBackColor = false;
      // 
      // label27
      // 
      label27.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label27.AutoSize = true;
      label27.BackColor = Color.FromArgb(199, 199, 199);
      label27.Font = new Font("Roboto", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label27.Location = new Point(0, 0);
      label27.Margin = new Padding(0);
      label27.Name = "label27";
      label27.Size = new Size(1551, 50);
      label27.TabIndex = 0;
      label27.Text = "Danh sách dữ liệu cân";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // FrmReportTruck
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1551, 550);
      Controls.Add(tableLayoutPanel7);
      Name = "FrmReportTruck";
      Text = "FrmReportTruck";
      tableLayoutPanel7.ResumeLayout(false);
      tableLayoutPanel7.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      tableLayoutPanel10.ResumeLayout(false);
      tableLayoutPanel10.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel7;
    private DataGridView dgv;
    private TableLayoutPanel tableLayoutPanel10;
    private Label label4;
    private Custom.RJTextBox txtSearchKey;
    private Label label17;
    private Custom.RJButton btnExport;
    private Label label18;
    private UserControls.UcTimeSearch ucTimeSearchFrom;
    private UserControls.UcTimeSearch ucTimeSearchTo;
    private Common.Custom.RJButton btnFilter;
    private Custom.RJButton btnSearchHistorical;
    private Label label27;
  }
}