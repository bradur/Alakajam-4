using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

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


    private Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public void SaveTextureAsPNG(Texture2D _texture)
    {
        Texture2D duplicate = duplicateTexture(_texture);
        byte[] pngShot = duplicate.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + duplicate.ToString() + "_" + Random.Range(0, 1024).ToString() + ".png", pngShot);
    }


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

    public void ShowNextSecretLevelScreen()
    {
        uiManager.ShowSecreNextLevelScreen();
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
        SoundManager.main.PlaySound(SoundType.End);
        cameraManager.StartLevelEndTimeLine();
    }

    public void StartSecretLevelEndTimeLine()
    {
        SoundManager.main.PlaySound(SoundType.SecretEnd);
        cameraManager.StartSecretLevelEndTimeLine();
    }

    void Start () {
        
    }

    void Update () {

    }
}
