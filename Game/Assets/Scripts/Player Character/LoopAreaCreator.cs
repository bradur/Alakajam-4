// Date   : 13.10.2018 10:13
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class LoopAreaCreator : MonoBehaviour
{

    [SerializeField]
    private MapGrid mapGrid;

    [SerializeField]
    private GameObject containerPrefab;

    [SerializeField]
    private Transform wallContainer;

    private int size = 7;
    private int x;
    private int y;

    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
        Debug.Log(string.Format("Creating area {0}, {1}", x, y));
        GameObject[,] area = mapGrid.GetArea(size, size, x, y);
        for (int i = -1; i < 2; i += 1)
        {
            for (int j = -1; j < 2; j += 1)
            {
                /*if (i == 0 && j == 0)
                {
                    continue;
                }*/
                SpawnArea(j * size, i * size, area);
            }
        }

    }

    void SpawnArea(int offsetX, int offsetY, GameObject[,] area)
    {
        GameObject container = Instantiate(containerPrefab);
        container.transform.position = new Vector3(offsetX, offsetY, 0f);
        foreach (GameObject worldObject in area)
        {
            if (worldObject != null)
            {
                GameObject duplicateObject = Instantiate(worldObject, container.transform);
            }
        }
        wallContainer.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
    }
}
