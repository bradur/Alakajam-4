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

    public void Init(TmxMap map, LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
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
                        SpawnMapObject(tile.X, mapHeight - tile.Y, mapObject);
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
                Debug.Log(string.Format("Found object {0} at {1}, {2}", tmxObject.Type, tmxObject.X, tmxObject.Y));
                MapObject mapObject = levelLoader.GetMapObject(tmxObject.Type);
                if (mapObject != null)
                {
                    Debug.Log(string.Format("Found object {0}", mapObject.name));
                    SpawnMapObject((int)tmxObject.X / tileSize, (mapHeight + 1) - (int)tmxObject.Y / tileSize, mapObject);
                }
            }
        }
    }

    private void SpawnMapObject(int x, int y, MapObject mapObject)
    {
        GameObject spawnedObject = Instantiate(mapObject.prefab);
        spawnedObject.transform.parent = mapObject.container;
        spawnedObject.transform.position = new Vector3(x, y, 0f);
        spawnedObject.name = string.Format("[{0}, {1}] {2} #{3} ", x, y, mapObject.name, spawnedObjects);
        spawnedObjects += 1;
    }

}
