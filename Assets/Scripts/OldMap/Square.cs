using UnityEngine;

public class Square {

	public Node topLeft, topRight, bottomRight, bottomLeft;
	public TerrainType value;

	public Square (Vector3 bottomLeftPosition, TerrainType tt)
	{
		this.value = tt;
		topLeft = new Node (bottomLeftPosition + new Vector3 (0f, 0f, -1f));
		topRight = new Node (bottomLeftPosition + new Vector3 (1f, 0f, -1f));
		bottomRight = new Node (bottomLeftPosition + new Vector3 (1f, 0f, 0f));
		bottomLeft = new Node (bottomLeftPosition);
	}
}
