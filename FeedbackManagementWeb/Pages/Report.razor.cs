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

        private FeedbackReportFilterModel filter = new() { Created = null, Source = null, Priorty = null, RespondDate = null, ReferralDate = null };

        private bool firstRender = true;

        private DateTime? Created;
        private DateTime? RespondDate;
        //private DateTime? Updated;

        IEnumerable<Product> products= new List<Product>();
        IEnumerable<Specialty> specialties= new List<Specialty>();
        IEnumerable<Customer> customers= new List<Customer>();

        public async Task<TableData<FeedbackReport>> GetFeedbackReport(TableState state)
        {
            if (firstRender)
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            try
            {
                var totalItems = await FeedbackService.GetFeedbackCount();
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
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
    }
}
