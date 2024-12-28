using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1-1"); // veya sahnenizin tam ad� neyse
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        GameManager.Instance.NewGame();
    }
}