// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

enum LayerType
{
    None,
    Ground,
    Wall,
    Player
}

[System.Serializable]
public class MapObject : System.Object
{
    public string name;
    public GameObject prefab;
    public string containerTag;
}

public class LevelLoader : MonoBehaviour
{

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("LevelLoader").Length == 0)
        {
            gameObject.tag = "LevelLoader";
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameManager.main.SetLevelLoader(this);
            SetSecretLevel(nextLevel);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private AudioSource normalMusic;

    [SerializeField]
    private AudioSource secretMusic;

    [SerializeField]
    private TiledMap tiledMapPrefab;

    [SerializeField]
    private MapGrid mapGrid;

    [SerializeField]
    private LevelList levelList;

    [SerializeField]
    private MapObjectList mapObjectList;

    [SerializeField]
    private int nextLevel = 0;
    //private int currentLevel = 0;
    private int secretLevel = 0;

    [SerializeField]
    private float musicLowPassPLMOn = 900;

    [SerializeField]
    private float musicLowPassPLMOff = 5000;

    int score = 0;

    [SerializeField]
    private bool inASecretLevel = false;
    public bool InASecretLevel { get { return inASecretLevel; } }

    void Start()
    {
        //OpenNextLevel();
    }

    void Update()
    {
    }

    public MapObject GetMapObject(string name)
    {
        return mapObjectList.MapObjects.Find(element => element.name == name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(string.Format("Scene loaded. In a secret level: {0}", inASecretLevel));
        if (!inASecretLevel)
        {
            if (nextLevel < levelList.Levels.Count)
            {
                Init(levelList.Levels[nextLevel]);
            }
            else
            {
                GameManager.main.TheEnd();
            }
        }
        else
        {
            Debug.Log(string.Format("Let's load secret level: {0}", secretLevel));
            Init(levelList.SecretLevels[secretLevel]);
        }
    }

    public void OpenNextLevel()
    {
        nextLevel += 1;
        SetSecretLevel(nextLevel);
        if (nextLevel < levelList.Levels.Count)
        {
            score += 1;
            inASecretLevel = false;
            //currentLevel = nextLevel;
            //Init(levelList.Levels[nextLevel]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            GameManager.main.TheEnd();
        }
    }

    [SerializeField]
    private AudioMixer audioMixer;

    public void SetMusicLowPass(bool PLMOn)
    {
        audioMixer.SetFloat("MusicLowPass", PLMOn ? musicLowPassPLMOn : musicLowPassPLMOff);
    }

    public void SetSecretLevel(int secretLevel)
    {
        this.secretLevel = secretLevel;
    }

    public void OpenSecretLevel()
    {
        if (secretLevel < levelList.SecretLevels.Count)
        {
            score += 2;
            Debug.Log(string.Format("Loading secret level: {0}", secretLevel));
            inASecretLevel = true;
            //Init(levelList.SecretLevels[nextLevel]);
            //currentLevel = nextLevel;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Secret level not found!");
            //GameManager.main.TheEnd();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*if (inASecretLevel)
        {
            //Init(levelList.Levels[currentLevel]);
        }
        else
        {
            //Init(levelList.SecretLevels[currentLevel]);
        }*/
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Init(TextAsset mapFile)
    {
        GameManager.main.SetScore(score);
        GameManager.main.ShowLevelText(inASecretLevel, nextLevel + 1);
        if (inASecretLevel)
        {
            SoundManager.main.PlaySound(SoundType.SecretEnd);
            secretMusic.volume = 0.8f;
            normalMusic.volume = 0f;
        }
        else if (nextLevel > 0)
        {
            SoundManager.main.PlaySound(SoundType.End);
            secretMusic.volume = 0f;
            normalMusic.volume = 0.6f;
        }
        mapGrid.Clear();
        GameManager.main.SetMapGrid(mapGrid);
        GameManager.main.SetLevelLoader(this);
        XDocument mapX = XDocument.Parse(mapFile.text);
        TmxMap map = new TmxMap(mapX);

        //PlayerMovement player = Instantiate(playerPrefab);
        //GameManager.main.SetPlayer(player);
        Debug.Log(string.Format("Opening {0}", mapFile.name));
        TiledMap tiledMap = Instantiate(tiledMapPrefab);
        tiledMap.Init(map, this, mapGrid);
        //mapGrid.Activate();
    }

}

[System.Serializable]
public class Level : System.Object
{
    public TextAsset file;
}
