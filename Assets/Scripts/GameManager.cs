using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro için gerekli

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 1;
    public int coins { get; private set; } = 0;

    [SerializeField] private TextMeshProUGUI coinText; // TextMeshPro Text alaný

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        // UI'yi güncelle
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coins++;

        // Eðer 100 coin toplandýysa bir can ekle
        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }

        // UI'yi güncelle
        UpdateCoinText();
    }

    public void AddLife()
    {
        lives++;
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        lives = 1;
        coins = 0;
        UpdateCoinText();
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = $"{coins}";
        }
    }

    public void NewGame()
    {
        lives = 1;
        coins = 0;

        UpdateCoinText();
        LoadLevel(1, 1);
    }
}
