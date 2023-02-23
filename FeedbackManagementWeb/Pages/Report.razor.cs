using Domain.Entities;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class Report
    {
        [Inject]
        private IFeedbackService FeedbackService { get; set; } = default!;
        [Inject]
        private IReportService ReportService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<FeedbackReport> feedbackList = new List<FeedbackReport>();

        private bool _loading = false;

        private HashSet<FeedbackReport> selectedFeedbacks = new();

        private readonly int[] _pageSizeOption = { 10, 20, 30 };

        private MudTable<FeedbackReport>? _table = new();

        private FeedbackReportFilterModel filter = new();

        private bool firstRender = true;

        private DateTime? Created;
        private DateTime? RespondDate;
        //private DateTime? Updated;

        IEnumerable<Product> products= new List<Product>();
        IEnumerable<Specialty> specialties= new List<Specialty>();
        IEnumerable<Customer> customers= new List<Customer>();

        private string customerNameFilter = string.Empty;
        private string productNameFilter = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender is not true)
                return;

            await GetAppLists();
        }

        public async Task<TableData<FeedbackReport>> GetFeedbackReport(TableState state)
        {
            if (firstRender)
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            try
            {
                var totalItems = await FeedbackService.GetFeedbackReportCount(filter);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    Snackbar.Add("داده ای برای نمایش یافت نشد", Severity.Info);
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }

                filter.Skip = state.Page * state.PageSize;
                filter.Take = state.PageSize;
                filter.Created = Created;
                filter.RespondDate = RespondDate;
                feedbackList = await FeedbackService.GetFeedbackReport(filter);

                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

        private async Task GetReport()
        {
            filter.CustomerId = customers.FirstOrDefault(i => i.NameAndFamily == customerNameFilter)?.Id ?? 0;
            filter.ProductId = products.FirstOrDefault(i => i.Name == productNameFilter)?.Id ?? 0;
            firstRender = false;
            await _table!.ReloadServerData();
        }

        private async Task GetAppLists()
        {
            try
            {
                products = await ReportService.GetProdcuts();
                customers = await ReportService.GetCustomers();
                specialties = await ReportService.GetSpecialties();
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        private async Task<IEnumerable<string>> SearchCustomerName(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
            {
                return customers.Select(i => i.NameAndFamily);
            }

            return customers.Where(i => i.NameAndFamily
            .Contains(value, StringComparison.OrdinalIgnoreCase)).Select(u => u.NameAndFamily);
        }  

        private async Task<IEnumerable<string>> SearchProductName(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
            {
                return products.Select(i => i.Name);
            }

            return products.Where(i => i.Name
            .Contains(value, StringComparison.OrdinalIgnoreCase)).Select(u => u.Name);
        }
    }
}
