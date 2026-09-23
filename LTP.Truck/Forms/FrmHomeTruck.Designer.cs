

using LTP.Truck.Custom;

namespace LTP.Truck.Forms
{
  partial class FrmHomeTruck
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
      DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
      tableLayoutPanel1 = new TableLayoutPanel();
      tableLayoutPanel2 = new TableLayoutPanel();
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel12 = new TableLayoutPanel();
      tableLayoutPanel17 = new TableLayoutPanel();
      lbWeightTrigger = new Label();
      label2 = new Label();
      tableLayoutPanel18 = new TableLayoutPanel();
      label3 = new Label();
      lbWeightValue = new Label();
      label5 = new Label();
      label1 = new Label();
      tableLayoutPanel6 = new TableLayoutPanel();
      btnWeightTime02 = new RJButton();
      btnWeightTime01 = new RJButton();
      btnTriggerWeight = new RJButton();
      btnBack = new RJButton();
      btnZero = new RJButton();
      btnCreate = new RJButton();
      tableLayoutPanel4 = new TableLayoutPanel();
      tableLayoutPanel8 = new TableLayoutPanel();
      label20 = new Label();
      txtNameDriver = new RJTextBox();
      txtNoLabel = new RJTextBox();
      label19 = new Label();
      tableLayoutPanel15 = new TableLayoutPanel();
      txtWareHouse = new RJTextBox();
      btnLoadWarehouse = new RJButton();
      tableLayoutPanel14 = new TableLayoutPanel();
      txtTypeGoods = new RJTextBox();
      btnLoadTypeGoods = new RJButton();
      tableLayoutPanel13 = new TableLayoutPanel();
      txtClient = new RJTextBox();
      btnLoadClient = new RJButton();
      label6 = new Label();
      label8 = new Label();
      label9 = new Label();
      label11 = new Label();
      tableLayoutPanel11 = new TableLayoutPanel();
      txtTypeWeight = new RJTextBox();
      label12 = new Label();
      txtNoLabelAuto = new RJTextBox();
      label16 = new Label();
      label7 = new Label();
      tableLayoutPanel16 = new TableLayoutPanel();
      txtLicensePlate = new RJTextBox();
      label13 = new Label();
      txtIdCard = new RJTextBox();
      txtDocument = new TextBox();
      label10 = new Label();
      tableLayoutPanel9 = new TableLayoutPanel();
      ucItemOffsetWeight = new LTP.Truck.UserControls.UcItem();
      label14 = new Label();
      ucItemWeight01 = new LTP.Truck.UserControls.UcItem();
      ucItemWeight02 = new LTP.Truck.UserControls.UcItem();
      ucItemWeightGoods = new LTP.Truck.UserControls.UcItem();
      tableLayoutPanel7 = new TableLayoutPanel();
      dgv = new DataGridView();
      tableLayoutPanel10 = new TableLayoutPanel();
      label4 = new Label();
      txtSearchKey = new RJTextBox();
      label17 = new Label();
      btnPrint = new RJButton();
      label18 = new Label();
      btnSearchHistorical = new RJButton();
      ucTimeSearchFrom = new LTP.Truck.UserControls.UcTimeSearch();
      ucTimeSearchTo = new LTP.Truck.UserControls.UcTimeSearch();
      btnFilter = new Common.Custom.RJButton();
      label27 = new Label();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel12.SuspendLayout();
      tableLayoutPanel17.SuspendLayout();
      tableLayoutPanel18.SuspendLayout();
      tableLayoutPanel6.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
      tableLayoutPanel8.SuspendLayout();
      tableLayoutPanel15.SuspendLayout();
      tableLayoutPanel14.SuspendLayout();
      tableLayoutPanel13.SuspendLayout();
      tableLayoutPanel11.SuspendLayout();
      tableLayoutPanel16.SuspendLayout();
      tableLayoutPanel9.SuspendLayout();
      tableLayoutPanel7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
      tableLayoutPanel10.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.White;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel7, 0, 2);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 4;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.Size = new Size(1530, 930);
      tableLayoutPanel1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 5;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
      tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 2, 0);
      tableLayoutPanel2.Controls.Add(tableLayoutPanel9, 4, 0);
      tableLayoutPanel2.Location = new Point(0, 0);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(1525, 506);
      tableLayoutPanel2.TabIndex = 0;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel12, 0, 2);
      tableLayoutPanel3.Controls.Add(label5, 0, 1);
      tableLayoutPanel3.Controls.Add(label1, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 0, 4);
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 6;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 125F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.Size = new Size(723, 506);
      tableLayoutPanel3.TabIndex = 0;
      // 
      // tableLayoutPanel12
      // 
      tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel12.ColumnCount = 1;
      tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel12.Controls.Add(tableLayoutPanel17, 0, 1);
      tableLayoutPanel12.Controls.Add(tableLayoutPanel18, 0, 0);
      tableLayoutPanel12.Location = new Point(0, 100);
      tableLayoutPanel12.Margin = new Padding(0);
      tableLayoutPanel12.Name = "tableLayoutPanel12";
      tableLayoutPanel12.RowCount = 2;
      tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel12.Size = new Size(723, 271);
      tableLayoutPanel12.TabIndex = 4;
      // 
      // tableLayoutPanel17
      // 
      tableLayoutPanel17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel17.ColumnCount = 2;
      tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel17.Controls.Add(lbWeightTrigger, 1, 0);
      tableLayoutPanel17.Controls.Add(label2, 0, 0);
      tableLayoutPanel17.Location = new Point(0, 221);
      tableLayoutPanel17.Margin = new Padding(0);
      tableLayoutPanel17.Name = "tableLayoutPanel17";
      tableLayoutPanel17.RowCount = 1;
      tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel17.Size = new Size(723, 50);
      tableLayoutPanel17.TabIndex = 3;
      // 
      // lbWeightTrigger
      // 
      lbWeightTrigger.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbWeightTrigger.AutoSize = true;
      lbWeightTrigger.BackColor = Color.Transparent;
      lbWeightTrigger.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbWeightTrigger.Location = new Point(252, 0);
      lbWeightTrigger.Margin = new Padding(0);
      lbWeightTrigger.Name = "lbWeightTrigger";
      lbWeightTrigger.Size = new Size(471, 50);
      lbWeightTrigger.TabIndex = 4;
      lbWeightTrigger.Text = "0.000";
      lbWeightTrigger.TextAlign = ContentAlignment.BottomLeft;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(252, 50);
      label2.TabIndex = 3;
      label2.Text = "Giá trị cân xác nhận(Kg):";
      label2.TextAlign = ContentAlignment.BottomLeft;
      // 
      // tableLayoutPanel18
      // 
      tableLayoutPanel18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel18.ColumnCount = 2;
      tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel18.Controls.Add(label3, 1, 0);
      tableLayoutPanel18.Controls.Add(lbWeightValue, 0, 0);
      tableLayoutPanel18.Location = new Point(0, 0);
      tableLayoutPanel18.Margin = new Padding(0);
      tableLayoutPanel18.Name = "tableLayoutPanel18";
      tableLayoutPanel18.RowCount = 1;
      tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel18.Size = new Size(723, 221);
      tableLayoutPanel18.TabIndex = 4;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.BackColor = Color.Transparent;
      label3.Font = new Font("Roboto", 39.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label3.Location = new Point(633, 0);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(90, 221);
      label3.TabIndex = 3;
      label3.Text = "Kg";
      label3.TextAlign = ContentAlignment.BottomLeft;
      // 
      // lbWeightValue
      // 
      lbWeightValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbWeightValue.AutoSize = true;
      lbWeightValue.BackColor = Color.Transparent;
      lbWeightValue.Font = new Font("Roboto", 90F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbWeightValue.Location = new Point(0, 0);
      lbWeightValue.Margin = new Padding(0);
      lbWeightValue.Name = "lbWeightValue";
      lbWeightValue.Size = new Size(633, 221);
      lbWeightValue.TabIndex = 2;
      lbWeightValue.Text = "---";
      lbWeightValue.TextAlign = ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 50);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(723, 50);
      label5.TabIndex = 2;
      label5.Text = "Trạng thái cân";
      label5.TextAlign = ContentAlignment.MiddleRight;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.FromArgb(199, 199, 199);
      label1.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(723, 50);
      label1.TabIndex = 0;
      label1.Text = "Thông tin cân";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel6
      // 
      tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel6.ColumnCount = 3;
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.Controls.Add(btnWeightTime02, 2, 0);
      tableLayoutPanel6.Controls.Add(btnWeightTime01, 1, 0);
      tableLayoutPanel6.Controls.Add(btnTriggerWeight, 0, 0);
      tableLayoutPanel6.Controls.Add(btnBack, 0, 1);
      tableLayoutPanel6.Controls.Add(btnZero, 1, 1);
      tableLayoutPanel6.Controls.Add(btnCreate, 2, 1);
      tableLayoutPanel6.Location = new Point(0, 376);
      tableLayoutPanel6.Margin = new Padding(0);
      tableLayoutPanel6.Name = "tableLayoutPanel6";
      tableLayoutPanel6.Padding = new Padding(5, 0, 5, 0);
      tableLayoutPanel6.RowCount = 2;
      tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel6.Size = new Size(723, 125);
      tableLayoutPanel6.TabIndex = 2;
      // 
      // btnWeightTime02
      // 
      btnWeightTime02.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnWeightTime02.BackColor = Color.FromArgb(64, 107, 177);
      btnWeightTime02.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnWeightTime02.BorderColor = Color.White;
      btnWeightTime02.BorderRadius = 5;
      btnWeightTime02.BorderSize = 0;
      btnWeightTime02.FlatAppearance.BorderColor = Color.White;
      btnWeightTime02.FlatAppearance.BorderSize = 0;
      btnWeightTime02.FlatStyle = FlatStyle.Flat;
      btnWeightTime02.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnWeightTime02.ForeColor = Color.White;
      btnWeightTime02.Image = Properties.Resources.icon_weight;
      btnWeightTime02.ImageAlign = ContentAlignment.MiddleLeft;
      btnWeightTime02.Location = new Point(482, 3);
      btnWeightTime02.Name = "btnWeightTime02";
      btnWeightTime02.Padding = new Padding(10, 0, 0, 0);
      btnWeightTime02.Size = new Size(233, 56);
      btnWeightTime02.TabIndex = 19;
      btnWeightTime02.Text = "Cân lần 02";
      btnWeightTime02.TextColor = Color.White;
      btnWeightTime02.UseVisualStyleBackColor = false;
      btnWeightTime02.Click += btnWeightTime02_Click;
      // 
      // btnWeightTime01
      // 
      btnWeightTime01.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnWeightTime01.BackColor = Color.FromArgb(64, 107, 177);
      btnWeightTime01.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnWeightTime01.BorderColor = Color.White;
      btnWeightTime01.BorderRadius = 5;
      btnWeightTime01.BorderSize = 0;
      btnWeightTime01.FlatAppearance.BorderColor = Color.White;
      btnWeightTime01.FlatAppearance.BorderSize = 0;
      btnWeightTime01.FlatStyle = FlatStyle.Flat;
      btnWeightTime01.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnWeightTime01.ForeColor = Color.White;
      btnWeightTime01.Image = Properties.Resources.icon_weight;
      btnWeightTime01.ImageAlign = ContentAlignment.MiddleLeft;
      btnWeightTime01.Location = new Point(245, 3);
      btnWeightTime01.Name = "btnWeightTime01";
      btnWeightTime01.Padding = new Padding(10, 0, 0, 0);
      btnWeightTime01.Size = new Size(231, 56);
      btnWeightTime01.TabIndex = 18;
      btnWeightTime01.Text = "Cân lần 01";
      btnWeightTime01.TextColor = Color.White;
      btnWeightTime01.UseVisualStyleBackColor = false;
      btnWeightTime01.Click += btnWeightTime01_Click;
      // 
      // btnTriggerWeight
      // 
      btnTriggerWeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnTriggerWeight.BackColor = Color.FromArgb(64, 107, 177);
      btnTriggerWeight.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnTriggerWeight.BorderColor = Color.White;
      btnTriggerWeight.BorderRadius = 5;
      btnTriggerWeight.BorderSize = 0;
      btnTriggerWeight.FlatAppearance.BorderColor = Color.White;
      btnTriggerWeight.FlatAppearance.BorderSize = 0;
      btnTriggerWeight.FlatStyle = FlatStyle.Flat;
      btnTriggerWeight.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnTriggerWeight.ForeColor = Color.White;
      btnTriggerWeight.Image = Properties.Resources.icon_weight_log;
      btnTriggerWeight.ImageAlign = ContentAlignment.MiddleLeft;
      btnTriggerWeight.Location = new Point(8, 3);
      btnTriggerWeight.Name = "btnTriggerWeight";
      btnTriggerWeight.Padding = new Padding(10, 0, 0, 0);
      btnTriggerWeight.Size = new Size(231, 55);
      btnTriggerWeight.TabIndex = 17;
      btnTriggerWeight.Text = "Xác nhận cân";
      btnTriggerWeight.TextColor = Color.White;
      btnTriggerWeight.UseVisualStyleBackColor = false;
      btnTriggerWeight.Click += btnTriggerWeight_Click;
      // 
      // btnBack
      // 
      btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnBack.BackColor = Color.FromArgb(64, 107, 177);
      btnBack.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnBack.BorderColor = Color.White;
      btnBack.BorderRadius = 5;
      btnBack.BorderSize = 0;
      btnBack.FlatAppearance.BorderColor = Color.White;
      btnBack.FlatAppearance.BorderSize = 0;
      btnBack.FlatStyle = FlatStyle.Flat;
      btnBack.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnBack.ForeColor = Color.White;
      btnBack.Image = Properties.Resources.icon_back;
      btnBack.ImageAlign = ContentAlignment.MiddleLeft;
      btnBack.Location = new Point(8, 65);
      btnBack.Name = "btnBack";
      btnBack.Padding = new Padding(10, 0, 0, 0);
      btnBack.Size = new Size(231, 57);
      btnBack.TabIndex = 21;
      btnBack.Text = "Quay lại";
      btnBack.TextColor = Color.White;
      btnBack.UseVisualStyleBackColor = false;
      btnBack.Click += btnBack_Click;
      // 
      // btnZero
      // 
      btnZero.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnZero.BackColor = Color.FromArgb(64, 107, 177);
      btnZero.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnZero.BorderColor = Color.White;
      btnZero.BorderRadius = 5;
      btnZero.BorderSize = 0;
      btnZero.FlatAppearance.BorderColor = Color.White;
      btnZero.FlatAppearance.BorderSize = 0;
      btnZero.FlatStyle = FlatStyle.Flat;
      btnZero.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnZero.ForeColor = Color.White;
      btnZero.Image = Properties.Resources.icon_zero;
      btnZero.ImageAlign = ContentAlignment.MiddleLeft;
      btnZero.Location = new Point(245, 65);
      btnZero.Name = "btnZero";
      btnZero.Padding = new Padding(10, 0, 0, 0);
      btnZero.Size = new Size(231, 57);
      btnZero.TabIndex = 22;
      btnZero.Text = "Zero";
      btnZero.TextColor = Color.White;
      btnZero.UseVisualStyleBackColor = false;
      btnZero.Click += btnZero_Click;
      // 
      // btnCreate
      // 
      btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnCreate.BackColor = Color.FromArgb(64, 107, 177);
      btnCreate.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnCreate.BorderColor = Color.White;
      btnCreate.BorderRadius = 5;
      btnCreate.BorderSize = 0;
      btnCreate.FlatAppearance.BorderColor = Color.White;
      btnCreate.FlatAppearance.BorderSize = 0;
      btnCreate.FlatStyle = FlatStyle.Flat;
      btnCreate.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnCreate.ForeColor = Color.White;
      btnCreate.Image = Properties.Resources.icon_new;
      btnCreate.ImageAlign = ContentAlignment.MiddleLeft;
      btnCreate.Location = new Point(482, 65);
      btnCreate.Name = "btnCreate";
      btnCreate.Padding = new Padding(15, 0, 0, 0);
      btnCreate.Size = new Size(233, 57);
      btnCreate.TabIndex = 23;
      btnCreate.Text = "Phiếu mới";
      btnCreate.TextColor = Color.White;
      btnCreate.UseVisualStyleBackColor = false;
      btnCreate.Click += btnCreate_Click;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel4.ColumnCount = 1;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Controls.Add(tableLayoutPanel8, 0, 1);
      tableLayoutPanel4.Controls.Add(label10, 0, 0);
      tableLayoutPanel4.Location = new Point(728, 0);
      tableLayoutPanel4.Margin = new Padding(0);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 2;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.Size = new Size(591, 506);
      tableLayoutPanel4.TabIndex = 1;
      // 
      // tableLayoutPanel8
      // 
      tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel8.ColumnCount = 2;
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel8.Controls.Add(label20, 0, 1);
      tableLayoutPanel8.Controls.Add(txtNameDriver, 1, 5);
      tableLayoutPanel8.Controls.Add(txtNoLabel, 1, 1);
      tableLayoutPanel8.Controls.Add(label19, 0, 8);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel15, 1, 4);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel14, 1, 3);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel13, 1, 2);
      tableLayoutPanel8.Controls.Add(label6, 0, 0);
      tableLayoutPanel8.Controls.Add(label8, 0, 2);
      tableLayoutPanel8.Controls.Add(label9, 0, 3);
      tableLayoutPanel8.Controls.Add(label11, 0, 4);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel11, 1, 0);
      tableLayoutPanel8.Controls.Add(label16, 0, 5);
      tableLayoutPanel8.Controls.Add(label7, 0, 6);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel16, 1, 6);
      tableLayoutPanel8.Controls.Add(txtDocument, 1, 8);
      tableLayoutPanel8.Location = new Point(3, 53);
      tableLayoutPanel8.Name = "tableLayoutPanel8";
      tableLayoutPanel8.RowCount = 9;
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285718F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285718F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857132F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
      tableLayoutPanel8.Size = new Size(585, 450);
      tableLayoutPanel8.TabIndex = 2;
      // 
      // label20
      // 
      label20.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label20.AutoSize = true;
      label20.BackColor = Color.Transparent;
      label20.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label20.Location = new Point(0, 46);
      label20.Margin = new Padding(0);
      label20.Name = "label20";
      label20.Size = new Size(159, 46);
      label20.TabIndex = 24;
      label20.Text = "Phiếu nhà máy:";
      label20.TextAlign = ContentAlignment.MiddleRight;
      // 
      // txtNameDriver
      // 
      txtNameDriver.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNameDriver.BackColor = SystemColors.Window;
      txtNameDriver.BorderColor = Color.Black;
      txtNameDriver.BorderFocusColor = Color.HotPink;
      txtNameDriver.BorderRadius = 5;
      txtNameDriver.BorderSize = 2;
      txtNameDriver.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNameDriver.ForeColor = Color.FromArgb(64, 64, 64);
      txtNameDriver.Location = new Point(163, 234);
      txtNameDriver.Margin = new Padding(4);
      txtNameDriver.Multiline = false;
      txtNameDriver.Name = "txtNameDriver";
      txtNameDriver.Padding = new Padding(10, 7, 10, 7);
      txtNameDriver.PasswordChar = false;
      txtNameDriver.PlaceholderColor = Color.DarkGray;
      txtNameDriver.PlaceholderText = "";
      txtNameDriver.Size = new Size(418, 42);
      txtNameDriver.TabIndex = 15;
      txtNameDriver.Texts = "";
      txtNameDriver.UnderlinedStyle = false;
      // 
      // txtNoLabel
      // 
      txtNoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNoLabel.BackColor = SystemColors.Window;
      txtNoLabel.BorderColor = Color.Black;
      txtNoLabel.BorderFocusColor = Color.HotPink;
      txtNoLabel.BorderRadius = 5;
      txtNoLabel.BorderSize = 2;
      txtNoLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNoLabel.ForeColor = Color.FromArgb(64, 64, 64);
      txtNoLabel.Location = new Point(163, 50);
      txtNoLabel.Margin = new Padding(4);
      txtNoLabel.Multiline = false;
      txtNoLabel.Name = "txtNoLabel";
      txtNoLabel.Padding = new Padding(10, 7, 10, 7);
      txtNoLabel.PasswordChar = false;
      txtNoLabel.PlaceholderColor = Color.DarkGray;
      txtNoLabel.PlaceholderText = "";
      txtNoLabel.Size = new Size(418, 42);
      txtNoLabel.TabIndex = 16;
      txtNoLabel.Texts = "";
      txtNoLabel.UnderlinedStyle = false;
      // 
      // label19
      // 
      label19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label19.AutoSize = true;
      label19.BackColor = Color.Transparent;
      label19.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label19.Location = new Point(0, 327);
      label19.Margin = new Padding(0);
      label19.Name = "label19";
      label19.Size = new Size(159, 123);
      label19.TabIndex = 17;
      label19.Text = "Ghi chú:";
      // 
      // tableLayoutPanel15
      // 
      tableLayoutPanel15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel15.ColumnCount = 2;
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel15.Controls.Add(txtWareHouse, 0, 0);
      tableLayoutPanel15.Controls.Add(btnLoadWarehouse, 1, 0);
      tableLayoutPanel15.Location = new Point(159, 184);
      tableLayoutPanel15.Margin = new Padding(0);
      tableLayoutPanel15.Name = "tableLayoutPanel15";
      tableLayoutPanel15.RowCount = 1;
      tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel15.Size = new Size(426, 46);
      tableLayoutPanel15.TabIndex = 20;
      // 
      // txtWareHouse
      // 
      txtWareHouse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtWareHouse.BackColor = SystemColors.Window;
      txtWareHouse.BorderColor = Color.Black;
      txtWareHouse.BorderFocusColor = Color.HotPink;
      txtWareHouse.BorderRadius = 5;
      txtWareHouse.BorderSize = 2;
      txtWareHouse.Enabled = false;
      txtWareHouse.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtWareHouse.ForeColor = Color.FromArgb(64, 64, 64);
      txtWareHouse.Location = new Point(4, 4);
      txtWareHouse.Margin = new Padding(4);
      txtWareHouse.Multiline = false;
      txtWareHouse.Name = "txtWareHouse";
      txtWareHouse.Padding = new Padding(10, 7, 10, 7);
      txtWareHouse.PasswordChar = false;
      txtWareHouse.PlaceholderColor = Color.DarkGray;
      txtWareHouse.PlaceholderText = "";
      txtWareHouse.Size = new Size(358, 42);
      txtWareHouse.TabIndex = 15;
      txtWareHouse.Texts = "";
      txtWareHouse.UnderlinedStyle = false;
      // 
      // btnLoadWarehouse
      // 
      btnLoadWarehouse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadWarehouse.BackColor = Color.White;
      btnLoadWarehouse.BackgroundColor = Color.White;
      btnLoadWarehouse.BorderColor = Color.Black;
      btnLoadWarehouse.BorderRadius = 5;
      btnLoadWarehouse.BorderSize = 0;
      btnLoadWarehouse.FlatAppearance.BorderColor = Color.Black;
      btnLoadWarehouse.FlatAppearance.BorderSize = 3;
      btnLoadWarehouse.FlatStyle = FlatStyle.Flat;
      btnLoadWarehouse.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadWarehouse.ForeColor = Color.Black;
      btnLoadWarehouse.Location = new Point(369, 4);
      btnLoadWarehouse.Name = "btnLoadWarehouse";
      btnLoadWarehouse.Size = new Size(54, 38);
      btnLoadWarehouse.TabIndex = 16;
      btnLoadWarehouse.Text = "...";
      btnLoadWarehouse.TextColor = Color.Black;
      btnLoadWarehouse.UseVisualStyleBackColor = false;
      btnLoadWarehouse.Click += btnLoadWarehouse_Click;
      // 
      // tableLayoutPanel14
      // 
      tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel14.ColumnCount = 2;
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel14.Controls.Add(txtTypeGoods, 0, 0);
      tableLayoutPanel14.Controls.Add(btnLoadTypeGoods, 1, 0);
      tableLayoutPanel14.Location = new Point(159, 138);
      tableLayoutPanel14.Margin = new Padding(0);
      tableLayoutPanel14.Name = "tableLayoutPanel14";
      tableLayoutPanel14.RowCount = 1;
      tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel14.Size = new Size(426, 46);
      tableLayoutPanel14.TabIndex = 19;
      // 
      // txtTypeGoods
      // 
      txtTypeGoods.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtTypeGoods.BackColor = SystemColors.Window;
      txtTypeGoods.BorderColor = Color.Black;
      txtTypeGoods.BorderFocusColor = Color.HotPink;
      txtTypeGoods.BorderRadius = 5;
      txtTypeGoods.BorderSize = 2;
      txtTypeGoods.Enabled = false;
      txtTypeGoods.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtTypeGoods.ForeColor = Color.FromArgb(64, 64, 64);
      txtTypeGoods.Location = new Point(4, 4);
      txtTypeGoods.Margin = new Padding(4);
      txtTypeGoods.Multiline = false;
      txtTypeGoods.Name = "txtTypeGoods";
      txtTypeGoods.Padding = new Padding(10, 7, 10, 7);
      txtTypeGoods.PasswordChar = false;
      txtTypeGoods.PlaceholderColor = Color.DarkGray;
      txtTypeGoods.PlaceholderText = "";
      txtTypeGoods.Size = new Size(358, 42);
      txtTypeGoods.TabIndex = 15;
      txtTypeGoods.Texts = "";
      txtTypeGoods.UnderlinedStyle = false;
      // 
      // btnLoadTypeGoods
      // 
      btnLoadTypeGoods.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadTypeGoods.BackColor = Color.White;
      btnLoadTypeGoods.BackgroundColor = Color.White;
      btnLoadTypeGoods.BorderColor = Color.Black;
      btnLoadTypeGoods.BorderRadius = 5;
      btnLoadTypeGoods.BorderSize = 0;
      btnLoadTypeGoods.FlatAppearance.BorderColor = Color.Black;
      btnLoadTypeGoods.FlatAppearance.BorderSize = 3;
      btnLoadTypeGoods.FlatStyle = FlatStyle.Flat;
      btnLoadTypeGoods.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadTypeGoods.ForeColor = Color.Black;
      btnLoadTypeGoods.Location = new Point(369, 4);
      btnLoadTypeGoods.Name = "btnLoadTypeGoods";
      btnLoadTypeGoods.Size = new Size(54, 38);
      btnLoadTypeGoods.TabIndex = 16;
      btnLoadTypeGoods.Text = "...";
      btnLoadTypeGoods.TextColor = Color.Black;
      btnLoadTypeGoods.UseVisualStyleBackColor = false;
      btnLoadTypeGoods.Click += btnLoadTypeGoods_Click;
      // 
      // tableLayoutPanel13
      // 
      tableLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel13.ColumnCount = 2;
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel13.Controls.Add(txtClient, 0, 0);
      tableLayoutPanel13.Controls.Add(btnLoadClient, 1, 0);
      tableLayoutPanel13.Location = new Point(159, 92);
      tableLayoutPanel13.Margin = new Padding(0);
      tableLayoutPanel13.Name = "tableLayoutPanel13";
      tableLayoutPanel13.RowCount = 1;
      tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel13.Size = new Size(426, 46);
      tableLayoutPanel13.TabIndex = 18;
      // 
      // txtClient
      // 
      txtClient.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtClient.BackColor = SystemColors.Window;
      txtClient.BorderColor = Color.Black;
      txtClient.BorderFocusColor = Color.HotPink;
      txtClient.BorderRadius = 5;
      txtClient.BorderSize = 2;
      txtClient.Enabled = false;
      txtClient.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtClient.ForeColor = Color.FromArgb(64, 64, 64);
      txtClient.Location = new Point(4, 4);
      txtClient.Margin = new Padding(4);
      txtClient.Multiline = false;
      txtClient.Name = "txtClient";
      txtClient.Padding = new Padding(10, 7, 10, 7);
      txtClient.PasswordChar = false;
      txtClient.PlaceholderColor = Color.DarkGray;
      txtClient.PlaceholderText = "";
      txtClient.Size = new Size(358, 42);
      txtClient.TabIndex = 15;
      txtClient.Texts = "";
      txtClient.UnderlinedStyle = false;
      // 
      // btnLoadClient
      // 
      btnLoadClient.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadClient.BackColor = Color.White;
      btnLoadClient.BackgroundColor = Color.White;
      btnLoadClient.BorderColor = Color.Black;
      btnLoadClient.BorderRadius = 5;
      btnLoadClient.BorderSize = 0;
      btnLoadClient.FlatAppearance.BorderColor = Color.Black;
      btnLoadClient.FlatAppearance.BorderSize = 3;
      btnLoadClient.FlatStyle = FlatStyle.Flat;
      btnLoadClient.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadClient.ForeColor = Color.Black;
      btnLoadClient.Location = new Point(369, 4);
      btnLoadClient.Name = "btnLoadClient";
      btnLoadClient.Size = new Size(54, 38);
      btnLoadClient.TabIndex = 16;
      btnLoadClient.Text = "...";
      btnLoadClient.TextColor = Color.Black;
      btnLoadClient.UseVisualStyleBackColor = false;
      btnLoadClient.Click += btnLoadClient_Click;
      // 
      // label6
      // 
      label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label6.AutoSize = true;
      label6.BackColor = Color.Transparent;
      label6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label6.Location = new Point(0, 0);
      label6.Margin = new Padding(0);
      label6.Name = "label6";
      label6.Size = new Size(159, 46);
      label6.TabIndex = 1;
      label6.Text = "Số phiếu:";
      label6.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label8.AutoSize = true;
      label8.BackColor = Color.Transparent;
      label8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label8.Location = new Point(0, 92);
      label8.Margin = new Padding(0);
      label8.Name = "label8";
      label8.Size = new Size(159, 46);
      label8.TabIndex = 3;
      label8.Text = "Khách hàng:";
      label8.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label9
      // 
      label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label9.AutoSize = true;
      label9.BackColor = Color.Transparent;
      label9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label9.Location = new Point(0, 138);
      label9.Margin = new Padding(0);
      label9.Name = "label9";
      label9.Size = new Size(159, 46);
      label9.TabIndex = 4;
      label9.Text = "Loại hàng:";
      label9.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label11
      // 
      label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label11.AutoSize = true;
      label11.BackColor = Color.Transparent;
      label11.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label11.Location = new Point(0, 184);
      label11.Margin = new Padding(0);
      label11.Name = "label11";
      label11.Size = new Size(159, 46);
      label11.TabIndex = 5;
      label11.Text = "Kho hàng:";
      label11.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel11
      // 
      tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel11.ColumnCount = 4;
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel11.Controls.Add(txtTypeWeight, 3, 0);
      tableLayoutPanel11.Controls.Add(label12, 2, 0);
      tableLayoutPanel11.Controls.Add(txtNoLabelAuto, 0, 0);
      tableLayoutPanel11.Location = new Point(159, 0);
      tableLayoutPanel11.Margin = new Padding(0);
      tableLayoutPanel11.Name = "tableLayoutPanel11";
      tableLayoutPanel11.RowCount = 1;
      tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.Size = new Size(426, 46);
      tableLayoutPanel11.TabIndex = 16;
      // 
      // txtTypeWeight
      // 
      txtTypeWeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtTypeWeight.BackColor = SystemColors.Window;
      txtTypeWeight.BorderColor = Color.Black;
      txtTypeWeight.BorderFocusColor = Color.HotPink;
      txtTypeWeight.BorderRadius = 5;
      txtTypeWeight.BorderSize = 2;
      txtTypeWeight.Enabled = false;
      txtTypeWeight.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtTypeWeight.ForeColor = Color.FromArgb(64, 64, 64);
      txtTypeWeight.Location = new Point(230, 4);
      txtTypeWeight.Margin = new Padding(4);
      txtTypeWeight.Multiline = false;
      txtTypeWeight.Name = "txtTypeWeight";
      txtTypeWeight.Padding = new Padding(10, 7, 10, 7);
      txtTypeWeight.PasswordChar = false;
      txtTypeWeight.PlaceholderColor = Color.DarkGray;
      txtTypeWeight.PlaceholderText = "";
      txtTypeWeight.Size = new Size(192, 42);
      txtTypeWeight.TabIndex = 22;
      txtTypeWeight.Texts = "";
      txtTypeWeight.UnderlinedStyle = false;
      // 
      // label12
      // 
      label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label12.AutoSize = true;
      label12.BackColor = Color.Transparent;
      label12.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label12.Location = new Point(125, 0);
      label12.Margin = new Padding(0);
      label12.Name = "label12";
      label12.Size = new Size(101, 46);
      label12.TabIndex = 17;
      label12.Text = "Kiểu cân:";
      label12.TextAlign = ContentAlignment.MiddleRight;
      // 
      // txtNoLabelAuto
      // 
      txtNoLabelAuto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNoLabelAuto.BackColor = SystemColors.Window;
      txtNoLabelAuto.BorderColor = Color.Black;
      txtNoLabelAuto.BorderFocusColor = Color.HotPink;
      txtNoLabelAuto.BorderRadius = 5;
      txtNoLabelAuto.BorderSize = 2;
      txtNoLabelAuto.Enabled = false;
      txtNoLabelAuto.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNoLabelAuto.ForeColor = Color.FromArgb(64, 64, 64);
      txtNoLabelAuto.Location = new Point(4, 4);
      txtNoLabelAuto.Margin = new Padding(4);
      txtNoLabelAuto.Multiline = false;
      txtNoLabelAuto.Name = "txtNoLabelAuto";
      txtNoLabelAuto.Padding = new Padding(10, 7, 10, 7);
      txtNoLabelAuto.PasswordChar = false;
      txtNoLabelAuto.PlaceholderColor = Color.DarkGray;
      txtNoLabelAuto.PlaceholderText = "";
      txtNoLabelAuto.Size = new Size(97, 42);
      txtNoLabelAuto.TabIndex = 15;
      txtNoLabelAuto.Texts = "";
      txtNoLabelAuto.UnderlinedStyle = false;
      // 
      // label16
      // 
      label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label16.AutoSize = true;
      label16.BackColor = Color.Transparent;
      label16.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label16.Location = new Point(0, 230);
      label16.Margin = new Padding(0);
      label16.Name = "label16";
      label16.Size = new Size(159, 46);
      label16.TabIndex = 12;
      label16.Text = "Tên lái xe:";
      label16.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label7
      // 
      label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label7.AutoSize = true;
      label7.BackColor = Color.Transparent;
      label7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label7.Location = new Point(0, 276);
      label7.Margin = new Padding(0);
      label7.Name = "label7";
      label7.Size = new Size(159, 46);
      label7.TabIndex = 2;
      label7.Text = "Biển số:";
      label7.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel16
      // 
      tableLayoutPanel16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel16.ColumnCount = 4;
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel16.Controls.Add(txtLicensePlate, 0, 0);
      tableLayoutPanel16.Controls.Add(label13, 2, 0);
      tableLayoutPanel16.Controls.Add(txtIdCard, 3, 0);
      tableLayoutPanel16.Location = new Point(159, 276);
      tableLayoutPanel16.Margin = new Padding(0);
      tableLayoutPanel16.Name = "tableLayoutPanel16";
      tableLayoutPanel16.RowCount = 1;
      tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel16.Size = new Size(426, 46);
      tableLayoutPanel16.TabIndex = 21;
      // 
      // txtLicensePlate
      // 
      txtLicensePlate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtLicensePlate.BackColor = SystemColors.Window;
      txtLicensePlate.BorderColor = Color.Black;
      txtLicensePlate.BorderFocusColor = Color.HotPink;
      txtLicensePlate.BorderRadius = 5;
      txtLicensePlate.BorderSize = 2;
      txtLicensePlate.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtLicensePlate.ForeColor = Color.FromArgb(64, 64, 64);
      txtLicensePlate.Location = new Point(4, 4);
      txtLicensePlate.Margin = new Padding(4);
      txtLicensePlate.Multiline = false;
      txtLicensePlate.Name = "txtLicensePlate";
      txtLicensePlate.Padding = new Padding(10, 7, 10, 7);
      txtLicensePlate.PasswordChar = false;
      txtLicensePlate.PlaceholderColor = Color.DarkGray;
      txtLicensePlate.PlaceholderText = "";
      txtLicensePlate.Size = new Size(192, 42);
      txtLicensePlate.TabIndex = 15;
      txtLicensePlate.Texts = "";
      txtLicensePlate.UnderlinedStyle = false;
      // 
      // label13
      // 
      label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label13.AutoSize = true;
      label13.BackColor = Color.Transparent;
      label13.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label13.Location = new Point(220, 0);
      label13.Margin = new Padding(0);
      label13.Name = "label13";
      label13.Size = new Size(73, 46);
      label13.TabIndex = 17;
      label13.Text = "CCCD:";
      label13.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtIdCard
      // 
      txtIdCard.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtIdCard.BackColor = SystemColors.Window;
      txtIdCard.BorderColor = Color.Black;
      txtIdCard.BorderFocusColor = Color.HotPink;
      txtIdCard.BorderRadius = 5;
      txtIdCard.BorderSize = 2;
      txtIdCard.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtIdCard.ForeColor = Color.FromArgb(64, 64, 64);
      txtIdCard.Location = new Point(297, 4);
      txtIdCard.Margin = new Padding(4);
      txtIdCard.Multiline = false;
      txtIdCard.Name = "txtIdCard";
      txtIdCard.Padding = new Padding(10, 7, 10, 7);
      txtIdCard.PasswordChar = false;
      txtIdCard.PlaceholderColor = Color.DarkGray;
      txtIdCard.PlaceholderText = "";
      txtIdCard.Size = new Size(125, 42);
      txtIdCard.TabIndex = 18;
      txtIdCard.Texts = "";
      txtIdCard.UnderlinedStyle = false;
      // 
      // txtDocument
      // 
      txtDocument.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      txtDocument.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtDocument.Location = new Point(162, 330);
      txtDocument.Multiline = true;
      txtDocument.Name = "txtDocument";
      txtDocument.Size = new Size(420, 117);
      txtDocument.TabIndex = 25;
      // 
      // label10
      // 
      label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label10.AutoSize = true;
      label10.BackColor = Color.FromArgb(199, 199, 199);
      label10.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label10.Location = new Point(0, 0);
      label10.Margin = new Padding(0);
      label10.Name = "label10";
      label10.Size = new Size(591, 50);
      label10.TabIndex = 0;
      label10.Text = "Thông tin phiếu cân";
      label10.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel9
      // 
      tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel9.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel9.ColumnCount = 1;
      tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel9.Controls.Add(ucItemOffsetWeight, 0, 3);
      tableLayoutPanel9.Controls.Add(label14, 0, 0);
      tableLayoutPanel9.Controls.Add(ucItemWeight01, 0, 1);
      tableLayoutPanel9.Controls.Add(ucItemWeight02, 0, 2);
      tableLayoutPanel9.Controls.Add(ucItemWeightGoods, 0, 4);
      tableLayoutPanel9.Location = new Point(1324, 0);
      tableLayoutPanel9.Margin = new Padding(0);
      tableLayoutPanel9.Name = "tableLayoutPanel9";
      tableLayoutPanel9.RowCount = 5;
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel9.Size = new Size(201, 506);
      tableLayoutPanel9.TabIndex = 2;
      // 
      // ucItemOffsetWeight
      // 
      ucItemOffsetWeight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemOffsetWeight.BackColor = Color.FromArgb(223, 239, 255);
      ucItemOffsetWeight.Location = new Point(3, 281);
      ucItemOffsetWeight.Name = "ucItemOffsetWeight";
      ucItemOffsetWeight.Size = new Size(195, 108);
      ucItemOffsetWeight.TabIndex = 5;
      // 
      // label14
      // 
      label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label14.AutoSize = true;
      label14.BackColor = Color.FromArgb(199, 199, 199);
      label14.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label14.Location = new Point(0, 0);
      label14.Margin = new Padding(0);
      label14.Name = "label14";
      label14.Size = new Size(201, 50);
      label14.TabIndex = 1;
      label14.Text = "Dữ liệu cân (Kg)";
      label14.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucItemWeight01
      // 
      ucItemWeight01.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemWeight01.BackColor = Color.FromArgb(223, 239, 255);
      ucItemWeight01.Location = new Point(3, 53);
      ucItemWeight01.Name = "ucItemWeight01";
      ucItemWeight01.Size = new Size(195, 108);
      ucItemWeight01.TabIndex = 2;
      // 
      // ucItemWeight02
      // 
      ucItemWeight02.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemWeight02.BackColor = Color.FromArgb(223, 239, 255);
      ucItemWeight02.Location = new Point(3, 167);
      ucItemWeight02.Name = "ucItemWeight02";
      ucItemWeight02.Size = new Size(195, 108);
      ucItemWeight02.TabIndex = 3;
      // 
      // ucItemWeightGoods
      // 
      ucItemWeightGoods.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemWeightGoods.BackColor = Color.FromArgb(223, 239, 255);
      ucItemWeightGoods.Location = new Point(3, 395);
      ucItemWeightGoods.Name = "ucItemWeightGoods";
      ucItemWeightGoods.Size = new Size(195, 108);
      ucItemWeightGoods.TabIndex = 4;
      // 
      // tableLayoutPanel7
      // 
      tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel7.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel7.ColumnCount = 1;
      tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Controls.Add(dgv, 0, 2);
      tableLayoutPanel7.Controls.Add(tableLayoutPanel10, 0, 1);
      tableLayoutPanel7.Controls.Add(label27, 0, 0);
      tableLayoutPanel7.Location = new Point(0, 511);
      tableLayoutPanel7.Margin = new Padding(0);
      tableLayoutPanel7.Name = "tableLayoutPanel7";
      tableLayoutPanel7.RowCount = 3;
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Size = new Size(1525, 414);
      tableLayoutPanel7.TabIndex = 2;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = SystemColors.Control;
      dataGridViewCellStyle1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = SystemColors.Window;
      dataGridViewCellStyle2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      dgv.DefaultCellStyle = dataGridViewCellStyle2;
      dgv.EnableHeadersVisualStyles = false;
      dgv.Location = new Point(3, 115);
      dgv.Name = "dgv";
      dgv.ReadOnly = true;
      dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = SystemColors.Control;
      dataGridViewCellStyle3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1519, 296);
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
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.Controls.Add(label4, 0, 0);
      tableLayoutPanel10.Controls.Add(txtSearchKey, 1, 0);
      tableLayoutPanel10.Controls.Add(label17, 3, 0);
      tableLayoutPanel10.Controls.Add(btnPrint, 9, 0);
      tableLayoutPanel10.Controls.Add(label18, 5, 0);
      tableLayoutPanel10.Controls.Add(btnSearchHistorical, 10, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchFrom, 4, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchTo, 6, 0);
      tableLayoutPanel10.Controls.Add(btnFilter, 7, 0);
      tableLayoutPanel10.Location = new Point(0, 50);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1525, 62);
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
      txtSearchKey.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtSearchKey.ForeColor = Color.FromArgb(64, 64, 64);
      txtSearchKey.Location = new Point(110, 10);
      txtSearchKey.Margin = new Padding(4);
      txtSearchKey.Multiline = false;
      txtSearchKey.Name = "txtSearchKey";
      txtSearchKey.Padding = new Padding(10, 7, 10, 7);
      txtSearchKey.PasswordChar = false;
      txtSearchKey.PlaceholderColor = Color.DarkGray;
      txtSearchKey.PlaceholderText = "";
      txtSearchKey.Size = new Size(164, 42);
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
      label17.Location = new Point(328, 0);
      label17.Margin = new Padding(0);
      label17.Name = "label17";
      label17.Size = new Size(38, 62);
      label17.TabIndex = 21;
      label17.Text = "Từ";
      label17.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // btnPrint
      // 
      btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnPrint.BackColor = Color.FromArgb(64, 107, 177);
      btnPrint.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnPrint.BorderColor = Color.White;
      btnPrint.BorderRadius = 5;
      btnPrint.BorderSize = 0;
      btnPrint.FlatAppearance.BorderColor = Color.White;
      btnPrint.FlatAppearance.BorderSize = 0;
      btnPrint.FlatStyle = FlatStyle.Flat;
      btnPrint.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnPrint.ForeColor = Color.White;
      btnPrint.Image = Properties.Resources.icon_print;
      btnPrint.ImageAlign = ContentAlignment.MiddleLeft;
      btnPrint.Location = new Point(1168, 3);
      btnPrint.Name = "btnPrint";
      btnPrint.Padding = new Padding(10, 0, 0, 0);
      btnPrint.Size = new Size(174, 56);
      btnPrint.TabIndex = 20;
      btnPrint.Text = "       In phiếu";
      btnPrint.TextAlign = ContentAlignment.MiddleLeft;
      btnPrint.TextColor = Color.White;
      btnPrint.UseVisualStyleBackColor = false;
      btnPrint.Click += btnPrint_Click;
      // 
      // label18
      // 
      label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label18.AutoSize = true;
      label18.BackColor = Color.Transparent;
      label18.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label18.Location = new Point(686, 0);
      label18.Margin = new Padding(0);
      label18.Name = "label18";
      label18.Size = new Size(49, 62);
      label18.TabIndex = 22;
      label18.Text = "đến";
      label18.TextAlign = ContentAlignment.MiddleLeft;
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
      btnSearchHistorical.Text = "       Tìm kiếm";
      btnSearchHistorical.TextAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.TextColor = Color.White;
      btnSearchHistorical.UseVisualStyleBackColor = false;
      btnSearchHistorical.Click += btnSearchHistorical_Click;
      // 
      // ucTimeSearchFrom
      // 
      ucTimeSearchFrom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchFrom.Location = new Point(369, 3);
      ucTimeSearchFrom.Name = "ucTimeSearchFrom";
      ucTimeSearchFrom.Size = new Size(314, 56);
      ucTimeSearchFrom.TabIndex = 30;
      // 
      // ucTimeSearchTo
      // 
      ucTimeSearchTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchTo.Location = new Point(738, 3);
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
      btnFilter.Location = new Point(1058, 3);
      btnFilter.Name = "btnFilter";
      btnFilter.Size = new Size(54, 55);
      btnFilter.TabIndex = 32;
      btnFilter.TextColor = Color.White;
      btnFilter.UseVisualStyleBackColor = false;
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
      label27.Size = new Size(1525, 50);
      label27.TabIndex = 0;
      label27.Text = "Lịch sử cân";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // FrmHomeTruck
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1530, 930);
      Controls.Add(tableLayoutPanel1);
      Name = "FrmHomeTruck";
      Text = "FrmHome";
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel12.ResumeLayout(false);
      tableLayoutPanel17.ResumeLayout(false);
      tableLayoutPanel17.PerformLayout();
      tableLayoutPanel18.ResumeLayout(false);
      tableLayoutPanel18.PerformLayout();
      tableLayoutPanel6.ResumeLayout(false);
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel4.PerformLayout();
      tableLayoutPanel8.ResumeLayout(false);
      tableLayoutPanel8.PerformLayout();
      tableLayoutPanel15.ResumeLayout(false);
      tableLayoutPanel14.ResumeLayout(false);
      tableLayoutPanel13.ResumeLayout(false);
      tableLayoutPanel11.ResumeLayout(false);
      tableLayoutPanel11.PerformLayout();
      tableLayoutPanel16.ResumeLayout(false);
      tableLayoutPanel16.PerformLayout();
      tableLayoutPanel9.ResumeLayout(false);
      tableLayoutPanel9.PerformLayout();
      tableLayoutPanel7.ResumeLayout(false);
      tableLayoutPanel7.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      tableLayoutPanel10.ResumeLayout(false);
      tableLayoutPanel10.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbWeightValue;
    private Label label5;
    private Label label3;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel6;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel8;
    private Label label6;
    private Label label7;
    private Label label8;
    private Label label9;
    private Label label11;
    private Label label10;
    private TableLayoutPanel tableLayoutPanel9;
    private Label label14;
    private UserControls.UcItem ucItemWeight01;
    private UserControls.UcItem ucItemWeight02;
    private UserControls.UcItem ucItemWeightGoods;
    private Label label16;
    private TableLayoutPanel tableLayoutPanel11;
    private Custom.RJTextBox txtNoLabelAuto;
    private Label label12;
    private Custom.RJTextBox txtNoLabel;
    private TableLayoutPanel tableLayoutPanel14;
    private Custom.RJTextBox txtTypeGoods;
    private RJButton btnLoadTypeGoods;
    private TableLayoutPanel tableLayoutPanel13;
    private Custom.RJTextBox txtClient;
    private RJButton btnLoadClient;
    private TableLayoutPanel tableLayoutPanel15;
    private Custom.RJTextBox txtWareHouse;
    private RJButton btnLoadWarehouse;
    private TableLayoutPanel tableLayoutPanel16;
    private Custom.RJTextBox txtLicensePlate;
    private Label label13;
    private Custom.RJTextBox txtIdCard;
    private Custom.RJTextBox txtNameDriver;
    private Label label19;
    private Custom.RJTextBox txtTypeWeight;
    private RJButton btnZero;
    private RJButton btnBack;
    private RJButton btnPrint;
    private RJButton btnWeightTime02;
    private RJButton btnWeightTime01;
    private RJButton btnTriggerWeight;
    private TableLayoutPanel tableLayoutPanel7;
    private Label label27;
    private TableLayoutPanel tableLayoutPanel10;
    private Label label4;
    private Custom.RJTextBox txtSearchKey;
    private Label label17;
    private Label label18;
    private RJButton btnSearchHistorical;
    private TableLayoutPanel tableLayoutPanel12;
    private TableLayoutPanel tableLayoutPanel17;
    private Label lbWeightTrigger;
    private Label label2;
    private TableLayoutPanel tableLayoutPanel18;
    private DataGridView dgv;
    private RJButton btnCreate;
    private Label label20;
    private UserControls.UcItem ucItemOffsetWeight;
    private UserControls.UcTimeSearch ucTimeSearchFrom;
    private UserControls.UcTimeSearch ucTimeSearchTo;
    private Common.Custom.RJButton btnFilter;
    private TextBox txtDocument;
  }
}
