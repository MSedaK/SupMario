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

    // Animator referans�
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    private void Update()
    {
        // Dinamik joystick'ten yatay hareket i�in de�er al
        float moveInput = dynamicJoystick.Horizontal;

        // Oyuncunun sadece sa�a sola hareketi
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Karakterin y�n�n� ayarla (sa�a/sola d�nme)
        if (moveInput > 0)
        {
            // Sa�a bak
            transform.localScale = new Vector3(1, 1, 1); // Orijinal y�n
        }
        else if (moveInput < 0)
        {
            // Sola bak
            transform.localScale = new Vector3(-1, 1, 1); // Y ekseninde aynalama
        }

        // Z�plama sabit joystick'e bas�larak tetiklenir
        if (fixedJoystick.Vertical > 0.5f && isGrounded)
        {
            Jump();
        }

        // Animator parametrelerini g�ncelle
        animator.SetBool("isJumping", !isGrounded); // Z�plama durumu
        animator.SetFloat("Speed", Mathf.Abs(moveInput)); // Hareket durumu
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
