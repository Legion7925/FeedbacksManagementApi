namespace FeedbackManagementWeb.Interface
{
    public interface ICaseService
    {
        Task AddCase(CaseBase @case);
        Task DeleteCases(int[] caseIds);
        Task<IEnumerable<Case>> GetCases();
        Task SubmitCaseForAnswer(int caseId);
        Task UpdateCase(CaseBase @case, int caseId);

    }
}
