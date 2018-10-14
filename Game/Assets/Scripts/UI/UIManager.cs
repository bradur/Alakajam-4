using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{



    public void ShowMessage(string message)
    {
        //uiDialogueManager.ShowDialogue(message);
    }


    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private UIDialog uIDialog;

    public void ShowLevelText(bool isSecretLevel, int levelNumber)
    {
        levelText.text = string.Format("{0} {1}", isSecretLevel ? "Secret level" : "Level", levelNumber);
    }

    public void ToggleSfx()
    {
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowMenu(string message, string title)
    {
        uIDialog.Show(message, title);
    }

    public void ShowPauseMenu()
    {
        uIDialog.Show();
    }

    public void CloseMenu()
    {
        uIDialog.Hide();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    bool menuOpen = false;
    void Update ()
    {

        if (gameOver || wonGame || menuOpen)
        {
            if (KeyManager.main.GetKeyDown(Action.Quit))
            {
                Application.Quit();
            }
            if (menuOpen && KeyManager.main.GetKeyDown(Action.Restart))
            {
                Restart();
            }
            if (menuOpen && KeyManager.main.GetKeyDown(Action.CloseMenu)) {
                CloseMenu();
                menuOpen = false;
                Time.timeScale = 1f;
            }
        }
        else
        {
            if (KeyManager.main.GetKeyDown(Action.OpenMenu))
            {
                ShowPauseMenu();
                menuOpen = true;
                //ShowMessage("Game paused.\n\nPress Q to quit, R to restart. Esc to close this menu.");
                Time.timeScale = 0f;
            }
        }
    }

    /*[SerializeField]
    private GameObject gameOverScreen;*/

    private bool gameOver = false;
    public void ShowGameOverScreen ()
    {
        gameOver = true;
        //ShowMessage("Game Over!\n\nPress Q to quit, R to restart.");
        Time.timeScale = 0f;
    }

    /*[SerializeField]
    private GameObject theEndScreen;*/

    private bool wonGame = false;
    public void ShowTheEndScreen()
    {
        wonGame = true;
        ShowMenu("Thanks for playing!\nPress <color=green>Q</color> to quit.", "The end");
        Time.timeScale = 0f;
    }
}
