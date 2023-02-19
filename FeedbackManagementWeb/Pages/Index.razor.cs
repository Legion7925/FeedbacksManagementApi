using FeedbackManagementWeb.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class Index
    {
        [Inject]
        private ICaseService CaseService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        private IEnumerable<Case> casesList  = new List<Case>();

        private bool _loading;

        private HashSet<Case> selectedCases = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            _loading = true;
            await GetCases();
        }

        public async Task GetCases()
        {
            try
            {
                casesList = await CaseService.GetCases();
                _loading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                _loading = false;
            }
        }

        public void CaseCheckChanged(ChangeEventArgs e)
        {
            //var value = (bool?)e.Value ?? false;
            //if (value)
            //{
            //    selectedCases.Add()
            //}

        }
    }
}
