// Date   : 13.10.2018 10:13
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class LoopAreaCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject containerPrefab;

    [SerializeField]
    private Transform wallContainer;

    [SerializeField]
    private Transform loopContainer;


    [SerializeField]
    private Color originalColor;

    [SerializeField]
    private Color unreachableColor;

    private int size = 7;
    private int x;
    private int y;



    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
        MapGrid mapGrid = GameManager.main.GetMapGrid();
        Debug.Log(string.Format("Creating area {0}, {1}", x, y));
        GameObject[,] area = mapGrid.GetArea(size, size, x, y);
        foreach (BoxCollider2D collider in wallContainer.GetComponentsInChildren<BoxCollider2D>())
        {
            collider.enabled = false;
        }
        foreach (SpriteRenderer sr in wallContainer.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = unreachableColor;
        }
        for (int i = -1; i < 2; i += 1)
        {
            for (int j = -1; j < 2; j += 1)
            {
                /*if (i == 0 && j == 0)
                {
                    continue;
                }*/
                bool isTheArea = i == 0 && j == 0;
                SpawnArea(j * size, i * size, area, isTheArea);
            }
        }

    }

    public void Clear()
    {
        foreach (Transform smallerContainer in loopContainer)
        {
            Destroy(smallerContainer.gameObject);
        }
        foreach (BoxCollider2D collider in wallContainer.GetComponentsInChildren<BoxCollider2D>())
        {
            collider.enabled = true;
        }
        foreach (SpriteRenderer sr in wallContainer.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = true;
            sr.color = originalColor;
        }
    }

    void SpawnArea(int offsetX, int offsetY, GameObject[,] area, bool isTheArea)
    {
        GameObject container = Instantiate(containerPrefab, loopContainer);
        container.transform.position = new Vector3(offsetX, offsetY, 0f);
        foreach (GameObject worldObject in area)
        {
            if (worldObject != null)
            {
                if (!isTheArea)
                {
                    GameObject duplicateObject = Instantiate(worldObject, container.transform);
                    duplicateObject.GetComponent<SpriteRenderer>().enabled = false;
                    foreach (BoxCollider2D bx2d in duplicateObject.GetComponents<BoxCollider2D>())
                    {
                        bx2d.enabled = true;
                    }
                }
                else
                {
                    //duplicateObject.GetComponent<BoxCollider2D>().enabled() = false;
                    SpriteRenderer sr = worldObject.GetComponent<SpriteRenderer>();
                    sr.enabled = true;
                    sr.color = originalColor;
                    foreach(BoxCollider2D bx2d in worldObject.GetComponents<BoxCollider2D>())
                    {
                        bx2d.enabled = true;
                    }
                }
            }
        }

        //wallContainer.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
    }
}
