using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs and Answer")]
    public GameObject[] optionPrefabs;        // Your clickable prefabs
    public GameObject guessSlotPrefab;        // Prefab for one slot (empty square)
    public Transform guessRowParent;          // Parent for all guess rows
    public int slotsPerRow = 4;               // How many tiles per row
    public int maxRows = 6;                   // How many guess rows player can use

    private Sprite[] answerSequence;          // Random answer sequence
    private Sprite[] playerGuess;             // Current guess row
    private Transform[] currentRowSlots;      // Current row’s slots
    private int currentSlot = 0;              // Index in current row

    void Start()
    {
        GenerateAnswerSequence();
        CreateRows();  // Create all rows under guessRowParent
    }

    void GenerateAnswerSequence()
    {
        answerSequence = new Sprite[slotsPerRow];

        for (int i = 0; i < slotsPerRow; i++)
        {
            Sprite sprite = optionPrefabs[Random.Range(0, optionPrefabs.Length)]
                                .GetComponent<SpriteRenderer>().sprite;
            answerSequence[i] = sprite;
        }

        string answerStr = "";
        foreach (var s in answerSequence)
            answerStr += s.name + " ";
        Debug.Log("Answer sequence: " + answerStr);
    }

    void CreateRows()
    {
        for (int row = 0; row < maxRows; row++)
        {
            GameObject newRow = new GameObject("GuessRow_" + row);
            newRow.transform.SetParent(guessRowParent);
            newRow.transform.localPosition = new Vector3(0, -row * 1.5f, 0); // vertically stacked

            for (int i = 0; i < slotsPerRow; i++)
            {
                GameObject slot = Instantiate(guessSlotPrefab, newRow.transform);
                slot.name = "Slot_" + i;
                slot.transform.localPosition = new Vector3(i * 1.5f, 0, 0);
            }

            // Only assign the first row to currentRowSlots
            if (row == 0)
            {
                currentRowSlots = newRow.GetComponentsInChildren<Transform>();
                // Filter out the row itself
                Transform[] temp = new Transform[slotsPerRow];
                int idx = 0;
                foreach (var t in currentRowSlots)
                    if (t != newRow.transform)
                        temp[idx++] = t;
                currentRowSlots = temp;

                playerGuess = new Sprite[slotsPerRow];
            }
        }
    }

    // Called by clicking a prefab
    public void PlaceObject(GameObject prefab)
    {
        if (currentSlot >= slotsPerRow)
        {
            Debug.Log("Guess row full!");
            return;
        }

        GameObject newObject = Instantiate(prefab, currentRowSlots[currentSlot]);
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localScale = Vector3.one * 0.35f;

        playerGuess[currentSlot] = newObject.GetComponent<SpriteRenderer>().sprite;
        currentSlot++;
    }

    public void DeleteLast()
    {
        if (currentSlot <= 0)
        {
            Debug.Log("Nothing to delete!");
            return;
        }

        currentSlot--;
        Transform slot = currentRowSlots[currentSlot];
        if (slot.childCount > 0)
            Destroy(slot.GetChild(0).gameObject);

        playerGuess[currentSlot] = null;
    }

    public void SubmitGuess()
    {
        if (currentSlot < slotsPerRow)
        {
            Debug.Log("Fill all slots first!");
            return;
        }

        CheckGuess();
        AdvanceRow();
    }

    void CheckGuess()
{
    bool[] answerUsed = new bool[answerSequence.Length];

    for (int i = 0; i < playerGuess.Length; i++)
    {
        // safety check
        if (currentRowSlots[i].childCount == 0)
            continue;

        // get the tile inside the slot
        Transform tile = currentRowSlots[i].GetChild(0);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();

        if (playerGuess[i] == answerSequence[i])
        {
            tileRenderer.color = new Color32(64, 225, 137, 225); // green
            answerUsed[i] = true;
        }
        else
        {
            bool found = false;

            for (int j = 0; j < answerSequence.Length; j++)
            {
                if (!answerUsed[j] && playerGuess[i] == answerSequence[j])
                {
                    tileRenderer.color = new Color32(255, 245, 157, 225); // yellow
                    answerUsed[j] = true;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                tileRenderer.color = new Color32(240, 100, 75, 225); // red
            }
        }
    }
}

    void AdvanceRow()
    {
        // Move to next row if available
        Transform parentRow = currentRowSlots[0].parent;
        int nextRowIndex = parentRow.GetSiblingIndex() + 1;

        if (nextRowIndex >= guessRowParent.childCount)
        {
            Debug.Log("No more rows!");
            return;
        }

        Transform nextRow = guessRowParent.GetChild(nextRowIndex);
        currentRowSlots = nextRow.GetComponentsInChildren<Transform>();
        Transform[] temp = new Transform[slotsPerRow];
        int idx = 0;
        foreach (var t in currentRowSlots)
            if (t != nextRow)
                temp[idx++] = t;
        currentRowSlots = temp;

        playerGuess = new Sprite[slotsPerRow];
        currentSlot = 0;
    }
}