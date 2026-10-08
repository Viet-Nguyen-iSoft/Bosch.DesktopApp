namespace LTP.Truck.Popup
{
  partial class PopupListPrinter
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupListPrinter));
      tableLayoutPanel3 = new TableLayoutPanel();
      lbTitle = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      flowLayoutPanelPrinter = new FlowLayoutPanel();
      tableLayoutPanelQuantity = new TableLayoutPanel();
      lbPrintCopies = new Label();
      numPrintCopies = new NumericUpDown();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanelQuantity.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)numPrintCopies).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 3);
      tableLayoutPanel3.Controls.Add(flowLayoutPanelPrinter, 0, 1);
      tableLayoutPanel3.Controls.Add(tableLayoutPanelQuantity, 0, 2);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(5);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.Padding = new Padding(5);
      tableLayoutPanel3.RowCount = 5;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.Size = new Size(925, 600);
      tableLayoutPanel3.TabIndex = 5;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.BackColor = Color.FromArgb(199, 199, 199);
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(5, 5);
      lbTitle.Margin = new Padding(0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(915, 60);
      lbTitle.TabIndex = 0;
      lbTitle.Text = "Chọn máy in và số lượng bản in";
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
      tableLayoutPanel2.Location = new Point(5, 530);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(915, 60);
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
      btnConfirm.Location = new Point(558, 3);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(174, 54);
      btnConfirm.TabIndex = 0;
      btnConfirm.Text = "       Xác nhận";
      btnConfirm.TextColor = Color.White;
      btnConfirm.UseVisualStyleBackColor = false;
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
      btnClose.Location = new Point(738, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // flowLayoutPanelPrinter
      // 
      flowLayoutPanelPrinter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      flowLayoutPanelPrinter.Location = new Point(5, 65);
      flowLayoutPanelPrinter.Margin = new Padding(0);
      flowLayoutPanelPrinter.Name = "flowLayoutPanelPrinter";
      flowLayoutPanelPrinter.Size = new Size(915, 395);
      flowLayoutPanelPrinter.TabIndex = 6;
      // 
      // tableLayoutPanelQuantity
      // 
      tableLayoutPanelQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanelQuantity.BackColor = Color.White;
      tableLayoutPanelQuantity.ColumnCount = 2;
      tableLayoutPanelQuantity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanelQuantity.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanelQuantity.Controls.Add(lbPrintCopies, 0, 0);
      tableLayoutPanelQuantity.Controls.Add(numPrintCopies, 1, 0);
      tableLayoutPanelQuantity.Location = new Point(5, 465);
      tableLayoutPanelQuantity.Margin = new Padding(0, 5, 0, 5);
      tableLayoutPanelQuantity.Name = "tableLayoutPanelQuantity";
      tableLayoutPanelQuantity.RowCount = 1;
      tableLayoutPanelQuantity.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanelQuantity.Size = new Size(915, 55);
      tableLayoutPanelQuantity.TabIndex = 7;
      // 
      // lbPrintCopies
      // 
      lbPrintCopies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbPrintCopies.AutoSize = true;
      lbPrintCopies.Font = new Font("Roboto", 14F, FontStyle.Bold);
      lbPrintCopies.Location = new Point(3, 0);
      lbPrintCopies.Name = "lbPrintCopies";
      lbPrintCopies.Padding = new Padding(0, 0, 15, 0);
      lbPrintCopies.Size = new Size(729, 55);
      lbPrintCopies.TabIndex = 0;
      lbPrintCopies.Text = "Số lượng bản in";
      lbPrintCopies.TextAlign = ContentAlignment.MiddleRight;
      // 
      // numPrintCopies
      // 
      numPrintCopies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      numPrintCopies.Font = new Font("Roboto", 14F, FontStyle.Bold);
      numPrintCopies.Location = new Point(745, 10);
      numPrintCopies.Margin = new Padding(10);
      numPrintCopies.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
      numPrintCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      numPrintCopies.Name = "numPrintCopies";
      numPrintCopies.Size = new Size(160, 30);
      numPrintCopies.TabIndex = 1;
      numPrintCopies.TextAlign = HorizontalAlignment.Center;
      numPrintCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
      // 
      // PopupListPrinter
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(925, 600);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupListPrinter";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanelQuantity.ResumeLayout(false);
      tableLayoutPanelQuantity.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)numPrintCopies).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private FlowLayoutPanel flowLayoutPanelPrinter;
    private TableLayoutPanel tableLayoutPanelQuantity;
    private Label lbPrintCopies;
    private NumericUpDown numPrintCopies;
  }
}
