using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveSpriteManager : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1920f, 1080f);

    void Start()
    {
        AdjustAllSprites();
    }

    void AdjustAllSprites()
    {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            AdjustSprite(sprite);
        }
    }

    void AdjustSprite(SpriteRenderer sprite)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float scaleX = screenWidth / referenceResolution.x;
        float scaleY = screenHeight / referenceResolution.y;

        sprite.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
