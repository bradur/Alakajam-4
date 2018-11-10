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

    [SerializeField]
    private CameraManager cameraManager;

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

    public void SetMusicLowPass(bool PLMOn)
    {
        levelLoader.SetMusicLowPass(PLMOn);
    }

    public void SetScore(int score)
    {
        uiManager.SetScore(score);
    }

    public void UpdateLingerTimer(float value)
    {
        uiManager.UpdateLingerTimer(value);
    }

    public void UpdateGrounded(bool grounded)
    {
        uiManager.UpdateGrounded(grounded);
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

    public void StopPlayer()
    {
        playerMovement.Stop();
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

    private PlayerMovement playerMovement;
    public void SetUpPlayer(PlayerCharacter pc)
    {
        playerMovement = pc.GetComponent<PlayerMovement>();
        followerCamera.SetTarget(pc.transform);
        followerArea.SetTarget(pc.transform);
    }

    public void SetCameraTarget(Transform target)
    {
        followerCamera.SetTarget(target);
    }


    public void ShowNextLevelScreen()
    {
        uiManager.ShowNextLevelScreen();
    }

    public void TheEnd()
    {
        uiManager.ShowTheEndScreen();
    }

    public void SwitchToEffectCamera ()
    {
        cameraManager.SwitchToEffect();
    }

    public void SwitchToPlayerCamera ()
    {
        cameraManager.SwitchToPlayer();
    }


    public void StartLevelEndTimeLine()
    {
        cameraManager.StartLevelEndTimeLine();
    }

    public void StartSecretLevelEndTimeLine()
    {
        cameraManager.StartSecretLevelEndTimeLine();
    }

    void Start () {
        
    }

    void Update () {

    }
}
