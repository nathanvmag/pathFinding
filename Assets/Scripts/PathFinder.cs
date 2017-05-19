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
            return Search(fromPosition, toPosition, tiles);

        }

        return path;
    }

    public static List<Grid.Position> Search(Grid.Position myPosition, Grid.Position positionToFind, Tile[,] tiles)
    {
        int width = tiles.GetLength(0);
        int heigth = tiles.GetLength(1);
        Queue<Grid.Position> fila = new Queue<Grid.Position>();
        List<Grid.Position> path = new List<Grid.Position>();
        fila.Enqueue(myPosition);
        HashSet<Grid.Position> PlacesVisited = new HashSet<Grid.Position>();
        Grid.Position[,] Paths = new Grid.Position[tiles.GetLength(0), tiles.GetLength(1)];

        Paths[myPosition.x, myPosition.y] = myPosition;
        while (fila.Count > 0)
        {                        
            Grid.Position current = fila.Dequeue();

            if (current.x == positionToFind.x && current.y == positionToFind.y)
            {
                path.Add(current);
            
                while (current.x != myPosition.x || current.y!=myPosition.y)
                {
                    current = Paths[current.x, current.y];
                    path.Add(current);
                }
                path.Reverse();
                break;
            }
           
            else
            {
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x + 1, y = current.y })
                    && IsinTile(new Grid.Position { x = current.x + 1, y = current.y }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x + 1, y = current.y });
                    PlacesVisited.Add(new Grid.Position { x = current.x + 1, y = current.y });
                    Paths[current.x + 1, current.y] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x - 1, y = current.y })
                    && IsinTile(new Grid.Position { x = current.x - 1, y = current.y }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x - 1, y = current.y });
                    PlacesVisited.Add(new Grid.Position { x = current.x - 1, y = current.y });
                    Paths[current.x - 1, current.y] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x, y = current.y + 1 })
                    && IsinTile(new Grid.Position { x = current.x, y = current.y + 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x, y = current.y + 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x, y = current.y + 1 });
                    Paths[current.x , current.y+1] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x, y = current.y - 1 })
                    && IsinTile(new Grid.Position { x = current.x, y = current.y - 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x, y = current.y - 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x, y = current.y - 1 });
                    Paths[current.x, current.y - 1] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x+1, y = current.y + 1 })
                   && IsinTile(new Grid.Position { x = current.x+1, y = current.y + 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x+1, y = current.y + 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x+1, y = current.y + 1 });
                    Paths[current.x+1, current.y + 1] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x - 1, y = current.y + 1 })
                   && IsinTile(new Grid.Position { x = current.x - 1, y = current.y + 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x - 1, y = current.y + 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x - 1, y = current.y + 1 });
                    Paths[current.x - 1, current.y + 1] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x + 1, y = current.y - 1 })
                  && IsinTile(new Grid.Position { x = current.x + 1, y = current.y - 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x + 1, y = current.y - 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x + 1, y = current.y - 1 });
                    Paths[current.x + 1, current.y - 1] = current;
                }
                if (!PlacesVisited.Contains(new Grid.Position { x = current.x - 1, y = current.y - 1 })
                 && IsinTile(new Grid.Position { x = current.x - 1, y = current.y - 1 }, tiles))
                {
                    fila.Enqueue(new Grid.Position { x = current.x - 1, y = current.y - 1 });
                    PlacesVisited.Add(new Grid.Position { x = current.x - 1, y = current.y - 1 });
                    Paths[current.x - 1, current.y - 1] = current;
                }


            }

        }
        return path;
    }
    public static bool IsinTile(Grid.Position posi, Tile[,] tiles)
    {
        if (posi.x >= 0 && posi.x < tiles.GetLength(0) && posi.y >= 0 && posi.y < tiles.GetLength(1)&&!tiles[posi.x,posi.y].isWall)
        {
            return true;
        }
        else return false;
    }
}
    
       

        
        