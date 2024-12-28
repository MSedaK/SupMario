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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Dinamik joystick'ten yatay hareket için deðer al
        float moveInput = dynamicJoystick.Horizontal;

        // Oyuncunun sadece saða sola hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Zýplama sabit joystick'e basýlarak tetiklenir
        if (fixedJoystick.Vertical > 0.5f && isGrounded)
        {
            Jump();
        }
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
