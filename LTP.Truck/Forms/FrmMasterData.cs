using ApiSyncData.Req;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.MasterData;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using static Common.EnumData;
using static HelperManager.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmMasterData : Form
  {
    private const string EditButtonColumnName = "btnEdit";
    private const string DeleteButtonColumnName = "btnDelete";

    private CancellationTokenSource? _searchDebounceCancellation;
    private ApiJobsService _apiJobsService { get; set; } 
    public FrmMasterData()
    {
      InitializeComponent();
      CustomUI();

      RegisterService();

      txtSearch._TextChanged += txtSearch_TextChanged;
      dgv.CellContentClick += dgv_CellContentClick;
    }

    private void RegisterService()
    {
      _apiJobsService = new ApiJobsService();
    }
    #region Instance
    private static FrmMasterData _Instance = null;
    public static FrmMasterData Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmMasterData();
        return _Instance;
      }
    }
    #endregion


    private void CustomUI()
    {
      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.BorderStyle = BorderStyle.None;
      dgv.MultiSelect = false;
      dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
      dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
      dgv.RowHeadersDefaultCellStyle.SelectionBackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
      dgv.RowHeadersDefaultCellStyle.SelectionForeColor = dgv.RowHeadersDefaultCellStyle.ForeColor;

    }

    private EnumTypeMasterData _enumTypeMasterDataCurrent { get; set; }

    public async Task LoadData(EnumTypeMasterData enumTypeMaster)
    {
      _enumTypeMasterDataCurrent = enumTypeMaster;
      string searchKey = txtSearch.Texts.Trim();
      switch (enumTypeMaster)
      {
        case EnumTypeMasterData.Client:
          var rsClient = await AppCore.Ins._clientService.GetAllAsync();
          var dtoClient = DTOHelper.ConvertClientDTO(rsClient);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoClient, searchKey));
          break;
        case EnumTypeMasterData.TypeGoods:
          var rsTypeGoods = await AppCore.Ins._typeGoodsService.GetAllAsync();
          var dtoTypeGoods = DTOHelper.ConvertTypeGoodsDTO(rsTypeGoods);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoTypeGoods, searchKey));
          break;
        case EnumTypeMasterData.Warehouse:
          var rsWarehouse = await AppCore.Ins._warehouseService.GetAllAsync();
          var dtoWarehouse = DTOHelper.ConvertWareHouseDTO(rsWarehouse);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoWarehouse, searchKey));
          break;
        case EnumTypeMasterData.Tare:
          var rsTare = await AppCore.Ins._categoryTareService.GetAllAsync();
          var dtoTare = DTOHelper.ConvertCategoryTareDTO(rsTare);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoTare, searchKey));
          break;
        case EnumTypeMasterData.GroupProduct:
          var rsGroupProduct = await AppCore.Ins._productGroupService.GetAllAsync();
          var dtoGroupProduct = DTOHelper.ConvertProductGroupDTO(rsGroupProduct);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoGroupProduct, searchKey));
          break;
        case EnumTypeMasterData.Product:
          var rsProduct = await AppCore.Ins._productService.GetAllAsync();
          var dtoProduct = DTOHelper.ConvertProductDTO(rsProduct);
          SetDgv(enumTypeMaster, FilterBySearchKey(dtoProduct, searchKey));
          break;
        default:
          break;
      }
    }

    private static List<T> FilterBySearchKey<T>(List<T>? values, string searchKey)
    {
      if (values == null)
        return new List<T>();

      if (string.IsNullOrWhiteSpace(searchKey))
        return values;

      var searchableProperties = TypeDescriptor.GetProperties(typeof(T))
        .Cast<PropertyDescriptor>()
        .Where(property => property.IsBrowsable)
        .ToArray();

      return values.Where(item => searchableProperties.Any(property =>
      {
        string? value = property.GetValue(item)?.ToString();
        return value?.Contains(searchKey, StringComparison.CurrentCultureIgnoreCase) == true;
      })).ToList();
    }

    private async void btnSearch_Click(object sender, EventArgs e)
    {
      await LoadData(_enumTypeMasterDataCurrent);
    }

    private async void txtSearch_TextChanged(object? sender, EventArgs e)
    {
      _searchDebounceCancellation?.Cancel();
      _searchDebounceCancellation?.Dispose();

      var cancellation = new CancellationTokenSource();
      _searchDebounceCancellation = cancellation;

      try
      {
        await Task.Delay(300, cancellation.Token);
        await LoadData(_enumTypeMasterDataCurrent);
      }
      catch (OperationCanceledException)
      {
        // Người dùng vẫn đang nhập, chờ lần thay đổi mới nhất.
      }
      finally
      {
        if (ReferenceEquals(_searchDebounceCancellation, cancellation))
        {
          _searchDebounceCancellation.Dispose();
          _searchDebounceCancellation = null;
        }
      }
    }

    private void btnAddnew_Click(object sender, EventArgs e)
    {
      if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
      {
        PopupAddClient popupAddClient = new PopupAddClient();
        popupAddClient.OnSendSuccess += PopupAddClient_OnSendAddSuccess;
        popupAddClient.ShowDialog();
      }  
    }

    private async void PopupAddClient_OnSendAddSuccess(Client client)
    {
      try
      {
        ClientUpsertRequest clientUpsertRequest = new ClientUpsertRequest();
        clientUpsertRequest.Id = client.Id;
        clientUpsertRequest.Name = client?.Name??string.Empty;
        clientUpsertRequest.Description = client?.Description ?? string.Empty;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(clientUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_Client;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);
      }
      catch (Exception ex)
      {

      }
    }

    public void SetDgv<T>(EnumTypeMasterData enumTypeMasterData, List<T>? values)
    {
      dgv.DataSource = null;
      dgv.DataSource = values;
      EnsureActionColumns();

      if (enumTypeMasterData == EnumTypeMasterData.Client)
      {
        var autoSizeColumns = new[]
         {
            nameof(ClientDTO.No),
            nameof(ClientDTO.UpdatedAt),
          };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
            nameof(ClientDTO.No),
          };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ClientDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.TypeGoods)
      {
        var autoSizeColumns = new[]
        {
          nameof(TypeGoodsDTO.No),
          nameof(TypeGoodsDTO.Code),
          nameof(TypeGoodsDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
            nameof(TypeGoodsDTO.No),
          };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(TypeGoodsDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Warehouse)
      {
        var autoSizeColumns = new[]
        {
          nameof(WareHouseDTO.No),
          nameof(WareHouseDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(WareHouseDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(WareHouseDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Tare)
      {
        var autoSizeColumns = new[]
        {
          nameof(CategoryTareDTO.No),
          nameof(CategoryTareDTO.Code),
          nameof(CategoryTareDTO.UpdatedAt),
          nameof(CategoryTareDTO.Value),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(CategoryTareDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(CategoryTareDTO.Value),
          nameof(CategoryTareDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.GroupProduct)
      {
        var autoSizeColumns = new[]
        {
          nameof(CategoryTareDTO.No),
          nameof(ProductGroupDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(ProductGroupDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ProductGroupDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Product)
      {
        var autoSizeColumns = new[]
        {
          nameof(ProductDTO.No),
          nameof(ProductDTO.UpdatedAt),
          nameof(ProductDTO.Group),
          nameof(ProductDTO.Code),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(ProductDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ProductDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
    }

    private void EnsureActionColumns()
    {
      if (!dgv.Columns.Contains(EditButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = EditButtonColumnName,
          HeaderText = "",
          Text = "Chỉnh sửa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 130,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains(DeleteButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = DeleteButtonColumnName,
          HeaderText = "",
          Text = "Xóa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 100,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      dgv.Columns[EditButtonColumnName].DisplayIndex = dgv.Columns.Count - 2;
      dgv.Columns[DeleteButtonColumnName].DisplayIndex = dgv.Columns.Count - 1;
    }

    private async void dgv_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

      var columnName = dgv.Columns[e.ColumnIndex].Name;
      var rowData = dgv.Rows[e.RowIndex].DataBoundItem;

      if (columnName == EditButtonColumnName)
        await EditMasterDataAsync(rowData);
      else if (columnName == DeleteButtonColumnName)
        await DeleteMasterDataAsync(rowData);
    }

    private Task EditMasterDataAsync(object? rowData)
    {
      if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
      {
        var data = rowData as ClientDTO;
        if (data?.Client != null)
        {
          PopupAddClient popupUpdateClient = new PopupAddClient(data.Client);
          popupUpdateClient.OnSendSuccess += PopupUpdateClient_OnSendAddSuccess;
          popupUpdateClient.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }  
      }


      return Task.CompletedTask;
    }

    private async void PopupUpdateClient_OnSendAddSuccess(Client client)
    {
      try
      {
        ClientUpsertRequest clientUpsertRequest = new ClientUpsertRequest();
        clientUpsertRequest.Id = client.Id;
        clientUpsertRequest.Name = client?.Name ?? string.Empty;
        clientUpsertRequest.Description = client?.Description ?? string.Empty;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(clientUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_Client;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);
      }
      catch (Exception ex)
      {

      }
    }

    private Task DeleteMasterDataAsync(object? rowData)
    {
      return Task.CompletedTask;
    }
  }
}
