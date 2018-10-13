// Date   : 13.10.2018 09:56
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGrid : MonoBehaviour {

    private GameObject[,] grid;

    private int width;
    private int height;

    public void Initialize(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new GameObject[width, height];
    }

    public void Clear()
    {
        grid = null;
    }

    public void AddObject(GameObject gridMember, int x, int y)
    {
        //Debug.Log(string.Format("GameObject {0}, {1} Grid[{2}, {3}]",gridMember.transform.position.x, gridMember.transform.position.y, x, y));
        grid[x,y] = gridMember;
    }

    public GameObject Get (int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            //Debug.Log("Can't get grid contents from out of bounds! [" + x + ", " + y + "]");
            return null;
        }
        return grid[x, y];
    }

    public GameObject[,] GetArea(int width, int height, int x, int y)
    {

        //Debug.Log(string.Format("Create area: [{0}, {1}] at [{2}, {3}]", width, height, x, y));
        GameObject[,] area = new GameObject[width,height];
        int offsetX = Mathf.FloorToInt(width / 2);
        int startX = x - offsetX;
        int endX = x + offsetX;
        int offsetY = Mathf.FloorToInt(height / 2);
        int startY = y - offsetY;
        int endY = y + offsetY;
        //Debug.Log(string.Format("Start from: [{0}, {1}] and finish at [{2}, {3}]", startX, startY, endX, endY));
        for (int i = startY; i <= endY; i += 1)
        {
            for (int j = startX; j <= endX; j += 1)
            {
                if (j > -1 && i > -1)
                {
                    //Debug.Log(string.Format("Get: [{0}, {1}]", j, i));
                    area[j - startX, i - startY] = Get(j, i);
                }
            }
        }
        return area;
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
