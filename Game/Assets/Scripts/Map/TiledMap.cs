using UnityEngine;
using TiledSharp;


public class TiledMap : MonoBehaviour
{

    private Mesh mesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private TmxMap map;

    private int spawnedObjects = 0;
    private LevelLoader levelLoader;
    [SerializeField]
    private int tileSize = 128;

    private MapGrid mapGrid;

    public void Init(TmxMap map, LevelLoader levelLoader, MapGrid mapGrid)
    {
        this.levelLoader = levelLoader;
        this.mapGrid = mapGrid;
        this.mapGrid.Initialize(map.Width, map.Height);
        DrawLayers(map);
        SpawnObjects(map);
    }

    private void DrawLayers(TmxMap map)
    {
        int mapHeight = map.Height;
        foreach (TmxLayer layer in map.Layers)
        {
            MapObject mapObject = levelLoader.GetMapObject(Tools.GetProperty(layer.Properties, "type"));
            if (mapObject != null)
            {
                foreach (TmxLayerTile tile in layer.Tiles)
                {
                    if (tile.Gid != 0)
                    {
                        SpawnMapObject(tile.X, mapHeight - tile.Y - 1, mapObject);
                    }
                }
            }
        }
    }

    private void SpawnObjects(TmxMap map)
    {
        int mapHeight = map.Height;
        foreach (TmxObjectGroup objectGroup in map.ObjectGroups)
        {
            foreach (TmxObject tmxObject in objectGroup.Objects)
            {
                //Debug.Log(string.Format("Found object {0} at {1}, {2}", tmxObject.Type, tmxObject.X, tmxObject.Y));
                MapObject mapObject = levelLoader.GetMapObject(tmxObject.Type);
                if (mapObject != null)
                {
                    //Debug.Log(string.Format("Found object {0}", mapObject.name));
                    SpawnMapObject((int)tmxObject.X / tileSize, (mapHeight) - (int)tmxObject.Y / tileSize, mapObject);
                }
            }
        }
    }

    private void SpawnMapObject(int x, int y, MapObject mapObject)
    {
        GameObject spawnedObject = Instantiate(mapObject.prefab);
        GameObject container = GameObject.FindGameObjectWithTag(mapObject.containerTag);
        if (container != null)
        {
            spawnedObject.transform.parent = container.transform;
        }
        spawnedObject.transform.position = new Vector3(x, y, 0f);
        spawnedObject.name = string.Format("[{0}, {1}] {2} #{3} ", x, y, mapObject.name, spawnedObjects);
        spawnedObjects += 1;
        if (mapObject.name == "wall" || mapObject.name == "destructiblewall")
        {
            mapGrid.AddObject(spawnedObject, x, y);
        }
    }

}
