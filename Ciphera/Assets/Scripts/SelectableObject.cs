using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnMouseDown()
    {
        // Pass the actual prefab instead of an index
        gameManager.PlaceObject(gameObject);
    }
}
