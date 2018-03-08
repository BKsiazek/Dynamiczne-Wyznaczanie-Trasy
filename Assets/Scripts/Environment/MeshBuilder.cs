using System.Collections.Generic;
using UnityEngine;

public static class MeshBuilder{

	static Square[,] squares;
	static List<Vector3> vertices;
	static List<int> triangles;
	static List<Vector2> uvs;

	public static void BuildMesh(Environment environment)
	{
		//Remove old mesh if exists
		GameObject.Destroy (environment.GetComponent<MeshFilter> ().sharedMesh);

		InitializeSquares (environment.values);

		vertices = new List<Vector3> ();
		triangles = new List<int> ();
		uvs = new List<Vector2> ();

		for (int x = 0; x < squares.GetLength(0); x++) {
			for (int z = 0; z < squares.GetLength(1); z++) {
				TriangulateSquare (squares [x, z]);
				AssignTextureToSquare (squares [x, z].value);
			}
		}

		Mesh mesh = new Mesh ();

		mesh.vertices = vertices.ToArray ();
		mesh.uv = uvs.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.RecalculateNormals ();

		environment.GetComponent<MeshFilter> ().sharedMesh = mesh;
		environment.GetComponent<MeshCollider> ().sharedMesh = mesh;

	}

	static void InitializeSquares(TerrainType[,] values)
	{
		Vector3 positionOffset = new Vector3 (-(Environment.env.xSize / 2f), 0f, -(Environment.env.zSize / 2f) + 1f);

		squares = new Square[Environment.env.xSize, Environment.env.zSize];

		for (int x = 0; x < squares.GetLength(0); x++) {
			for (int z = 0; z < squares.GetLength(1); z++) {
				Vector3 squareBottomLeftPos = new Vector3 (x, 0f, z) + positionOffset;
				squares [x, z] = new Square (squareBottomLeftPos, values [x, z]);
			}
		}
	}

	static void TriangulateSquare(Square square) {
		ConnectPoints(square.bottomLeft, square.topRight, square.topLeft, square.bottomRight);
	}

	static void ConnectPoints(params Node[] points) {
		AttachVertices (points);

		CreateTriangle (points [0], points [1], points [2]);
		CreateTriangle (points [1], points [0], points [3]);
	}

	static void AttachVertices(Node[] points) {
		for (int i = 0; i < points.Length; i++) {
			if (points [i].index == -1) {
				points [i].index = vertices.Count;
				vertices.Add (points [i].position);
			}
		}
	}

	static void CreateTriangle(Node a, Node b, Node c) {
		triangles.Add (a.index);
		triangles.Add (b.index);
		triangles.Add (c.index);
	}

	static void AssignTextureToSquare (TerrainType tt) {

		int tileX, tileY;

		if (tt == TerrainType.waterDeep) {
			tileX = 0;
			tileY = 3;
		} else if (tt == TerrainType.waterShallow) {
			tileX = 1;
			tileY = 3;
		} else if (tt == TerrainType.grass) {
			tileX = 2;
			tileY = 3;
		} else if (tt == TerrainType.mountainLow) {
			tileX = 3;
			tileY = 3;
		} else if (tt == TerrainType.mountainMedium) {
			tileX = 0;
			tileY = 2;
		} else if (tt == TerrainType.mountainHigh) {
			tileX = 1;
			tileY = 2;
		} else {
			tileX = -1;
			tileY = -1;
		}

		if (tileX > -1) {
			float umin = Environment.env.coordInTileTexture * tileX;
			float umax = Environment.env.coordInTileTexture * (tileX + 1);
			float vmin = Environment.env.coordInTileTexture * tileY;
			float vmax = Environment.env.coordInTileTexture * (tileY + 1);

			uvs.Add (new Vector2 (umin, vmax));
			uvs.Add (new Vector2 (umax, vmin));
			uvs.Add (new Vector2 (umin, vmin));
			uvs.Add (new Vector2 (umax, vmax));
		}
	}
}