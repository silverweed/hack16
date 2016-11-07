using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour
{
    public Sprite[] bubbleSprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bubbleSprites[Random.Range(0, bubbleSprites.Length)];
    }
}
