using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Added for SequenceEqual

public class GameBoardManager : MonoBehaviour
{
    public List<RowData> rows; // Using a simple class wrapper for easier Unity Inspector visibility
    private int currentRow = 0;
    private int currentCol = 0;
    private int maxCols = 3;
    private int maxRows = 4;

    private List<string> targetSequence;
    private string[] possibleColors = { "Red", "Yellow", "Green", "Blue" };

    void Start()
    {
        GenerateTargetSequence();
    }

    private void GenerateTargetSequence()
    {
        targetSequence = new List<string>();
        for (int i = 0; i < maxCols; i++)
        {
            // Generates a random sequence of 3 colors
            targetSequence.Add(possibleColors[Random.Range(0, possibleColors.Length)]);
        }
        Debug.Log("Target Sequence: " + string.Join(", ", targetSequence));
    }

    public void AddWord(string colorName)
    {
        if (currentRow < maxRows && currentCol < maxCols)
        {
            rows[currentRow].tiles[currentCol].SetText(colorName);
            currentCol++;
        }
    }

    public void DeleteWord()
    {
        if (currentCol > 0)
        {
            currentCol--;
            rows[currentRow].tiles[currentCol].ClearText();
        }
    }

    public void SubmitRow()
    {
        // Only allow submit if the row is actually full
        if (currentCol == maxCols)
        {
            List<string> playerInput = new List<string>();
            foreach (var tile in rows[currentRow].tiles)
            {
                playerInput.Add(tile.GetText());
            }

            if (ValidateSequence(playerInput))
            {
                Debug.Log("Row " + (currentRow + 1) + " Correct! Level Complete.");
                // Add success logic here (e.g., lock the game or move to next level)
            }
            else
            {
                Debug.Log("Row " + (currentRow + 1) + " Incorrect.");
                MoveToNextRow();
            }
        }
    }

    private bool ValidateSequence(List<string> seq)
    {
        // SequenceEqual checks both the content AND the order
        return seq.SequenceEqual(targetSequence);
    }

    private void MoveToNextRow()
    {
        currentRow++;
        currentCol = 0;

        if (currentRow >= maxRows)
        {
            Debug.Log("Game Over: Out of rows!");
            // Handle failure state here
        }
    }
}

[System.Serializable]
public class RowData
{
    public List<Tile> tiles;
}