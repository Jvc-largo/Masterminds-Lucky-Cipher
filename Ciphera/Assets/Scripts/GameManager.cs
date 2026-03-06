using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Random answer generation
    public Sprite[] optionSprites;
    
    // Clicking logic
    public Transform[] guessSlots;
    private int currentSlot = 0;

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

        newObject.transform.position = guessSlots[currentSlot].position;

        currentSlot++;
    }
}
