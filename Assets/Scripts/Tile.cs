using UnityEngine;
using TMPro; // if you're using TextMeshPro

public class Tile : MonoBehaviour
{
    public TextMeshProUGUI text; // assign in Inspector

    public void SetText(string value)
    {
        if (text != null)
            text.text = value;
    }

    public void ClearText()
    {
        if (text != null)
            text.text = "";
    }

    public string GetText()
    {
        return text != null ? text.text : "";
    }
}