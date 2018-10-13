using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager main;

    [SerializeField]
    private FollowCamera followerCamera;

    [SerializeField]
    private FollowTargetSnapToGrid followerArea;

    private bool gameOver = false;
    public bool GameIsOver { get { return gameOver; } }

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 0)
        {
            main = this;
            gameObject.tag = "GameManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    MapGrid mapGrid;

    public void SetMapGrid(MapGrid mapGrid)
    {
        this.mapGrid = mapGrid;
    }

    LevelLoader levelLoader;
    public void SetLevelLoader(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    public void LoadNextLevel()
    {
        levelLoader.OpenNextLevel();
    }

    public void LoadSecretLevel()
    {
        levelLoader.OpenSecretLevel();
    }

    public MapGrid GetMapGrid()
    {
        return mapGrid;
    }

    public void SetUpPlayer(PlayerCharacter pc)
    {
        followerCamera.SetTarget(pc.transform);
        followerArea.SetTarget(pc.transform);
    }

    public void SetCameraTarget(Transform target)
    {
        followerCamera.SetTarget(target);
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
    }

    public void TheEnd()
    {
        gameOver = true;
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void Start () {
    }

    void Update () {
        if (gameOver)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Exit();
            } else if (Input.GetKeyUp(KeyCode.R))
            {
                Restart();
            }
        }
    }
}
