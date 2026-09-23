using LTP.Truck.Custom;

namespace LTP.Truck.Forms
{
  partial class FrmOperation
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperation));
      tableLayoutPanel1 = new TableLayoutPanel();
      panelMenu = new TableLayoutPanel();
      tableLayoutPanel5 = new TableLayoutPanel();
      btnMenu = new PictureBox();
      tableLayoutPanel6 = new TableLayoutPanel();
      flowLayoutPanel1 = new FlowLayoutPanel();
      btnHomeTruck = new RJButton();
      btnHomeGoods = new RJButton();
      btnSetting = new RJButton();
      btnMasterData = new RJButton();
      btnWarehouse = new RJButton();
      btnTare = new RJButton();
      btnTypeGoods = new RJButton();
      btnGroupProduct = new RJButton();
      btnProduct = new RJButton();
      btnClient = new RJButton();
      btnReportTruck = new RJButton();
      btnReportGoods = new RJButton();
      tableLayoutPanel7 = new TableLayoutPanel();
      lbVersion = new Label();
      pictureBox1 = new PictureBox();
      tableLayoutPanel2 = new TableLayoutPanel();
      tableLayoutPanel4 = new TableLayoutPanel();
      tableLayoutPanel3 = new TableLayoutPanel();
      lbTitlePage = new Label();
      lbTitle = new Label();
      btnLogout = new Common.Custom.RJButton();
      ucLogin = new LTP.Truck.UserControls.UcLogin();
      panelMain = new Panel();
      tableLayoutPanel8 = new TableLayoutPanel();
      ucStatusConnectServer = new LTP.Truck.UserControls.UcStatusConnect();
      ucStatusConnectWeight = new LTP.Truck.UserControls.UcStatusConnect();
      lbTime = new Label();
      tableLayoutPanel1.SuspendLayout();
      panelMenu.SuspendLayout();
      tableLayoutPanel5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)btnMenu).BeginInit();
      tableLayoutPanel6.SuspendLayout();
      flowLayoutPanel1.SuspendLayout();
      tableLayoutPanel7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel8.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.White;
      tableLayoutPanel1.ColumnCount = 3;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(panelMenu, 0, 0);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 2, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(1268, 941);
      tableLayoutPanel1.TabIndex = 3;
      // 
      // panelMenu
      // 
      panelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      panelMenu.BackColor = Color.White;
      panelMenu.ColumnCount = 1;
      panelMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      panelMenu.Controls.Add(tableLayoutPanel5, 0, 0);
      panelMenu.Controls.Add(tableLayoutPanel6, 0, 2);
      panelMenu.Location = new Point(0, 0);
      panelMenu.Margin = new Padding(0);
      panelMenu.Name = "panelMenu";
      panelMenu.RowCount = 3;
      panelMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
      panelMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      panelMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      panelMenu.Size = new Size(250, 941);
      panelMenu.TabIndex = 1;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(btnMenu, 0, 0);
      tableLayoutPanel5.Location = new Point(0, 0);
      tableLayoutPanel5.Margin = new Padding(0);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 1;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Size = new Size(250, 80);
      tableLayoutPanel5.TabIndex = 1;
      // 
      // btnMenu
      // 
      btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnMenu.Image = (Image)resources.GetObject("btnMenu.Image");
      btnMenu.Location = new Point(5, 5);
      btnMenu.Margin = new Padding(5);
      btnMenu.Name = "btnMenu";
      btnMenu.Size = new Size(70, 70);
      btnMenu.SizeMode = PictureBoxSizeMode.StretchImage;
      btnMenu.TabIndex = 0;
      btnMenu.TabStop = false;
      btnMenu.Click += btnMenu_Click;
      // 
      // tableLayoutPanel6
      // 
      tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel6.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel6.ColumnCount = 1;
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel6.Controls.Add(flowLayoutPanel1, 0, 0);
      tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 0, 1);
      tableLayoutPanel6.Location = new Point(0, 85);
      tableLayoutPanel6.Margin = new Padding(0);
      tableLayoutPanel6.Name = "tableLayoutPanel6";
      tableLayoutPanel6.RowCount = 2;
      tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel6.RowStyles.Add(new RowStyle());
      tableLayoutPanel6.Size = new Size(250, 856);
      tableLayoutPanel6.TabIndex = 2;
      // 
      // flowLayoutPanel1
      // 
      flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      flowLayoutPanel1.BackColor = Color.FromArgb(236, 236, 236);
      flowLayoutPanel1.Controls.Add(btnHomeTruck);
      flowLayoutPanel1.Controls.Add(btnHomeGoods);
      flowLayoutPanel1.Controls.Add(btnSetting);
      flowLayoutPanel1.Controls.Add(btnMasterData);
      flowLayoutPanel1.Controls.Add(btnWarehouse);
      flowLayoutPanel1.Controls.Add(btnTare);
      flowLayoutPanel1.Controls.Add(btnTypeGoods);
      flowLayoutPanel1.Controls.Add(btnGroupProduct);
      flowLayoutPanel1.Controls.Add(btnProduct);
      flowLayoutPanel1.Controls.Add(btnClient);
      flowLayoutPanel1.Controls.Add(btnReportTruck);
      flowLayoutPanel1.Controls.Add(btnReportGoods);
      flowLayoutPanel1.Location = new Point(0, 0);
      flowLayoutPanel1.Margin = new Padding(0);
      flowLayoutPanel1.Name = "flowLayoutPanel1";
      flowLayoutPanel1.Size = new Size(250, 765);
      flowLayoutPanel1.TabIndex = 0;
      // 
      // btnHomeTruck
      // 
      btnHomeTruck.BackColor = Color.Silver;
      btnHomeTruck.BackgroundColor = Color.Silver;
      btnHomeTruck.BorderColor = Color.PaleVioletRed;
      btnHomeTruck.BorderRadius = 5;
      btnHomeTruck.BorderSize = 0;
      btnHomeTruck.Dock = DockStyle.Top;
      btnHomeTruck.FlatAppearance.BorderSize = 0;
      btnHomeTruck.FlatStyle = FlatStyle.Flat;
      btnHomeTruck.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnHomeTruck.ForeColor = Color.Black;
      btnHomeTruck.Image = Properties.Resources.icon_truck;
      btnHomeTruck.ImageAlign = ContentAlignment.MiddleLeft;
      btnHomeTruck.Location = new Point(3, 3);
      btnHomeTruck.Name = "btnHomeTruck";
      btnHomeTruck.Padding = new Padding(15, 0, 0, 0);
      btnHomeTruck.Size = new Size(242, 70);
      btnHomeTruck.TabIndex = 0;
      btnHomeTruck.Text = "        CÂN XE TẢI";
      btnHomeTruck.TextAlign = ContentAlignment.MiddleLeft;
      btnHomeTruck.TextColor = Color.Black;
      btnHomeTruck.UseVisualStyleBackColor = false;
      btnHomeTruck.Click += btnHomeTruck_Click;
      // 
      // btnHomeGoods
      // 
      btnHomeGoods.BackColor = Color.Silver;
      btnHomeGoods.BackgroundColor = Color.Silver;
      btnHomeGoods.BorderColor = Color.PaleVioletRed;
      btnHomeGoods.BorderRadius = 5;
      btnHomeGoods.BorderSize = 0;
      btnHomeGoods.Dock = DockStyle.Top;
      btnHomeGoods.FlatAppearance.BorderSize = 0;
      btnHomeGoods.FlatStyle = FlatStyle.Flat;
      btnHomeGoods.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnHomeGoods.ForeColor = Color.Black;
      btnHomeGoods.Image = Properties.Resources.icon_goods;
      btnHomeGoods.ImageAlign = ContentAlignment.MiddleLeft;
      btnHomeGoods.Location = new Point(3, 79);
      btnHomeGoods.Name = "btnHomeGoods";
      btnHomeGoods.Padding = new Padding(15, 0, 0, 0);
      btnHomeGoods.Size = new Size(242, 70);
      btnHomeGoods.TabIndex = 2;
      btnHomeGoods.Text = "        CÂN HÀNG";
      btnHomeGoods.TextAlign = ContentAlignment.MiddleLeft;
      btnHomeGoods.TextColor = Color.Black;
      btnHomeGoods.UseVisualStyleBackColor = false;
      // 
      // btnSetting
      // 
      btnSetting.BackColor = Color.Silver;
      btnSetting.BackgroundColor = Color.Silver;
      btnSetting.BorderColor = Color.PaleVioletRed;
      btnSetting.BorderRadius = 5;
      btnSetting.BorderSize = 0;
      btnSetting.Dock = DockStyle.Top;
      btnSetting.FlatAppearance.BorderSize = 0;
      btnSetting.FlatStyle = FlatStyle.Flat;
      btnSetting.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnSetting.ForeColor = Color.Black;
      btnSetting.Image = (Image)resources.GetObject("btnSetting.Image");
      btnSetting.ImageAlign = ContentAlignment.MiddleLeft;
      btnSetting.Location = new Point(3, 155);
      btnSetting.Name = "btnSetting";
      btnSetting.Padding = new Padding(15, 0, 0, 0);
      btnSetting.Size = new Size(242, 70);
      btnSetting.TabIndex = 6;
      btnSetting.Text = "        CÀI ĐẶT";
      btnSetting.TextAlign = ContentAlignment.MiddleLeft;
      btnSetting.TextColor = Color.Black;
      btnSetting.UseVisualStyleBackColor = false;
      btnSetting.Click += btnSetting_Click;
      // 
      // btnMasterData
      // 
      btnMasterData.BackColor = Color.Silver;
      btnMasterData.BackgroundColor = Color.Silver;
      btnMasterData.BorderColor = Color.PaleVioletRed;
      btnMasterData.BorderRadius = 5;
      btnMasterData.BorderSize = 0;
      btnMasterData.Dock = DockStyle.Top;
      btnMasterData.FlatAppearance.BorderSize = 0;
      btnMasterData.FlatStyle = FlatStyle.Flat;
      btnMasterData.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnMasterData.ForeColor = Color.Black;
      btnMasterData.Image = Properties.Resources.icon_btn_masterdata;
      btnMasterData.ImageAlign = ContentAlignment.MiddleLeft;
      btnMasterData.Location = new Point(3, 231);
      btnMasterData.Name = "btnMasterData";
      btnMasterData.Padding = new Padding(15, 0, 0, 0);
      btnMasterData.Size = new Size(242, 70);
      btnMasterData.TabIndex = 7;
      btnMasterData.Text = "        MASTERDATA";
      btnMasterData.TextAlign = ContentAlignment.MiddleLeft;
      btnMasterData.TextColor = Color.Black;
      btnMasterData.UseVisualStyleBackColor = false;
      btnMasterData.Click += btnMasterData_Click;
      // 
      // btnWarehouse
      // 
      btnWarehouse.BackColor = Color.Transparent;
      btnWarehouse.BackgroundColor = Color.Transparent;
      btnWarehouse.BorderColor = Color.PaleVioletRed;
      btnWarehouse.BorderRadius = 5;
      btnWarehouse.BorderSize = 0;
      btnWarehouse.Dock = DockStyle.Top;
      btnWarehouse.FlatAppearance.BorderSize = 0;
      btnWarehouse.FlatStyle = FlatStyle.Flat;
      btnWarehouse.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnWarehouse.ForeColor = Color.Black;
      btnWarehouse.ImageAlign = ContentAlignment.MiddleLeft;
      btnWarehouse.Location = new Point(3, 307);
      btnWarehouse.Name = "btnWarehouse";
      btnWarehouse.Padding = new Padding(60, 0, 0, 0);
      btnWarehouse.Size = new Size(242, 40);
      btnWarehouse.TabIndex = 10;
      btnWarehouse.Text = "Kho hàng";
      btnWarehouse.TextAlign = ContentAlignment.MiddleLeft;
      btnWarehouse.TextColor = Color.Black;
      btnWarehouse.UseVisualStyleBackColor = false;
      // 
      // btnTare
      // 
      btnTare.BackColor = Color.Transparent;
      btnTare.BackgroundColor = Color.Transparent;
      btnTare.BorderColor = Color.PaleVioletRed;
      btnTare.BorderRadius = 5;
      btnTare.BorderSize = 0;
      btnTare.Dock = DockStyle.Top;
      btnTare.FlatAppearance.BorderSize = 0;
      btnTare.FlatStyle = FlatStyle.Flat;
      btnTare.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnTare.ForeColor = Color.Black;
      btnTare.ImageAlign = ContentAlignment.MiddleLeft;
      btnTare.Location = new Point(3, 353);
      btnTare.Name = "btnTare";
      btnTare.Padding = new Padding(60, 0, 0, 0);
      btnTare.Size = new Size(242, 40);
      btnTare.TabIndex = 11;
      btnTare.Text = "Nhóm Tare";
      btnTare.TextAlign = ContentAlignment.MiddleLeft;
      btnTare.TextColor = Color.Black;
      btnTare.UseVisualStyleBackColor = false;
      // 
      // btnTypeGoods
      // 
      btnTypeGoods.BackColor = Color.Transparent;
      btnTypeGoods.BackgroundColor = Color.Transparent;
      btnTypeGoods.BorderColor = Color.PaleVioletRed;
      btnTypeGoods.BorderRadius = 5;
      btnTypeGoods.BorderSize = 0;
      btnTypeGoods.Dock = DockStyle.Top;
      btnTypeGoods.FlatAppearance.BorderSize = 0;
      btnTypeGoods.FlatStyle = FlatStyle.Flat;
      btnTypeGoods.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnTypeGoods.ForeColor = Color.Black;
      btnTypeGoods.ImageAlign = ContentAlignment.MiddleLeft;
      btnTypeGoods.Location = new Point(3, 399);
      btnTypeGoods.Name = "btnTypeGoods";
      btnTypeGoods.Padding = new Padding(60, 0, 0, 0);
      btnTypeGoods.Size = new Size(242, 40);
      btnTypeGoods.TabIndex = 14;
      btnTypeGoods.Text = "Loại hàng";
      btnTypeGoods.TextAlign = ContentAlignment.MiddleLeft;
      btnTypeGoods.TextColor = Color.Black;
      btnTypeGoods.UseVisualStyleBackColor = false;
      // 
      // btnGroupProduct
      // 
      btnGroupProduct.BackColor = Color.Transparent;
      btnGroupProduct.BackgroundColor = Color.Transparent;
      btnGroupProduct.BorderColor = Color.PaleVioletRed;
      btnGroupProduct.BorderRadius = 5;
      btnGroupProduct.BorderSize = 0;
      btnGroupProduct.Dock = DockStyle.Top;
      btnGroupProduct.FlatAppearance.BorderSize = 0;
      btnGroupProduct.FlatStyle = FlatStyle.Flat;
      btnGroupProduct.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnGroupProduct.ForeColor = Color.Black;
      btnGroupProduct.ImageAlign = ContentAlignment.MiddleLeft;
      btnGroupProduct.Location = new Point(3, 445);
      btnGroupProduct.Name = "btnGroupProduct";
      btnGroupProduct.Padding = new Padding(60, 0, 0, 0);
      btnGroupProduct.Size = new Size(242, 40);
      btnGroupProduct.TabIndex = 15;
      btnGroupProduct.Text = "Nhóm chất thải";
      btnGroupProduct.TextAlign = ContentAlignment.MiddleLeft;
      btnGroupProduct.TextColor = Color.Black;
      btnGroupProduct.UseVisualStyleBackColor = false;
      // 
      // btnProduct
      // 
      btnProduct.BackColor = Color.Transparent;
      btnProduct.BackgroundColor = Color.Transparent;
      btnProduct.BorderColor = Color.PaleVioletRed;
      btnProduct.BorderRadius = 5;
      btnProduct.BorderSize = 0;
      btnProduct.Dock = DockStyle.Top;
      btnProduct.FlatAppearance.BorderSize = 0;
      btnProduct.FlatStyle = FlatStyle.Flat;
      btnProduct.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnProduct.ForeColor = Color.Black;
      btnProduct.ImageAlign = ContentAlignment.MiddleLeft;
      btnProduct.Location = new Point(3, 491);
      btnProduct.Name = "btnProduct";
      btnProduct.Padding = new Padding(60, 0, 0, 0);
      btnProduct.Size = new Size(242, 40);
      btnProduct.TabIndex = 16;
      btnProduct.Text = "Chất thải";
      btnProduct.TextAlign = ContentAlignment.MiddleLeft;
      btnProduct.TextColor = Color.Black;
      btnProduct.UseVisualStyleBackColor = false;
      // 
      // btnClient
      // 
      btnClient.BackColor = Color.Transparent;
      btnClient.BackgroundColor = Color.Transparent;
      btnClient.BorderColor = Color.PaleVioletRed;
      btnClient.BorderRadius = 5;
      btnClient.BorderSize = 0;
      btnClient.Dock = DockStyle.Top;
      btnClient.FlatAppearance.BorderSize = 0;
      btnClient.FlatStyle = FlatStyle.Flat;
      btnClient.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnClient.ForeColor = Color.Black;
      btnClient.ImageAlign = ContentAlignment.MiddleLeft;
      btnClient.Location = new Point(3, 537);
      btnClient.Name = "btnClient";
      btnClient.Padding = new Padding(60, 0, 0, 0);
      btnClient.Size = new Size(242, 40);
      btnClient.TabIndex = 17;
      btnClient.Text = "Khách hàng";
      btnClient.TextAlign = ContentAlignment.MiddleLeft;
      btnClient.TextColor = Color.Black;
      btnClient.UseVisualStyleBackColor = false;
      // 
      // btnReportTruck
      // 
      btnReportTruck.BackColor = Color.Silver;
      btnReportTruck.BackgroundColor = Color.Silver;
      btnReportTruck.BorderColor = Color.PaleVioletRed;
      btnReportTruck.BorderRadius = 5;
      btnReportTruck.BorderSize = 0;
      btnReportTruck.Dock = DockStyle.Top;
      btnReportTruck.FlatAppearance.BorderSize = 0;
      btnReportTruck.FlatStyle = FlatStyle.Flat;
      btnReportTruck.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnReportTruck.ForeColor = Color.Black;
      btnReportTruck.Image = Properties.Resources.icon_report;
      btnReportTruck.ImageAlign = ContentAlignment.MiddleLeft;
      btnReportTruck.Location = new Point(3, 583);
      btnReportTruck.Name = "btnReportTruck";
      btnReportTruck.Padding = new Padding(15, 0, 0, 0);
      btnReportTruck.Size = new Size(242, 70);
      btnReportTruck.TabIndex = 18;
      btnReportTruck.Text = "        BÁO CÁO";
      btnReportTruck.TextAlign = ContentAlignment.MiddleLeft;
      btnReportTruck.TextColor = Color.Black;
      btnReportTruck.UseVisualStyleBackColor = false;
      btnReportTruck.Click += btnReportTruck_Click;
      // 
      // btnReportGoods
      // 
      btnReportGoods.BackColor = Color.Silver;
      btnReportGoods.BackgroundColor = Color.Silver;
      btnReportGoods.BorderColor = Color.PaleVioletRed;
      btnReportGoods.BorderRadius = 5;
      btnReportGoods.BorderSize = 0;
      btnReportGoods.Dock = DockStyle.Top;
      btnReportGoods.FlatAppearance.BorderSize = 0;
      btnReportGoods.FlatStyle = FlatStyle.Flat;
      btnReportGoods.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnReportGoods.ForeColor = Color.Black;
      btnReportGoods.Image = Properties.Resources.icon_report;
      btnReportGoods.ImageAlign = ContentAlignment.MiddleLeft;
      btnReportGoods.Location = new Point(3, 659);
      btnReportGoods.Name = "btnReportGoods";
      btnReportGoods.Padding = new Padding(15, 0, 0, 0);
      btnReportGoods.Size = new Size(242, 70);
      btnReportGoods.TabIndex = 19;
      btnReportGoods.Text = "        BÁO CÁO";
      btnReportGoods.TextAlign = ContentAlignment.MiddleLeft;
      btnReportGoods.TextColor = Color.Black;
      btnReportGoods.UseVisualStyleBackColor = false;
      btnReportGoods.Click += btnReportGoods_Click;
      // 
      // tableLayoutPanel7
      // 
      tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel7.ColumnCount = 1;
      tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Controls.Add(lbVersion, 0, 1);
      tableLayoutPanel7.Controls.Add(pictureBox1, 0, 0);
      tableLayoutPanel7.Location = new Point(3, 768);
      tableLayoutPanel7.Name = "tableLayoutPanel7";
      tableLayoutPanel7.RowCount = 3;
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle());
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel7.Size = new Size(244, 85);
      tableLayoutPanel7.TabIndex = 1;
      // 
      // lbVersion
      // 
      lbVersion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbVersion.AutoSize = true;
      lbVersion.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lbVersion.Location = new Point(0, 53);
      lbVersion.Margin = new Padding(0);
      lbVersion.Name = "lbVersion";
      lbVersion.Size = new Size(244, 27);
      lbVersion.TabIndex = 4;
      lbVersion.Text = "Version: 1.0.0";
      lbVersion.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // pictureBox1
      // 
      pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox1.Image = Properties.Resources.LogoBosch;
      pictureBox1.Location = new Point(10, 0);
      pictureBox1.Margin = new Padding(10, 0, 10, 0);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(224, 53);
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.BackColor = Color.White;
      tableLayoutPanel2.ColumnCount = 1;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 0);
      tableLayoutPanel2.Controls.Add(panelMain, 0, 2);
      tableLayoutPanel2.Controls.Add(tableLayoutPanel8, 0, 3);
      tableLayoutPanel2.Location = new Point(255, 0);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 4;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel2.Size = new Size(1013, 941);
      tableLayoutPanel2.TabIndex = 0;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel4.ColumnCount = 3;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
      tableLayoutPanel4.Controls.Add(tableLayoutPanel3, 0, 0);
      tableLayoutPanel4.Controls.Add(btnLogout, 1, 0);
      tableLayoutPanel4.Controls.Add(ucLogin, 2, 0);
      tableLayoutPanel4.Location = new Point(0, 0);
      tableLayoutPanel4.Margin = new Padding(0);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 1;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Size = new Size(1013, 80);
      tableLayoutPanel4.TabIndex = 2;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(lbTitlePage, 0, 1);
      tableLayoutPanel3.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel3.Location = new Point(3, 3);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 2;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle());
      tableLayoutPanel3.Size = new Size(527, 74);
      tableLayoutPanel3.TabIndex = 22;
      // 
      // lbTitlePage
      // 
      lbTitlePage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitlePage.AutoSize = true;
      lbTitlePage.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitlePage.Location = new Point(5, 47);
      lbTitlePage.Margin = new Padding(5, 0, 0, 0);
      lbTitlePage.Name = "lbTitlePage";
      lbTitlePage.Size = new Size(522, 27);
      lbTitlePage.TabIndex = 4;
      lbTitlePage.Text = "Trang chính";
      lbTitlePage.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.Font = new Font("Roboto", 24.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(0, 0);
      lbTitle.Margin = new Padding(0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(527, 47);
      lbTitle.TabIndex = 3;
      lbTitle.Text = "HỆ THỐNG CÂN XE TẢI";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // btnLogout
      // 
      btnLogout.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLogout.BackColor = Color.Red;
      btnLogout.BackgroundColor = Color.Red;
      btnLogout.BorderColor = Color.PaleVioletRed;
      btnLogout.BorderRadius = 5;
      btnLogout.BorderSize = 0;
      btnLogout.FlatAppearance.BorderSize = 0;
      btnLogout.FlatStyle = FlatStyle.Flat;
      btnLogout.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLogout.ForeColor = Color.White;
      btnLogout.Image = Properties.Resources.icon_logout;
      btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
      btnLogout.Location = new Point(536, 10);
      btnLogout.Margin = new Padding(3, 3, 10, 3);
      btnLogout.Name = "btnLogout";
      btnLogout.Padding = new Padding(5, 0, 0, 0);
      btnLogout.Size = new Size(187, 60);
      btnLogout.TabIndex = 23;
      btnLogout.Text = "        Đăng xuất";
      btnLogout.TextAlign = ContentAlignment.MiddleLeft;
      btnLogout.TextColor = Color.White;
      btnLogout.UseVisualStyleBackColor = false;
      btnLogout.Click += btnLogout_Click;
      // 
      // ucLogin
      // 
      ucLogin.Account = "Login";
      ucLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      ucLogin.Location = new Point(736, 9);
      ucLogin.Name = "ucLogin";
      ucLogin.Size = new Size(274, 61);
      ucLogin.TabIndex = 24;
      // 
      // panelMain
      // 
      panelMain.BackColor = Color.FromArgb(236, 236, 236);
      panelMain.Dock = DockStyle.Fill;
      panelMain.Location = new Point(0, 85);
      panelMain.Margin = new Padding(0);
      panelMain.Name = "panelMain";
      panelMain.Size = new Size(1013, 806);
      panelMain.TabIndex = 3;
      // 
      // tableLayoutPanel8
      // 
      tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel8.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel8.ColumnCount = 5;
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 3F));
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
      tableLayoutPanel8.Controls.Add(ucStatusConnectServer, 1, 0);
      tableLayoutPanel8.Controls.Add(ucStatusConnectWeight, 3, 0);
      tableLayoutPanel8.Controls.Add(lbTime, 4, 0);
      tableLayoutPanel8.Location = new Point(0, 891);
      tableLayoutPanel8.Margin = new Padding(0);
      tableLayoutPanel8.Name = "tableLayoutPanel8";
      tableLayoutPanel8.RowCount = 1;
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel8.Size = new Size(1013, 50);
      tableLayoutPanel8.TabIndex = 4;
      // 
      // ucStatusConnectServer
      // 
      ucStatusConnectServer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucStatusConnectServer.Location = new Point(203, 3);
      ucStatusConnectServer.Name = "ucStatusConnectServer";
      ucStatusConnectServer.Size = new Size(274, 44);
      ucStatusConnectServer.TabIndex = 0;
      // 
      // ucStatusConnectWeight
      // 
      ucStatusConnectWeight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucStatusConnectWeight.Location = new Point(486, 3);
      ucStatusConnectWeight.Name = "ucStatusConnectWeight";
      ucStatusConnectWeight.Size = new Size(274, 44);
      ucStatusConnectWeight.TabIndex = 1;
      // 
      // lbTime
      // 
      lbTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTime.AutoSize = true;
      lbTime.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTime.Location = new Point(766, 0);
      lbTime.Name = "lbTime";
      lbTime.Size = new Size(244, 50);
      lbTime.TabIndex = 2;
      lbTime.Text = "...";
      lbTime.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // FrmOperation
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1268, 941);
      Controls.Add(tableLayoutPanel1);
      Name = "FrmOperation";
      Text = "FrmOperation";
      tableLayoutPanel1.ResumeLayout(false);
      panelMenu.ResumeLayout(false);
      tableLayoutPanel5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)btnMenu).EndInit();
      tableLayoutPanel6.ResumeLayout(false);
      flowLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel7.ResumeLayout(false);
      tableLayoutPanel7.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel8.ResumeLayout(false);
      tableLayoutPanel8.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel panelMenu;
    private TableLayoutPanel tableLayoutPanel5;
    private PictureBox btnMenu;
    private TableLayoutPanel tableLayoutPanel6;
    private FlowLayoutPanel flowLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel7;
    private Label lbVersion;
    private PictureBox pictureBox1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbTitlePage;
    private Label lbTitle;
    private Panel panelMain;
    private Custom.RJButton btnHomeTruck;
    private Custom.RJButton btnHomeGoods;
    private RJButton btnSetting;
    private Custom.RJButton btnMasterData;
    private Custom.RJButton btnWarehouse;
    private Custom.RJButton btnTare;
    private RJButton btnTypeGoods;
    private RJButton btnGroupProduct;
    private RJButton btnProduct;
    private RJButton btnClient;
    private TableLayoutPanel tableLayoutPanel8;
    private UserControls.UcStatusConnect ucStatusConnectServer;
    private UserControls.UcStatusConnect ucStatusConnectWeight;
    private Label lbTime;
    private Common.Custom.RJButton btnLogout;
    private UserControls.UcLogin ucLogin;
    private RJButton btnReportTruck;
    private RJButton btnReportGoods;
  }
}
