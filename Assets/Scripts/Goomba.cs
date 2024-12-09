using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.starpower)
            {
                Hit(); // Starpower aktifse d��man �l�r.
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten(); // �st�ne bas�ld�ysa d�zle�ir.
            }
            else
            {
                player.Hit(); // Player d��mana �arpt���nda hasar al�r.
            }
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 0.5f);
    }
}

