using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }
    public Animator animator;

    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !starpower)
        {
            // �arp��ma noktalar�n� al
            ContactPoint2D[] contacts = collision.contacts;

            // �lk temas noktas�n� kontrol et
            if (contacts.Length > 0)
            {
                // �arp��ma normalini kontrol et
                Vector2 contactNormal = contacts[0].normal;

                // E�er �arp��ma yukar�dan ger�ekle�mi�se
                if (contactNormal.y > 0.5f)
                {
                    // D��man� yok et
                    Destroy(collision.gameObject);
                    return;
                }
                else
                {
                    // Yukar�dan �arp��ma de�ilse hasar al
                    Hit();
                }
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !starpower)
        {
            Hit(); // Alternatif kontrol
        }
    }

    public void Jump()
{
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.velocity = new Vector2(rb.velocity.x, 8f); // Oyuncu yukar� do�ru z�plar
}


    public void Hit()
    {
        if (!dead && !starpower)
        {
            Death();
        }
    }

    public void Death()
    {
        deathAnimation.enabled = true;
        animator.SetTrigger("Die");

        GameManager.Instance.ResetLevel(3f);
    }

    public void Shrink()
    {
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        animator.SetBool("IsBig", false);
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        starpower = false;
    }
}
