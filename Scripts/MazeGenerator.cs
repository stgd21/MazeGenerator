using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    int mazeWidth;
    int mazeHeight;
    GridLevel level;

    // Start is called before the first frame update
    void Start()
    {
        mazeWidth = GridLevel.width;
        mazeHeight = GridLevel.height;
        level = new GridLevel();
        Location start = new Location(0, 0);

        maze(level, start);
        BuildMaze();
    }

    public void maze(Level level, Location start)
    {
        Stack<Location> locations = new Stack<Location>();
        locations.Push(start);
        level.StartAt(start);

        while (locations.Count != 0)
        {
            Location current = locations.Peek();
            Location next = level.MakeConnection(current);
            if (next != null)
            {
                locations.Push(next);
            }
            else
            {
                locations.Pop();
            }
        }
    }

    //code from github.com/bslease/Procedural_Content/
    public void BuildMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Connections currentCell = level.cells[x, y];
                if (level.cells[x, y].inMaze)
                {
                    Vector3 cellPos = new Vector3(x, 0, y);
                    float lineLength = 1f;
                    if (!currentCell.directions[0])
                    {
                        Vector3 wallPos = new Vector3(x + lineLength / 2, 0, y);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity) as GameObject;
                    }
                    if (!currentCell.directions[1])
                    {
                        Vector3 wallPos = new Vector3(x, 0, y + lineLength / 2);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                    }
                }
            }
        }
    }
}

