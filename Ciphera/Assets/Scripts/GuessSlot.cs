using UnityEngine;

public class GuessSlot : MonoBehaviour
{
    private SpriteRenderer sr;

    public enum FeedbackType
    {
        None,
        Correct,
        WrongPosition,
        Wrong
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ResetSlot()
    {
        sr.color = Color.white;
    }

    public void SetFeedback(FeedbackType type)
    {
        switch (type)
        {
            case FeedbackType.Correct:
                sr.color = Color.green;
                break;

            case FeedbackType.WrongPosition:
                sr.color = Color.yellow;
                break;

            case FeedbackType.Wrong:
                sr.color = Color.gray;
                break;

            default:
                sr.color = Color.white;
                break;
        }
    }
}
