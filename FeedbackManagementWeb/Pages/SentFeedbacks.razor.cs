using FeedbackManagementWeb.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class SentFeedbacks
    {
        [Inject]
        private IFeedbackService FeedbackService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<FeedbackReport> feedbackList = new List<FeedbackReport>();

        private bool _loading = false;

        private HashSet<FeedbackReport> selectedFeedbacks = new();

        private readonly int[] _pageSizeOption = { 10, 20, 30 };

        private MudTable<FeedbackReport>? _table = new();

        private bool isFeedbackCompleted = false;

        public async Task<TableData<FeedbackReport>> GetFeedbacks(TableState state)
        {

            try
            {
                var totalItems = await FeedbackService.GetFeedbackCount();
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }
                if (isFeedbackCompleted)
                    feedbackList = await FeedbackService.GetFeedbacks(state.PageSize, state.Page * state.PageSize, FeedbackState.Answered);
                else
                    feedbackList = await FeedbackService.GetFeedbacks(state.PageSize, state.Page * state.PageSize, FeedbackState.SentToExpert);

                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

        private async void SelectedValueChanged(ChangeEventArgs e)
        {
            await _table!.ReloadServerData();
            StateHasChanged();
        }
    }
}
