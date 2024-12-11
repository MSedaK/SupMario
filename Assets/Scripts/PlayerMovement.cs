using Terresquall;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket hýzý
    public float jumpForce = 8f; // Oyuncunun zýplama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;

    // Joystick baðlantýsý
    public VirtualJoystick movementJoystick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Joystick'ten yatay hareket için deðer al
        float moveInput = movementJoystick.axis.x;

        // Oyuncunun hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Zýplama joystick'in yukarý ekseniyle tetiklenir
        if (movementJoystick.axis.y > 0.5f && isGrounded)
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
        // Oyuncunun zemine temasýný kontrol et
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
