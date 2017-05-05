using UnityEngine;

public class Tile : MonoBehaviour
{
	[System.NonSerialized]
	public Grid.Position position;

	[System.NonSerialized]
	public Grid grid;

	[System.NonSerialized]
	public bool isWall;

	private Renderer tileRenderer;
	private MaterialPropertyBlock propertyBlock;

	public void Highlight( bool highlighted )
	{
		if( isWall )
			return;

		if( highlighted )
			propertyBlock.SetColor( "_Color", Color.yellow );
		else
			propertyBlock.Clear();

		tileRenderer.SetPropertyBlock( propertyBlock );
	}

	private void Awake()
	{
		tileRenderer = GetComponent<Renderer>();
		propertyBlock = new MaterialPropertyBlock();
	}

	private void OnMouseDown()
	{
		grid.MoveTo( position );
	}
}
