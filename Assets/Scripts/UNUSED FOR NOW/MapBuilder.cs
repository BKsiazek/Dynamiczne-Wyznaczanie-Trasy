using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	public static MapBuilder _instance;

	public int chunkXSize = 10, chunkZSize = 10;
	[HideInInspector]
	public float coordInTileTexture = 0.25f;

	//Noise generation
	public float seed = 0f;
	public float noiseDivider = 5f;
	public float noiseXOffset = 1f;
	public float noiseZOffset = 1f;

	//NEW
	public GameObject chunkPrefab, triggersPrefab;
	public List<GameObject> chunks;
	//bool mapBuilt;
	GameObject triggers, triggerLeft, triggerRight, triggerFront, triggerBack;
	Chunk centerChunk;


	//TEST OLD
	[Range(0f, 0.5f)]
	public float water;

	void Start () {
		_instance = this;
		//mapBuilt = false;
	}

	public void BuildTheMap(Vector3 startPoint){

		//seed = Random.Range (1000f, 2000f);
		InitializeListOfChunks (startPoint);
		SetTriggers ();

		//mapBuilt = true;
	}

	void InitializeListOfChunks (Vector3 startPoint){

		chunks = new List<GameObject> ();

		float firstX = startPoint.x - chunkXSize;
		float firstZ = startPoint.z  - chunkZSize;

		for (int x = 0; x < 3; x++) {
			for (int z = 0; z < 3; z++) {
				Chunk newChunksScript = Instantiate (chunkPrefab, transform).GetComponent<Chunk> ();
				Vector3 position = new Vector3(x * chunkXSize + firstX, startPoint.y, z * chunkZSize + firstZ);

				newChunksScript.Initialize (position);
				newChunksScript.GeneratePerlinNoiseValues ();

				MeshBuilder.BuildMesh (newChunksScript);

				if (x == 1 && z == 1)
					centerChunk = newChunksScript;

				chunks.Add (newChunksScript.gameObject);
			}
		}
	}

	public void SetTriggers()
	{
		if (triggers == null) {
			triggers = (GameObject)Instantiate (triggersPrefab, transform.parent);
			triggerLeft = triggers.transform.GetChild (0).gameObject;
			triggerRight = triggers.transform.GetChild (1).gameObject;
			triggerBack = triggers.transform.GetChild (2).gameObject;
			triggerFront = triggers.transform.GetChild (3).gameObject;
			SetTriggersSizeAndPosition ();
		}

		triggers.transform.position = centerChunk.chunkPosition;
	}

	void SetTriggersSizeAndPosition()
	{
		//size
		triggerLeft.transform.localScale = new Vector3 (0.05f, triggerLeft.transform.localScale.y, chunkZSize);
		triggerRight.transform.localScale = new Vector3 (0.05f, triggerLeft.transform.localScale.y, chunkZSize);
		triggerBack.transform.localScale = new Vector3 (chunkXSize, triggerLeft.transform.localScale.y, 0.05f);
		triggerFront.transform.localScale = new Vector3 (chunkXSize, triggerLeft.transform.localScale.y, 0.05f);

		//position towards triggers gameobject
		triggerLeft.transform.localPosition = new Vector3(-0.5f * chunkXSize, triggerLeft.transform.localPosition.y, triggerLeft.transform.localPosition.z);
		triggerRight.transform.localPosition = new Vector3(0.5f * chunkXSize, triggerRight.transform.localPosition.y, triggerRight.transform.localPosition.z);
		triggerBack.transform.localPosition = new Vector3 (triggerBack.transform.localPosition.x, triggerBack.transform.localPosition.y, -0.5f * chunkZSize);
		triggerFront.transform.localPosition = new Vector3 (triggerFront.transform.localPosition.x, triggerFront.transform.localPosition.y, 0.5f * chunkZSize);
	}
		
	public void Generate(Direction dir)
	{
		if (dir == Direction.Left) {
			//removing needless chunks
			for (int i = (chunks.Count - 1); i >= 0; i--) {
				if (chunks [i].transform.position.x > centerChunk.transform.position.x) {
					AddSimplifiedChunkToDictionary (chunks [i]);
					chunks.RemoveAt (i);
				}
			}

			float x = centerChunk.transform.position.x - (2 * chunkXSize);
			float firstZ = centerChunk.transform.position.z - chunkZSize;

			for (int z = 0; z < 3; z++) {
				Chunk newChunksScript = Instantiate (chunkPrefab, transform).GetComponent<Chunk> ();
				Vector3 position = new Vector3 (x, chunks [0].transform.position.y, z * chunkZSize + firstZ);
				newChunksScript.Initialize (position);
				newChunksScript.GeneratePerlinNoiseValues ();

				MeshBuilder.BuildMesh (newChunksScript);
				chunks.Add (newChunksScript.gameObject);
			}

			centerChunk = chunks.Find (chunk => chunk.transform.position == (centerChunk.transform.position + new Vector3 (-chunkXSize, 0f, 0f))).GetComponent<Chunk> ();

		} else if (dir == Direction.Right) {
			//removing needless chunks
			for (int i = (chunks.Count - 1); i >= 0; i--) {
				if (chunks [i].transform.position.x < centerChunk.transform.position.x) {
					AddSimplifiedChunkToDictionary (chunks [i]);
					chunks.RemoveAt (i);
				}
			}

			float x = centerChunk.transform.position.x + (2 * chunkXSize);
			float firstZ = centerChunk.transform.position.z - chunkZSize;

			for (int z = 0; z < 3; z++) {
				Chunk newChunksScript = Instantiate (chunkPrefab, transform).GetComponent<Chunk> ();
				Vector3 position = new Vector3 (x, chunks [0].transform.position.y, z * chunkZSize + firstZ);
				newChunksScript.Initialize (position);
				newChunksScript.GeneratePerlinNoiseValues ();

				MeshBuilder.BuildMesh (newChunksScript);
				chunks.Add (newChunksScript.gameObject);
			}

			centerChunk = chunks.Find (chunk => chunk.transform.position == (centerChunk.transform.position + new Vector3 (chunkXSize, 0f, 0f))).GetComponent<Chunk> ();

		} else if (dir == Direction.Back) {
			//removing needless chunks
			for (int i = (chunks.Count - 1); i >= 0; i--) {
				if (chunks [i].transform.position.z > centerChunk.transform.position.z) {
					AddSimplifiedChunkToDictionary (chunks [i]);
					chunks.RemoveAt (i);
				}
			}

			float z = centerChunk.transform.position.z - (2 * chunkZSize);
			float firstX = centerChunk.transform.position.x - chunkXSize;

			for (int x = 0; x < 3; x++) {
				Chunk newChunksScript = Instantiate (chunkPrefab, transform).GetComponent<Chunk> ();
				Vector3 position = new Vector3 (x * chunkXSize + firstX, chunks [0].transform.position.y, z);
				newChunksScript.Initialize (position);
				newChunksScript.GeneratePerlinNoiseValues ();

				MeshBuilder.BuildMesh (newChunksScript);
				chunks.Add (newChunksScript.gameObject);
			}

			centerChunk = chunks.Find (chunk => chunk.transform.position == (centerChunk.transform.position + new Vector3 (0f, 0f, -chunkZSize))).GetComponent<Chunk> ();

		} else if (dir == Direction.Front) {
			//removing needless chunks
			for (int i = (chunks.Count - 1); i >= 0; i--) {
				if (chunks [i].transform.position.z < centerChunk.transform.position.z) {
					AddSimplifiedChunkToDictionary (chunks [i]);
					chunks.RemoveAt (i);
				}
			}

			float z = centerChunk.transform.position.z + (2 * chunkZSize);
			float firstX = centerChunk.transform.position.x - chunkXSize;

			for (int x = 0; x < 3; x++) {
				Chunk newChunksScript = Instantiate (chunkPrefab, transform).GetComponent<Chunk> ();
				Vector3 position = new Vector3 (x * chunkXSize + firstX, chunks [0].transform.position.y, z);
				newChunksScript.Initialize (position);
				newChunksScript.GeneratePerlinNoiseValues ();

				MeshBuilder.BuildMesh (newChunksScript);
				chunks.Add (newChunksScript.gameObject);
			}

			centerChunk = chunks.Find (chunk => chunk.transform.position == (centerChunk.transform.position + new Vector3 (0f, 0f, chunkZSize))).GetComponent<Chunk> ();
		}


		SetTriggers ();

	}

	void AddSimplifiedChunkToDictionary(GameObject chunk)
	{
		//TODO OLD add to temporary

		Destroy(chunk);
	}

	public void ClearAll()
	{
		foreach (GameObject chunk in chunks) {
			Destroy (chunk);
		}

		chunks.Clear ();

		//TODO OLD clean simplified

		Destroy (triggers);
	}

	public bool IsHeroInsideCenterChunk() {

		Vector3 point = PlayerController.player.transform.position;

		if (point.x >= (centerChunk.transform.position.x - (0.5f * chunkXSize)) && point.x <= (centerChunk.transform.position.x + (0.5f * chunkXSize))
			&& point.z >= (centerChunk.transform.position.z - (0.5f * chunkZSize)) && point.z <= (centerChunk.transform.position.z + (0.5f * chunkZSize)))
			return true;
		else
			return false;
	}
}

public enum Direction{
	Left,
	Right,
	Front,
	Back
};