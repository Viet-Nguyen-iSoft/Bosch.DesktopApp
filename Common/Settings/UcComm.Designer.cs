namespace Common.Settings
{
  partial class UcComm
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
      tableLayoutPanel3 = new TableLayoutPanel();
      lbAutoConnect = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnDelete = new Common.Custom.RJButton();
      btnDetail = new Common.Custom.RJButton();
      tableLayoutPanel1 = new TableLayoutPanel();
      lbCommName = new Label();
      lbInfor = new Label();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.AliceBlue;
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(lbAutoConnect, 0, 2);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 4);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 0);
      tableLayoutPanel3.Controls.Add(lbInfor, 0, 1);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 6;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.Size = new Size(588, 284);
      tableLayoutPanel3.TabIndex = 4;
      // 
      // lbAutoConnect
      // 
      lbAutoConnect.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbAutoConnect.AutoSize = true;
      lbAutoConnect.BackColor = Color.Transparent;
      lbAutoConnect.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lbAutoConnect.Location = new Point(0, 132);
      lbAutoConnect.Margin = new Padding(0);
      lbAutoConnect.Name = "lbAutoConnect";
      lbAutoConnect.Padding = new Padding(10, 0, 0, 0);
      lbAutoConnect.Size = new Size(588, 72);
      lbAutoConnect.TabIndex = 6;
      lbAutoConnect.Text = "Tự động kết nối: Bật";
      lbAutoConnect.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 3;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.Controls.Add(btnDelete, 1, 0);
      tableLayoutPanel2.Controls.Add(btnDetail, 2, 0);
      tableLayoutPanel2.Location = new Point(5, 219);
      tableLayoutPanel2.Margin = new Padding(5, 0, 5, 0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(578, 60);
      tableLayoutPanel2.TabIndex = 4;
      // 
      // btnDelete
      // 
      btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnDelete.BackColor = Color.Tomato;
      btnDelete.BackgroundColor = Color.Tomato;
      btnDelete.BorderColor = Color.PaleVioletRed;
      btnDelete.BorderRadius = 4;
      btnDelete.BorderSize = 0;
      btnDelete.FlatAppearance.BorderSize = 0;
      btnDelete.FlatStyle = FlatStyle.Flat;
      btnDelete.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnDelete.ForeColor = Color.White;
      btnDelete.Image = Properties.Resources.icon_delete;
      btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
      btnDelete.Location = new Point(289, 3);
      btnDelete.Name = "btnDelete";
      btnDelete.Padding = new Padding(10, 0, 0, 0);
      btnDelete.Size = new Size(126, 54);
      btnDelete.TabIndex = 1;
      btnDelete.Text = "        Xóa";
      btnDelete.TextAlign = ContentAlignment.MiddleLeft;
      btnDelete.TextColor = Color.White;
      btnDelete.UseVisualStyleBackColor = false;
      btnDelete.Click += btnDelete_Click;
      // 
      // btnDetail
      // 
      btnDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnDetail.BackColor = Color.FromArgb(51, 108, 181);
      btnDetail.BackgroundColor = Color.FromArgb(51, 108, 181);
      btnDetail.BorderColor = Color.PaleVioletRed;
      btnDetail.BorderRadius = 4;
      btnDetail.BorderSize = 0;
      btnDetail.FlatAppearance.BorderSize = 0;
      btnDetail.FlatStyle = FlatStyle.Flat;
      btnDetail.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnDetail.ForeColor = Color.White;
      btnDetail.Image = Properties.Resources.icon_detail;
      btnDetail.ImageAlign = ContentAlignment.MiddleLeft;
      btnDetail.Location = new Point(421, 3);
      btnDetail.Name = "btnDetail";
      btnDetail.Padding = new Padding(10, 0, 0, 0);
      btnDetail.Size = new Size(154, 54);
      btnDetail.TabIndex = 0;
      btnDetail.Text = "       Chi tiết";
      btnDetail.TextColor = Color.White;
      btnDetail.UseVisualStyleBackColor = false;
      btnDetail.Click += btnDetail_Click;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.7182121F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.2817879F));
      tableLayoutPanel1.Controls.Add(lbCommName, 0, 0);
      tableLayoutPanel1.Location = new Point(3, 3);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel1.Size = new Size(582, 54);
      tableLayoutPanel1.TabIndex = 5;
      // 
      // lbCommName
      // 
      lbCommName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbCommName.AutoSize = true;
      lbCommName.BackColor = Color.Transparent;
      lbCommName.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbCommName.ForeColor = Color.Black;
      lbCommName.Location = new Point(0, 0);
      lbCommName.Margin = new Padding(0);
      lbCommName.Name = "lbCommName";
      lbCommName.Padding = new Padding(10, 0, 0, 0);
      lbCommName.Size = new Size(301, 54);
      lbCommName.TabIndex = 0;
      lbCommName.Text = "Tên kết nối";
      lbCommName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // lbInfor
      // 
      lbInfor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbInfor.AutoSize = true;
      lbInfor.BackColor = Color.Transparent;
      lbInfor.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lbInfor.Location = new Point(0, 60);
      lbInfor.Margin = new Padding(0);
      lbInfor.Name = "lbInfor";
      lbInfor.Padding = new Padding(10, 0, 0, 0);
      lbInfor.Size = new Size(588, 72);
      lbInfor.TabIndex = 1;
      lbInfor.Text = "Chuẩn kết nối";
      lbInfor.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // UcComm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tableLayoutPanel3);
      Name = "UcComm";
      Size = new Size(588, 284);
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label lbCommName;
    private TableLayoutPanel tableLayoutPanel2;
    private Custom.RJButton btnDetail;
    private Label lbInfor;
    private Custom.RJButton btnDelete;
    private TableLayoutPanel tableLayoutPanel1;
    private Label lbAutoConnect;
  }
}
