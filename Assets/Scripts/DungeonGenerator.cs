using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4]; //1-north, 2 - south, 3 - east, 4 - west
    }

    public Vector2 size;
    public int startPosition = 0;
    public GameObject room;
    public Vector2 offset;

    List<Cell> board;

    void Start()
    {
        MazeGen();
    }

    void GenerateDungeon()
    {       
        for (int i= 0; i < size.x; i++)
        {
            for (int j=0; j< size.y; j++)
            {                         
                Cell currentCell = board[Mathf.FloorToInt(i+j * size.x)];  
                if(currentCell.visited)
                {                                                         
                var newRoom = Instantiate(room, new Vector3(i*offset.x, 0, -j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                newRoom.UpdateRoom(board [Mathf.FloorToInt(i+j*size.x)].status);

                newRoom.name += " " + i + "-" +  j;
                }
            }
        }
    }
    void MazeGen()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell()); // adds new cells
            }
        }

        int currentCell = startPosition;

        Stack<int> path = new Stack<int>();

        int k=0;

        while (k<50)
        {
            k++;

            board[currentCell].visited = true;
            if(currentCell == board.Count - 1)
            {
                break;
            }

            List<int> neighbors = CheckNeighbor(currentCell);

            if(neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if(newCell > currentCell)
                {
                    
                    if (newCell - 1 == currentCell)//South or East.
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;

                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;   
                    }
                }
                else
                {
                    if (newCell + 1 == currentCell) // North or West
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;

                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;   
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbor(int cell)
    {
        List<int> neighbors = new List<int>(); //checks all neighbors. 
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt (cell-size.x)].visited )//North
        {
            neighbors.Add(Mathf.FloorToInt (cell-size.x));
        }

        if (cell + size.x <= board.Count && !board[Mathf.FloorToInt(cell+size.x)].visited)//South
        {
            neighbors.Add(Mathf.FloorToInt (cell+size.x));
        }

        if ((cell+1)% size.x != 0 && !board[Mathf.FloorToInt(cell+1)].visited)//
        {
            neighbors.Add(Mathf.FloorToInt (cell+1));
        }

        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell-1)].visited)//
        {
            neighbors.Add(Mathf.FloorToInt (cell-1));
        }
        return neighbors;
    }
}
