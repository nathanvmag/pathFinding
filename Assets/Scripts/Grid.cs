using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[System.Serializable]
	public struct Position
	{
		public int x;
		public int y;

		public Vector3 ToWorldPosition( Vector2 spacing, float height )
		{
			return new Vector3( x * spacing.x, height, y * spacing.y );
		}
	}

	public Player playerPrefab;
	public Tile blackTilePrefab;
	public Tile whiteTilePrefab;
	public Tile wallTilePrefab;

	public int width = 10;
	public int height = 10;

	public Vector2 tileSpacing = new Vector2( 1.0f, 1.0f );

	public Position playerStartingPosition;
	public Position[] wallPositions;

	private Player player;
	private Tile[,] tiles;

    public void MoveTo(Position targetPosition)
    {
        if (!player.Ismoving)
            {
            foreach (Tile tile in tiles)
                tile.Highlight(false);

            if (tiles[targetPosition.x, targetPosition.y].isWall)
            {
                Debug.Log("é uma parede");
                if (!tiles[targetPosition.x - 1, targetPosition.y].isWall)
                {
                    targetPosition = new Grid.Position { x = targetPosition.x - 1, y = targetPosition.y };
                }
                else if (!tiles[targetPosition.x + 1, targetPosition.y].isWall)
                {
                    targetPosition = new Grid.Position { x = targetPosition.x + 1, y = targetPosition.y };
                }
                else if (!tiles[targetPosition.x, targetPosition.y + 1].isWall)
                {
                    targetPosition = new Grid.Position { x = targetPosition.x, y = targetPosition.y + 1 };
                }
                else if (!tiles[targetPosition.x, targetPosition.y - 1].isWall)
                {
                    targetPosition = new Grid.Position { x = targetPosition.x, y = targetPosition.y - 1 };
                }
            }
            List<Position> path = PathFinder.FindPath(tiles, player.position, targetPosition, PathFinder.Method.Second);

            foreach (Position position in path)
                tiles[position.x, position.y].Highlight(true);

            StartCoroutine(player.Walk(path, tileSpacing));
        }
    }

	private void Start()
	{
		tiles = new Tile[width, height];

		for( int i = 0; i < width; i++ )
		{
			for( int j = 0; j < height; j++ )
			{
				bool chooseWhite = ( i + j ) % 2 == 0;
				bool isWall = System.Array.IndexOf( wallPositions, new Position { x = i, y = j } ) >= 0;

				Tile tilePrefab;
				if( isWall )
					tilePrefab = wallTilePrefab;
				else if( chooseWhite )
					tilePrefab = whiteTilePrefab;
				else
					tilePrefab = blackTilePrefab;

				var position = new Position { x = i, y = j };
				var worldPosition = position.ToWorldPosition( tileSpacing, 0.0f );

				Tile tile = Instantiate( tilePrefab, worldPosition, Quaternion.identity, transform );
				tile.position = position;
				tile.grid = this;
				tile.isWall = isWall;

				tiles[i, j] = tile;
			}
		}

		player = Instantiate( playerPrefab, transform, true );
		player.SetPosition( playerStartingPosition, tileSpacing );
	}
}
