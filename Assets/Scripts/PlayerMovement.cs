using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Oyuncunun hareket h�z�
    public float jumpForce = 8f; // Oyuncunun z�plama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;

    // Dinamik joystick referans� (sadece sa�a sola hareket i�in)
    public DynamicJoystick dynamicJoystick;

    // Sabit joystick referans� (z�plama i�in)
    public FixedJoystick fixedJoystick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Dinamik joystick'ten yatay hareket i�in de�er al
        float moveInput = dynamicJoystick.Horizontal;

        // Oyuncunun sadece sa�a sola hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Z�plama sabit joystick'e bas�larak tetiklenir
        if (fixedJoystick.Vertical > 0.5f && isGrounded)
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
        // Oyuncunun zemine temas� kontrol edilir
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
