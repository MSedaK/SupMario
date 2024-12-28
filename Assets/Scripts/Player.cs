using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 0.2f;
    [SerializeField] private float waitingDuration = 4f;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }
    public Animator animator;

    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !IsGrounded());
        animator.SetBool("IsGrounded", IsGrounded());

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            animator.SetBool("IsCrouching", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !starpower)
        {
            ContactPoint2D[] contacts = collision.contacts;

            if (contacts.Length > 0)
            {
                Vector2 contactNormal = contacts[0].normal;

                if (contactNormal.y > 0.5f)
                {
                    Destroy(collision.gameObject);
                    return;
                }
                else
                {
                    Hit();
                }
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !starpower)
        {
            Hit();
        }
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
            animator.SetBool("IsJumping", true);
        }
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
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // Ölüm animasyonu için bekle
        yield return new WaitForSeconds(3f);

        // Can sayısını kontrol et
        if (GameManager.Instance.lives <= 1)
        {
            // GameOver sahnesine geç
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Hala can varsa normal reset işlemi
            GameManager.Instance.ResetLevel(0f);
        }
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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    private IEnumerator MakePlayerBiggerCoroutine()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1f), scaleDuration).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(waitingDuration);
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.InBack);
    }

    public void MakePlayerBigger()
    {
        StartCoroutine(MakePlayerBiggerCoroutine());
    }
}

