using Domain.Entities;

namespace Domain.Interfaces;

public interface ICasesRepository
{
    Task AddCase(CaseBase feedbackCase);
    Task DeleteCase(int caseId);
    Task DeleteMultipleCases(int[] caseIds);
    Task<Case?> GetCaseById(int caseId);
    IEnumerable<Case> GetCases();
    Task UpdateCase(CaseBase feedbackCase, int caseId);
    Task SubmitForRespond(int caseId);
}
