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

    private void Start()
    {
#if UNITY_EDITOR
        debugMode = true;
        Debug.Log("editor detected, debugMode ON");
#else
        debugHud.SetActive(false);
#endif
    }

    [SerializeField]
    private Text debugText;

    private bool debugMode = false;

    [SerializeField]
    private Text lingerTimeText;

    [SerializeField]
    private GameObject debugHud;

    [SerializeField]
    private Text groundedText;

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

    public void UpdateLingerTimer(float value)
    {
        if (debugMode)
        {
            lingerTimeText.text = value.ToString();
        }
    }

    public void UpdateGrounded(bool grounded)
    {
        if (debugMode)
        {
            groundedText.text = string.Format("<color={0}>{1}</color>", grounded ? "green" : "red", grounded);
        }
    }
    void Update ()
    {
        if (debugMode)
        {
            debugText.text = (Input.GetAxis("Horizontal")).ToString();
        }
        if (nextLevelScreen)
        {
            if (KeyManager.main.GetKeyDown(Action.Restart))
            {
                Restart();
            }
            if (KeyManager.main.GetKeyDown(Action.NextLevel))
            {
                GameManager.main.LoadNextLevel();
            }
        }
        else if (secretNextLevelScreen)
        {
            if (KeyManager.main.GetKeyDown(Action.Restart))
            {
                Restart();
            }
            if (KeyManager.main.GetKeyDown(Action.NextLevel))
            {
                GameManager.main.LoadSecretLevel();
            }
        }
        else if (gameOver || wonGame || menuOpen)
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

    bool nextLevelScreen = false;
    public void ShowNextLevelScreen()
    {
        nextLevelScreen = true;
        ShowMenu("Well done!\nPress <color=green>R</color> to restart this level.\nPress <color=green>Space</color> to go to the next level.", "Level finished");
    }

    bool secretNextLevelScreen = false;
    public void ShowSecreNextLevelScreen()
    {
        secretNextLevelScreen = true;
        ShowMenu("Wow<color=#7F1734>!</color>\nPress <color=green>R</color> to restart this level.\nPress <color=#7F1734>Space</color> to go to the <color=#7F1734>secret</color> level.", "Secret level found!");
    }
}
