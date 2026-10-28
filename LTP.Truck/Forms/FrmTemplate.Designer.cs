namespace LTP.Truck.Forms
{
  partial class FrmTemplate
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
      DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
      tableLayoutPanel7 = new TableLayoutPanel();
      dgv = new DataGridView();
      label27 = new Label();
      btnSearch = new LTP.Truck.Custom.RJButton();
      tableLayoutPanel10 = new TableLayoutPanel();
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
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Size = new Size(1187, 560);
      tableLayoutPanel7.TabIndex = 4;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = SystemColors.Control;
      dataGridViewCellStyle7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle8.BackColor = SystemColors.Window;
      dataGridViewCellStyle8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
      dgv.DefaultCellStyle = dataGridViewCellStyle8;
      dgv.EnableHeadersVisualStyles = false;
      dgv.Location = new Point(3, 113);
      dgv.Name = "dgv";
      dgv.ReadOnly = true;
      dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle9.BackColor = SystemColors.Control;
      dataGridViewCellStyle9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1181, 444);
      dgv.TabIndex = 23;
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
      label27.Size = new Size(1187, 50);
      label27.TabIndex = 0;
      label27.Text = "Danh sách template";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // btnSearch
      // 
      btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnSearch.BackColor = Color.FromArgb(64, 107, 177);
      btnSearch.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnSearch.BorderColor = Color.White;
      btnSearch.BorderRadius = 5;
      btnSearch.BorderSize = 0;
      btnSearch.FlatAppearance.BorderColor = Color.White;
      btnSearch.FlatAppearance.BorderSize = 0;
      btnSearch.FlatStyle = FlatStyle.Flat;
      btnSearch.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnSearch.ForeColor = Color.White;
      btnSearch.Image = Properties.Resources.icon_search;
      btnSearch.ImageAlign = ContentAlignment.MiddleLeft;
      btnSearch.Location = new Point(990, 3);
      btnSearch.Name = "btnSearch";
      btnSearch.Padding = new Padding(10, 0, 0, 0);
      btnSearch.Size = new Size(194, 54);
      btnSearch.TabIndex = 27;
      btnSearch.Text = "Tìm kiếm";
      btnSearch.TextColor = Color.White;
      btnSearch.UseVisualStyleBackColor = false;
      // 
      // tableLayoutPanel10
      // 
      tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel10.ColumnCount = 2;
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel10.Controls.Add(btnSearch, 1, 0);
      tableLayoutPanel10.Location = new Point(0, 50);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1187, 60);
      tableLayoutPanel10.TabIndex = 22;
      // 
      // FrmTemplate
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1187, 560);
      Controls.Add(tableLayoutPanel7);
      Name = "FrmTemplate";
      Text = "FrmTemplate";
      tableLayoutPanel7.ResumeLayout(false);
      tableLayoutPanel7.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      tableLayoutPanel10.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel7;
    private DataGridView dgv;
    private Label label27;
    private TableLayoutPanel tableLayoutPanel10;
    private Custom.RJButton btnSearch;
  }
}