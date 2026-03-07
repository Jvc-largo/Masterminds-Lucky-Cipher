using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Random answer generation
    public Sprite[] optionSprites;
    public Sprite[] answerSequence;
    // Clicking logic
    public Transform[] guessSlots;
    public Sprite[] playerGuess;

    private int currentSlot = 0;

    void Start()
    {
        answerSequence = new Sprite[guessSlots.Length];
        playerGuess = new Sprite[guessSlots.Length];

        for (int i = 0; i < answerSequence.Length; i++)
        {
            answerSequence[i] = optionSprites[Random.Range(0, optionSprites.Length)];
        }

        // For debugging, print the answer
        string answerStr = "";
        foreach (var sprite in answerSequence)
        {
            answerStr += sprite.name + " ";
        }

        Debug.Log("Answer sequence: " + answerStr);
    }

    public void PlaceObject(Sprite selectedSprite)
    {
        if (currentSlot >= guessSlots.Length)
        {
            Debug.Log("Guess row full");
            return;
        }
        GameObject newObject = new GameObject("GuessItem");

        SpriteRenderer sr = newObject.AddComponent<SpriteRenderer>();
        sr.sprite = selectedSprite;
        
        newObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // prevents it from blocking clicks

        newObject.transform.SetParent(guessSlots[currentSlot]);
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        playerGuess[currentSlot] = selectedSprite;

        currentSlot++;
    }

    //Submit button
    public void SubmitGuess()
    {   
        if (currentSlot < guessSlots.Length)
        {
            Debug.Log("Not enough objects placed!");
            return;
        }
        Debug.Log("Checking guess");
    }

    //Delete button
    public void DeleteGuess()
    {   
        if (currentSlot < guessSlots.Length)
        {
            Debug.Log("Not enough objects placed!");
            return;
        }
        Debug.Log("Checking guess");
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Return))
    {
        SubmitGuess();
    }

    if (Input.GetKeyDown(KeyCode.Delete))
    {
        DeleteGuess();
    }
}

}
