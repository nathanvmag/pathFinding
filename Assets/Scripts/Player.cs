using UnityEngine;

public class Player : MonoBehaviour
{
	[System.NonSerialized]
	public Grid.Position position;

	public void SetPosition( Grid.Position position, Vector2 spacing )
	{
		this.position = position;
		transform.localPosition = position.ToWorldPosition( spacing, 1.0f );
	}
}
