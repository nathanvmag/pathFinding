using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public enum Method
    {
        First,
        Second
    }
    public static List<Grid.Position> FindPath(Tile[,] tiles, Grid.Position fromPosition, Grid.Position toPosition, Method method)
    {
        var path = new List<Grid.Position>();
        path.Add(fromPosition);
        if (method == Method.First)
        {
            Grid.Position position = fromPosition;
            while (position.x != toPosition.x || position.y != toPosition.y)
            {
                if (position.x > toPosition.x)
                {
                    position.x--;
                }
                else if (position.x < toPosition.x) position.x++;

                if (position.y > toPosition.y)
                {
                    position.y--;
                }
                else if (position.y < toPosition.y)
                    position.y++;

                path.Add(position);

            }

        }
        else if (method == Method.Second)
        {
            Search(fromPosition, toPosition,tiles.GetLength(0),tiles.GetLength(1));

        }

        return path;
    }

        public static void Search(Grid.Position myPosition, Grid.Position positionToFind,int width,int heigth)
        {
        Debug.Log(myPosition.x + " " + myPosition.y);
       
                if (myPosition.x  == positionToFind.x && myPosition.y == positionToFind.y)
                {
                    Debug.Log("Achei");
                }      
                else if (myPosition.x>width||myPosition.x<0||myPosition.y>heigth||myPosition.y<0)
                { Debug.Log("END"); }
                else
                 {
                  Search( new Grid.Position { x = myPosition.x + 1, y = myPosition.y }, positionToFind,width,heigth);
                  Search( new Grid.Position { x = myPosition.x -1, y = myPosition.y }, positionToFind, width, heigth);
                  Search( new Grid.Position { x = myPosition.x , y = myPosition.y +1}, positionToFind, width, heigth);
                  Search( new Grid.Position { x = myPosition.x , y = myPosition.y -1 }, positionToFind, width, heigth);
                }     
                           
                
        }   
}
