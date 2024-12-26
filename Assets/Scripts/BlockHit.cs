using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI elemanlar�n� kontrol etmek i�in

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;

    // Coin sayac�
    public static int coinCount = 0; // Bu coin say�s�n� tutar
    public Text coinText; // Canvas'taki Text bile�enine referans

    private void Start()
    {
        if (coinText != null)
        {
            // Ba�lang��ta coin say�s�n� g�ncelle
            UpdateCoinText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.tag == "Coin")
            {
                IncreaseCoinCount();
            }

            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // blo�u g�ster (e�er gizli ise)

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

    // Coin say�s�n� art�rma fonksiyonu
    private void IncreaseCoinCount()
    {
        coinCount++; // Coin say�s�n� 1 art�r
        Debug.Log("Coin collected! Current coin count: " + coinCount); // Debug mesaj� yazd�r
        UpdateCoinText(); // Text'i g�ncelle
    }


    // UI'deki coin say�s�n� g�ncelleme
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount.ToString();
        }
    }
}
