using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager main;


    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("UIManager").Length == 0)
        {
            main = this;
            gameObject.tag = "UIManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }


/*
    [SerializeField]
    private UIDialogueManager uiDialogueManager;*/

    public void ShowMessage(string message)
    {
        //uiDialogueManager.ShowDialogue(message);
    }


    /*[SerializeField]
    private UIToggle uiMusic;

    public void ToggleMusic()
    {
        uiMusic.ToggleMusic();
    }*/

    public void ToggleSfx()
    {
    }

    public void Restart()
    {
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
            if (KeyManager.main.GetKeyDown(Action.Restart))
            {
                Restart();
            }
            if (menuOpen && KeyManager.main.GetKeyDown(Action.CloseMenu)) {
                //CloseDialog();
                menuOpen = false;
                Time.timeScale = 1f;
            }
        }
        else
        {
            if (KeyManager.main.GetKeyDown(Action.OpenMenu))
            {
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
        //ShowMessage("The end.\n\nThanks for playing!\nPress Q to quit, R to restart.");
        Time.timeScale = 0f;
    }
}
