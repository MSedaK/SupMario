using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket hýzý
    public float jumpForce = 8f; // Oyuncunun zýplama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;

    // Dinamik joystick referansý (sadece saða sola hareket için)
    public DynamicJoystick dynamicJoystick;

    // Sabit joystick referansý (zýplama için)
    public FixedJoystick fixedJoystick;

    // Animator referansý
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    private void Update()
    {
        // Dinamik joystick'ten yatay hareket için deðer al
        float moveInput = dynamicJoystick.Horizontal;

        // Oyuncunun sadece saða sola hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Karakterin yönünü ayarla (saða/sola dönme)
        if (moveInput > 0)
        {
            // Saða bak
            transform.localScale = new Vector3(1, 1, 1); // Orijinal yön
        }
        else if (moveInput < 0)
        {
            // Sola bak
            transform.localScale = new Vector3(-1, 1, 1); // Y ekseninde aynalama
        }

        // Zýplama sabit joystick'e basýlarak tetiklenir
        if (fixedJoystick.Vertical > 0.5f && isGrounded)
        {
            Jump();
        }

        // Animator parametrelerini güncelle
        animator.SetBool("isJumping", !isGrounded); // Zýplama durumu
        animator.SetFloat("Speed", Mathf.Abs(moveInput)); // Hareket durumu
    }

    public void Jump()
    {
        // Oyuncunun yukarý doðru zýplamasýný saðlar
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncunun zemine temasý kontrol edilir
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Oyuncu zeminle temasýný kaybettiðinde
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
