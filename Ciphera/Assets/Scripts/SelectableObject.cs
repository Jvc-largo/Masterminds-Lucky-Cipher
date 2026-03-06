using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer sr;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        gameManager.PlaceObject(sr.sprite);
    }
}
