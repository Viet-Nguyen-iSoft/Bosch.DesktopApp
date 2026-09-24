using LTP.Truck.Custom;

namespace LTP.Truck.Popup
{
  partial class PopupLoadMD
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupLoadMD));
      DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel10 = new TableLayoutPanel();
      label4 = new Label();
      txtSearch = new RJTextBox();
      btnSearch = new RJButton();
      lbTitle = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      dgv = new DataGridView();
      btnAdd = new RJButton();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel10.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel10, 0, 1);
      tableLayoutPanel3.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 2);
      tableLayoutPanel3.Controls.Add(dgv, 0, 2);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 5;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.Size = new Size(1507, 731);
      tableLayoutPanel3.TabIndex = 1;
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
      tableLayoutPanel10.Controls.Add(btnSearch, 3, 0);
      tableLayoutPanel10.Controls.Add(btnAdd, 4, 0);
      tableLayoutPanel10.Location = new Point(0, 60);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1507, 55);
      tableLayoutPanel10.TabIndex = 23;
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
      label4.Size = new Size(90, 55);
      label4.TabIndex = 17;
      label4.Text = "Tìm kiếm";
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
      txtSearch.Location = new Point(94, 8);
      txtSearch.Margin = new Padding(4);
      txtSearch.Multiline = false;
      txtSearch.Name = "txtSearch";
      txtSearch.Padding = new Padding(10, 7, 10, 7);
      txtSearch.PasswordChar = false;
      txtSearch.PlaceholderColor = Color.DarkGray;
      txtSearch.PlaceholderText = "";
      txtSearch.Size = new Size(703, 38);
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
      btnSearch.Location = new Point(1109, 3);
      btnSearch.Name = "btnSearch";
      btnSearch.Padding = new Padding(10, 0, 0, 0);
      btnSearch.Size = new Size(194, 49);
      btnSearch.TabIndex = 27;
      btnSearch.Text = "Tìm kiếm";
      btnSearch.TextColor = Color.White;
      btnSearch.UseVisualStyleBackColor = false;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.BackColor = Color.FromArgb(199, 199, 199);
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(0, 0);
      lbTitle.Margin = new Padding(0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(1507, 60);
      lbTitle.TabIndex = 0;
      lbTitle.Text = "Danh sách dữ liệu";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 3;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel2.Controls.Add(btnConfirm, 1, 0);
      tableLayoutPanel2.Controls.Add(btnClose, 2, 0);
      tableLayoutPanel2.Location = new Point(0, 666);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(1507, 60);
      tableLayoutPanel2.TabIndex = 5;
      // 
      // btnConfirm
      // 
      btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnConfirm.BackColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BackgroundColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BorderColor = Color.PaleVioletRed;
      btnConfirm.BorderRadius = 4;
      btnConfirm.BorderSize = 0;
      btnConfirm.FlatAppearance.BorderSize = 0;
      btnConfirm.FlatStyle = FlatStyle.Flat;
      btnConfirm.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnConfirm.ForeColor = Color.White;
      btnConfirm.Image = (Image)resources.GetObject("btnConfirm.Image");
      btnConfirm.ImageAlign = ContentAlignment.MiddleLeft;
      btnConfirm.Location = new Point(1150, 3);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(174, 54);
      btnConfirm.TabIndex = 0;
      btnConfirm.Text = "       Xác nhận";
      btnConfirm.TextColor = Color.White;
      btnConfirm.UseVisualStyleBackColor = false;
      btnConfirm.Click += btnConfirm_Click;
      // 
      // btnClose
      // 
      btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnClose.BackColor = Color.Tomato;
      btnClose.BackgroundColor = Color.Tomato;
      btnClose.BorderColor = Color.PaleVioletRed;
      btnClose.BorderRadius = 4;
      btnClose.BorderSize = 0;
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.FlatStyle = FlatStyle.Flat;
      btnClose.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnClose.ForeColor = Color.White;
      btnClose.Image = (Image)resources.GetObject("btnClose.Image");
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(1330, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      btnClose.Click += btnClose_Click;
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
      dgv.Location = new Point(3, 118);
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
      dgv.Size = new Size(1501, 545);
      dgv.TabIndex = 6;
      // 
      // btnAdd
      // 
      btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnAdd.BackColor = Color.FromArgb(64, 107, 177);
      btnAdd.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnAdd.BorderColor = Color.White;
      btnAdd.BorderRadius = 5;
      btnAdd.BorderSize = 0;
      btnAdd.FlatAppearance.BorderColor = Color.White;
      btnAdd.FlatAppearance.BorderSize = 0;
      btnAdd.FlatStyle = FlatStyle.Flat;
      btnAdd.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnAdd.ForeColor = Color.White;
      btnAdd.Image = Properties.Resources.icon_add_new;
      btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
      btnAdd.Location = new Point(1309, 3);
      btnAdd.Name = "btnAdd";
      btnAdd.Padding = new Padding(10, 0, 0, 0);
      btnAdd.Size = new Size(195, 49);
      btnAdd.TabIndex = 28;
      btnAdd.Text = "Thêm mới";
      btnAdd.TextColor = Color.White;
      btnAdd.UseVisualStyleBackColor = false;
      // 
      // PopupLoadMD
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1507, 731);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupLoadMD";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel10.ResumeLayout(false);
      tableLayoutPanel10.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private DataGridView dgv;
    private TableLayoutPanel tableLayoutPanel10;
    private Label label4;
    private Custom.RJTextBox txtSearch;
    private RJButton btnSearch;
    private RJButton btnAdd;
  }
}