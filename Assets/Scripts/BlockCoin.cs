using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private static int score = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
            StartCoroutine(Animate());
        }
    }

    private void CollectCoin()
    {
        score++;
        Debug.Log($"Coin collected! Score: {score}");
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = this.transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            this.transform.localPosition = Vector3.Lerp(restingPosition, animatedPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        this.transform.localPosition = animatedPosition;

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            this.transform.localPosition = Vector3.Lerp(animatedPosition, restingPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        this.transform.localPosition = restingPosition;

        Destroy(gameObject);
    }
}