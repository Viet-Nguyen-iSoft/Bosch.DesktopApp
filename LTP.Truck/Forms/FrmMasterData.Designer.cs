using LTP.Truck.Custom;

namespace LTP.Truck.Forms
{
  partial class FrmMasterData
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
      DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
      tableLayoutPanel7 = new TableLayoutPanel();
      dgv = new DataGridView();
      tableLayoutPanel10 = new TableLayoutPanel();
      label4 = new Label();
      txtSearch = new RJTextBox();
      btnSearch = new RJButton();
      btnAddnew = new RJButton();
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
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Size = new Size(1244, 580);
      tableLayoutPanel7.TabIndex = 3;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle10.BackColor = SystemColors.Control;
      dataGridViewCellStyle10.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle11.BackColor = SystemColors.Window;
      dataGridViewCellStyle11.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
      dgv.DefaultCellStyle = dataGridViewCellStyle11;
      dgv.EnableHeadersVisualStyles = false;
      dgv.Location = new Point(3, 113);
      dgv.Name = "dgv";
      dgv.ReadOnly = true;
      dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle12.BackColor = SystemColors.Control;
      dataGridViewCellStyle12.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1238, 464);
      dgv.TabIndex = 23;
      // 
      // tableLayoutPanel10
      // 
      tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel10.ColumnCount = 5;
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel10.Controls.Add(label4, 0, 0);
      tableLayoutPanel10.Controls.Add(txtSearch, 1, 0);
      tableLayoutPanel10.Controls.Add(btnSearch, 4, 0);
      tableLayoutPanel10.Controls.Add(btnAddnew, 3, 0);
      tableLayoutPanel10.Location = new Point(0, 50);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1244, 60);
      tableLayoutPanel10.TabIndex = 22;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label4.Location = new Point(0, 0);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(95, 60);
      label4.TabIndex = 17;
      label4.Text = "Tìm kiếm:";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtSearch
      // 
      txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtSearch.BackColor = SystemColors.Window;
      txtSearch.BorderColor = Color.Black;
      txtSearch.BorderFocusColor = Color.HotPink;
      txtSearch.BorderRadius = 5;
      txtSearch.BorderSize = 2;
      txtSearch.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtSearch.ForeColor = Color.FromArgb(64, 64, 64);
      txtSearch.Location = new Point(99, 11);
      txtSearch.Margin = new Padding(4);
      txtSearch.Multiline = false;
      txtSearch.Name = "txtSearch";
      txtSearch.Padding = new Padding(10, 7, 10, 7);
      txtSearch.PasswordChar = false;
      txtSearch.PlaceholderColor = Color.DarkGray;
      txtSearch.PlaceholderText = "";
      txtSearch.Size = new Size(516, 38);
      txtSearch.TabIndex = 18;
      txtSearch.Texts = "";
      txtSearch.UnderlinedStyle = false;
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
      btnSearch.Location = new Point(1046, 3);
      btnSearch.Name = "btnSearch";
      btnSearch.Padding = new Padding(10, 0, 0, 0);
      btnSearch.Size = new Size(195, 54);
      btnSearch.TabIndex = 27;
      btnSearch.Text = "Tìm kiếm";
      btnSearch.TextColor = Color.White;
      btnSearch.UseVisualStyleBackColor = false;
      btnSearch.Click += btnSearch_Click;
      // 
      // btnAddnew
      // 
      btnAddnew.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnAddnew.BackColor = Color.FromArgb(64, 107, 177);
      btnAddnew.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnAddnew.BorderColor = Color.White;
      btnAddnew.BorderRadius = 5;
      btnAddnew.BorderSize = 0;
      btnAddnew.FlatAppearance.BorderColor = Color.White;
      btnAddnew.FlatAppearance.BorderSize = 0;
      btnAddnew.FlatStyle = FlatStyle.Flat;
      btnAddnew.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnAddnew.ForeColor = Color.White;
      btnAddnew.Image = Properties.Resources.icon_add_new;
      btnAddnew.ImageAlign = ContentAlignment.MiddleLeft;
      btnAddnew.Location = new Point(846, 3);
      btnAddnew.Name = "btnAddnew";
      btnAddnew.Padding = new Padding(10, 0, 0, 0);
      btnAddnew.Size = new Size(194, 54);
      btnAddnew.TabIndex = 28;
      btnAddnew.Text = "Thêm mới";
      btnAddnew.TextColor = Color.White;
      btnAddnew.UseVisualStyleBackColor = false;
      btnAddnew.Click += btnAddnew_Click;
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
      label27.Size = new Size(1244, 50);
      label27.TabIndex = 0;
      label27.Text = "Danh sách dữ liệu";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // FrmMasterData
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1244, 580);
      Controls.Add(tableLayoutPanel7);
      Name = "FrmMasterData";
      Text = "FrmMasterData";
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
    private Custom.RJTextBox txtSearch;
    private RJButton btnSearch;
    private Label label27;
    private RJButton btnAddnew;
  }
}
