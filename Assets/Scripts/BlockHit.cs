using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI elemanlarýný kontrol etmek için

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;

    // Coin sayacý
    public static int coinCount = 0; // Bu coin sayýsýný tutar
    public Text coinText; // Canvas'taki Text bileþenine referans

    private void Start()
    {
        if (coinText != null)
        {
            // Baþlangýçta coin sayýsýný güncelle
            UpdateCoinText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // bloðu göster (eðer gizli ise)

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        // Coin sayýsýný arttýr ve UI'yi güncelle
        IncreaseCoinCount();

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

    // Coin sayýsýný artýrma fonksiyonu
    private void IncreaseCoinCount()
    {
        coinCount++; // Coin sayýsýný 1 artýr
        Debug.Log("Coin collected! Current coin count: " + coinCount); // Debug mesajý yazdýr
        UpdateCoinText(); // Text'i güncelle
    }


    // UI'deki coin sayýsýný güncelleme
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount.ToString();
        }
    }
}
