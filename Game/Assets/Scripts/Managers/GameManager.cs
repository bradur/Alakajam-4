using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager main;

    [SerializeField]
    private FollowCamera followerCamera;

    [SerializeField]
    private FollowTargetSnapToGrid followerArea;

    [SerializeField]
    private Material secretMaterial;
    [SerializeField]
    private Material defaultMaterial;

    [SerializeField]
    private MeshRenderer backgroundMesh;

    private bool gameOver = false;
    public bool GameIsOver { get { return gameOver; } }

    [SerializeField]
    private UIManager uiManager;

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

    public void SetScore(int score)
    {
        uiManager.SetScore(score);
    }

    LevelLoader levelLoader;
    public void SetLevelLoader(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
        if (levelLoader.InASecretLevel)
        {
            backgroundMesh.material = secretMaterial;
        } else
        {
            backgroundMesh.material = defaultMaterial;
        }
    }

    public void ShowLevelText(bool isSecretLevel, int levelNumber)
    {
        uiManager.ShowLevelText(isSecretLevel, levelNumber);
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


    public void TheEnd()
    {
        uiManager.ShowTheEndScreen();
    }


    void Start () {
    }

    void Update () {

    }
}
