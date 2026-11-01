namespace Wks.UseCases;

public class CreateFeedback(IFeedbackRepository _feedbackRepository)
    : ICreateFeedback
{
    private static readonly int LENGHT_MAX = 12;

    public Feedback Execute(
        Guid profileId,
        FeedbackObject.Create createParams
    ) =>
        _feedbackRepository.Create(
            new Feedback(
                profileId,
                createParams.Name,
                Cryptography.RandomHexString(LENGHT_MAX).ToUpper(),
                createParams.Contact,
                createParams.Content
            )
        );
}
