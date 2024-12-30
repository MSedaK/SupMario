using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro için gerekli

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;

    // Coin sayacı
    public static int coinCount = 0; // Bu artık kullanılmayabilir
    public TextMeshProUGUI coinText; // Canvas'taki Text bileşenine referans

    private void Start()
    {
        if (coinText != null)
        {
            // Başlangıçta coin sayısını güncelle
            UpdateCoinText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                CollectCoin();
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
        spriteRenderer.enabled = true; // Bloğu göster (eğer gizli ise)

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

    private void CollectCoin()
    {
        // GameManager üzerinden coin toplama işlemini yap
        GameManager.Instance.AddCoin();
        Debug.Log("Coin collected! Total Coins: " + GameManager.Instance.coins);
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + GameManager.Instance.coins.ToString();
        }
    }
}
