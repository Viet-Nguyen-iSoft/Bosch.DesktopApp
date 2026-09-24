using Common;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;
using static Common.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupLoadMD : Form
  {
    public event Action<object, EnumTypeData>? OnSendData;
    public event Action<EnumTypeData>? OnAddData;

    private EnumTypeData _enumTypeData {  get; set; }
    public PopupLoadMD()
    {
      InitializeComponent();
      CustomUI();
      btnAdd.Click += btnAdd_Click;
    }

    private void CustomUI()
    {
      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.BorderStyle = BorderStyle.None;
      dgv.MultiSelect = false;
    }

    public void SetData<T>(List<T> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof(items));

      if (typeof(T) == typeof(Client))
      {
        _enumTypeData = EnumTypeData.Client;
        btnAdd.Visible = true;

        var dto = DTOHelper.ConvertClientDTO(items as List<Client>);
        dgv.DataSource = dto;

        dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
      else if (typeof(T) == typeof(TypeGoods))
      {
        _enumTypeData = EnumTypeData.TypeGoods;
        btnAdd.Visible = true;

        var dto = DTOHelper.ConvertTypeGoodsDTO(items as List<TypeGoods>);
        dgv.DataSource = dto;

        dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
      else if (typeof(T) == typeof(RecordTruckDTO))
      {
        _enumTypeData = EnumTypeData.RecordTruck;
        btnAdd.Visible = false;
        lbTitle.Text = "Danh sách phiếu đã cân lần 1";
        dgv.DataSource = items;
        dgv.Columns[nameof(RecordTruckDTO.NetTime02)].Visible = false;
        dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        var hideColumns = new[]
        {
          nameof(RecordTruckDTO.NetTime01),
          nameof(RecordTruckDTO.NetTime02),
          nameof(RecordTruckDTO.Status),
          nameof(RecordTruckDTO.Warehouse),
          nameof(RecordTruckDTO.TypeGoods),
          nameof(RecordTruckDTO.Client),
          nameof(RecordTruckDTO.Document),
        };
        foreach (var columnName in hideColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].Visible = false;
        }


        var autoSizeColumns = new[]
        {
            nameof(RecordTruckDTO.No),
            nameof(RecordTruckDTO.NoLabelManual),
          };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
      }
      else if (typeof(T) == typeof(Warehouse))
      {
        _enumTypeData = EnumTypeData.Warehouse;
        btnAdd.Visible = true;

        var dto = DTOHelper.ConvertWareHouseDTO(items as List<Warehouse>);
        dgv.DataSource = dto;

        dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
      OnAddData?.Invoke(_enumTypeData);
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
      if (dgv.SelectedRows.Count > 0)
      {
        DataGridViewRow row = dgv.SelectedRows[0];
        OnSendData?.Invoke(row.DataBoundItem, _enumTypeData);
        this.Close();
      }
      else
      {
        PopupConfirm popupConfirm = new PopupConfirm("Vui lòng chọn dữ liệu cần chọn !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
      }

      
    }
  }
}
