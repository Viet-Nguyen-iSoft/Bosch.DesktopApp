using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTP.Truck.UserControls
{
  public partial class UcPage : UserControl
  {
    private int _currentPage = 1;
    private int _pageSize = 50;
    private int _totalRecords;

    public event EventHandler<PageChangedEventArgs>? PageChanged;

    public int CurrentPage => _currentPage;
    public int PageSize => _pageSize;
    public int TotalRecords => _totalRecords;
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)_pageSize));

    public UcPage()
    {
      InitializeComponent();

      cbbNumberRecord.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbNumberRecord.SelectedItem = _pageSize.ToString();
      cbbNumberRecord.SelectedIndexChanged += cbbNumberRecord_SelectedIndexChanged;
      btnPrevious.Click += btnPrevious_Click;
      btnNext.Click += btnNext_Click;
      UpdateUI();
    }

    public void SetTotalRecords(int totalRecords, int requestedPage)
    {
      _totalRecords = Math.Max(0, totalRecords);
      _currentPage = Math.Clamp(requestedPage, 1, TotalPages);
      UpdateUI();
    }

    public void ResetToFirstPage()
    {
      _currentPage = 1;
      UpdateUI();
    }

    private void btnPrevious_Click(object? sender, EventArgs e)
    {
      if (_currentPage <= 1)
        return;

      _currentPage--;
      UpdateUI();
      PageChanged?.Invoke(this, new PageChangedEventArgs(_currentPage, _pageSize));
    }

    private void btnNext_Click(object? sender, EventArgs e)
    {
      if (_currentPage >= TotalPages)
        return;

      _currentPage++;
      UpdateUI();
      PageChanged?.Invoke(this, new PageChangedEventArgs(_currentPage, _pageSize));
    }

    private void cbbNumberRecord_SelectedIndexChanged(object? sender, EventArgs e)
    {
      if (!int.TryParse(cbbNumberRecord.SelectedItem?.ToString(), out var pageSize) || pageSize <= 0)
        return;

      _pageSize = pageSize;
      _currentPage = 1;
      UpdateUI();
      PageChanged?.Invoke(this, new PageChangedEventArgs(_currentPage, _pageSize));
    }

    private void UpdateUI()
    {
      lbInforPage.Text = $"{_currentPage}/{TotalPages}";
      lbTotal.Text = _totalRecords.ToString("N0");
      btnPrevious.Enabled = _currentPage > 1;
      btnNext.Enabled = _currentPage < TotalPages;
    }
  }

  public sealed class PageChangedEventArgs : EventArgs
  {
    public PageChangedEventArgs(int pageNumber, int pageSize)
    {
      PageNumber = pageNumber;
      PageSize = pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
  }
}
