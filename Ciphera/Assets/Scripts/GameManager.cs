using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs and Answer")]
    public GameObject[] optionPrefabs;        // Drag your clickable prefabs here
    public GameObject[] guessSlots;           // Assign your guess slot transforms
    public Sprite[] answerSequence;           // Automatically generated answer
    public Sprite[] playerGuess;              // Current player guesses

    private int currentSlot = 0;

    void Start()
    {
        // Initialize arrays based on the number of guess slots
        answerSequence = new Sprite[guessSlots.Length];
        playerGuess = new Sprite[guessSlots.Length];

        // Generate random answer sequence
        for (int i = 0; i < answerSequence.Length; i++)
        {
            Sprite sprite = optionPrefabs[Random.Range(0, optionPrefabs.Length)]
                                .GetComponent<SpriteRenderer>().sprite;
            answerSequence[i] = sprite;
        }

        // Debug print
        string answerStr = "";
        foreach (var s in answerSequence)
            answerStr += s.name + " ";
        Debug.Log("Answer sequence: " + answerStr);
    }

    // Called by clicking a prefab
    // Replace PlaceObject(int prefabIndex) with this:
public void PlaceObject(GameObject prefab)
{
    if (currentSlot >= guessSlots.Length)
    {
        Debug.Log("Guess row full!");
        return;
    }

    // Instantiate the prefab directly
    GameObject newObject = Instantiate(prefab, guessSlots[currentSlot].transform);
    newObject.transform.localPosition = Vector3.zero;
    newObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

    // Store the sprite for checking
    playerGuess[currentSlot] = newObject.GetComponent<SpriteRenderer>().sprite;

    currentSlot++;
}

    // Delete last placed object
    public void DeleteLast()
    {
        if (currentSlot <= 0)
        {
            Debug.Log("Nothing to delete!");
            return;
        }

        currentSlot--;
        Transform slot = guessSlots[currentSlot].transform;

        if (slot.childCount > 0)
            Destroy(slot.GetChild(0).gameObject);

        playerGuess[currentSlot] = null;
    }

    // Submit the guess
    public void SubmitGuess()
    {
        if (currentSlot < guessSlots.Length)
        {
            Debug.Log("Fill all slots first!");
            return;
        }

        CheckGuess();
    }

    // Compare player guess to answer
    void CheckGuess()
    {
        int correctPosition = 0;
        int correctSpriteWrongPlace = 0;

        bool[] answerUsed = new bool[answerSequence.Length];

        // Correct positions
        for (int i = 0; i < playerGuess.Length; i++)
        {
            if (playerGuess[i] == answerSequence[i])
            {
                correctPosition++;
                answerUsed[i] = true;
            }
        }

        // Correct sprite, wrong position
        for (int i = 0; i < playerGuess.Length; i++)
        {
            if (playerGuess[i] == answerSequence[i])
                continue;

            for (int j = 0; j < answerSequence.Length; j++)
            {
                if (!answerUsed[j] && playerGuess[i] == answerSequence[j])
                {
                    correctSpriteWrongPlace++;
                    answerUsed[j] = true;
                    break;
                }
            }
        }

        Debug.Log("Correct Position: " + correctPosition);
        Debug.Log("Correct Sprite Wrong Place: " + correctSpriteWrongPlace);
    }

    // Optional: Reset for a new row
    public void ResetGuessRow()
    {
        while (currentSlot > 0)
            DeleteLast();
    }
}
