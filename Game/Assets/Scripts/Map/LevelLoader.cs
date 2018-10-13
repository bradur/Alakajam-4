// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;
using UnityEngine.SceneManagement;

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
    public Transform container;
}

public class LevelLoader : MonoBehaviour
{

    //[SerializeField]
    //private MapGrid mapGrid;

    [SerializeField]
    private Transform tiledMapContainer;

    [SerializeField]
    private TiledMap tiledMapPrefab;

    [SerializeField]
    private MapGrid mapGrid;

    [SerializeField]
    private List<TextAsset> levels;

    [SerializeField]
    private List<MapObject> mapObjects;

    [SerializeField]
    private int nextLevel = 0;
    private int currentLevel = 0;

    void Start()
    {
        //Init(debugMap);
        OpenNextLevel();
    }

    void Update()
    {
    }

    public MapObject GetMapObject(string name)
    {
        return mapObjects.Find(element => element.name == name);
    }

    public void OpenNextLevel()
    {
        if (nextLevel < levels.Count)
        {
            Init(levels[nextLevel]);
            currentLevel = nextLevel;
        }
        else
        {
            GameManager.main.TheEnd();
        }
        nextLevel += 1;
    }

    public void RestartLevel()
    {
        Init(levels[currentLevel]);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Init(TextAsset mapFile)
    {
        XDocument mapX = XDocument.Parse(mapFile.text);
        TmxMap map = new TmxMap(mapX);

        //PlayerMovement player = Instantiate(playerPrefab);
        //GameManager.main.SetPlayer(player);

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
