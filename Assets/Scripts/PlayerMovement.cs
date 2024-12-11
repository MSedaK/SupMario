using Terresquall;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket h�z�
    public float jumpForce = 8f; // Oyuncunun z�plama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;

    // Joystick ba�lant�s�
    public VirtualJoystick movementJoystick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Joystick'ten yatay hareket i�in de�er al
        float moveInput = movementJoystick.axis.x;

        // Oyuncunun hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Z�plama joystick'in yukar� ekseniyle tetiklenir
        if (movementJoystick.axis.y > 0.5f && isGrounded)
        {
            Jump();
        }
    }

    public void Jump()
    {
        // Oyuncunun yukar� do�ru z�plamas�n� sa�lar
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncunun zemine temas�n� kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Oyuncu zeminle temas�n� kaybetti�inde
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
