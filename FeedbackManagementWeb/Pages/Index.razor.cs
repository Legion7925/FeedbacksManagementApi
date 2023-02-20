using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace FeedbackManagementWeb.Pages
{
    partial class Index
    {
        [Inject]
        private ICaseService CaseService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<CaseReport> casesList = new List<CaseReport>();

        private bool _loading;

        private HashSet<CaseReport> selectedCases = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCases();
        }

        public async Task GetCases()
        {
            try
            {
                casesList = await CaseService.GetCases(int.MaxValue, 0);
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

        public async Task DeleteSelectedCases()
        {
            try
            {
                var caseIds = selectedCases.Select(i => i.Id).ToArray();
                if (!caseIds.Any())
                {
                    Snackbar.Add("موردی برای حذف انتخاب نشده است", Severity.Warning);
                    return;
                }
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف موارد انتخابی اطمینان دارید ");
                var dialodDelete = DialogService.Show<DeleteDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await CaseService.DeleteCases(caseIds);
                    Snackbar.Add("مورد با موفقیت حذف شد", Severity.Success);
                    await GetCases();
                }
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public async Task SendCasesForAnswer()
        {
            try
            {
                var caseIds = selectedCases.Select(i => i.Id).ToArray();
                if (!caseIds.Any())
                {
                    Snackbar.Add("موردی برای ارسال انتخاب نشده است", Severity.Warning);
                    return;
                }
                foreach (var id in caseIds)
                {
                    await CaseService.SubmitCaseForAnswer(id);
                }
                Snackbar.Add("موارد انتخابی با موفقیت برای پاسخ دهی ارسال شدند", Severity.Success);
                await GetCases();
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        private async void OpenAddCaseDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            var addDialog = DialogService.Show<AddCaseDialog>("افزودن کاربر", options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
                await GetCases();
        }

        public void ShowCaseDetails(CaseReport @case)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, NoHeader = true, MaxWidth = MaxWidth.ExtraLarge };
            var parameters = new DialogParameters();
            parameters.Add("Case", @case);
            DialogService.Show<CaseDetailsDialog>(string.Empty, parameters, options);
        }
    }
}
