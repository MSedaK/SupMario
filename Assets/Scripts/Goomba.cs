using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite; // Düzleþmiþ düþman için kullanýlacak sprite
    private SpriteRenderer spriteRenderer; // SpriteRenderer bileþeni

    private void Awake()
    {
        // SpriteRenderer bileþenini alýyoruz
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("Player detected!");
            if (player.starpower)
            {
                Flatten();
            }
            else
            {
                Vector2 contactNormal = collision.contacts[0].normal;

                if (contactNormal.y < -0.5f)
                {
                    Debug.Log("Top collision detected!");
                    Hit();
                }
                else
                {
                    Debug.Log("Side collision detected!");
                    player.Hit();
                }
            }
        }
    }

    private void Hit()
    {
        Debug.Log("Hit called: Changing sprite...");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;

        if (spriteRenderer != null && flatSprite != null)
        {
            spriteRenderer.sprite = flatSprite;
            Debug.Log("Sprite changed to flatSprite.");
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or flatSprite is missing!");
        }

        Destroy(gameObject, 0.5f);
    }


    private void Flatten()
    {
        // Starpower ile düþman yok edilir (animasyonlu yok etme)
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        Destroy(gameObject, 0.5f); // 0.5 saniye sonra düþmaný yok et
    }
}
