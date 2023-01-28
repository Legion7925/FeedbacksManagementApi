namespace FeedbacksManagementApi.Helper.Enums;

public enum FeedbackState : byte
{
    /// <summary>
    /// آماده ارسال
    /// </summary>
    ReadyToSend = 0,
    /// <summary>
    /// ارسال شده به متخصص
    /// </summary>
    SentToExpert = 1,
    /// <summary>
    /// حذف شده
    /// </summary>
    Deleted = 2,
    /// <summary>
    /// بایگانی شده
    /// </summary>
    Archived = 3,
}
