using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	public static Environment env;

	public int xSize = 10, zSize = 10;
	public TerrainType[,] values;

	[HideInInspector]
	public float coordInTileTexture = 0.25f;

	//Noise generation
	public float seed = 0f;
	public float noiseDivider = 5f;
	public float noiseXOffset = 1f;
	public float noiseZOffset = 1f;

	//TEST OLD może do wyrzucenia
	[Range(0f, 0.5f)]
	public float water;

	void Start () {
		env = this;
		BuildTheMap ();
	}

	public void BuildTheMap(){

		seed = Random.Range (1000f, 2000f);
		values = new TerrainType[xSize, zSize];
		GeneratePerlinNoiseValues ();
		MeshBuilder.BuildMesh (this);
		transform.position += new Vector3 (0f, -0.3f, 0f);
	}

	public void GeneratePerlinNoiseValues()
	{
		for (int x = 0; x < values.GetLength(0); x++) {
			for (int z = 0; z < values.GetLength(1); z++) {
				float generatedValue = Mathf.PerlinNoise (
						(x / noiseDivider * noiseXOffset) + seed, 
						(z / noiseDivider * noiseZOffset) + seed
				);
				
				values [x, z] = GetTerrainTypeFromNoiseValue (generatedValue);
			}
		}
	}

	TerrainType GetTerrainTypeFromNoiseValue(float noiseValue)
	{
		TerrainType typeOfTerrain;

		/*if (noiseValue < 0.2f)
			typeOfTerrain = TerrainType.waterDeep;
		else if (noiseValue < 0.3f)
			typeOfTerrain = TerrainType.waterShallow;
		else if (noiseValue < 0.6f)
			typeOfTerrain = TerrainType.grass;
		else if (noiseValue < 0.7f)
			typeOfTerrain = TerrainType.mountainLow;
		else if (noiseValue < 0.85f)
			typeOfTerrain = TerrainType.mountainMedium;
		else typeOfTerrain = TerrainType.mountainHigh;*/

//		if(noiseValue < (0.66f * MapBuilder._instance.water))
//			typeOfTerrain = TerrainType.waterDeep;
//		else if(noiseValue < MapBuilder._instance.water)
//			typeOfTerrain = TerrainType.waterShallow;
//		else if(noiseValue < 0.6f)
//			typeOfTerrain = TerrainType.grass;
//		else if (noiseValue < 0.7f)
//			typeOfTerrain = TerrainType.mountainLow;
//		else if (noiseValue < 0.85f)
//			typeOfTerrain = TerrainType.mountainMedium;
//		else typeOfTerrain = TerrainType.mountainHigh;

		if(noiseValue < 0.6f)
			typeOfTerrain = TerrainType.grass;
		//else if(noiseValue < 0.7f)
			//typeOfTerrain = TerrainType.mountainLow;
		else typeOfTerrain = TerrainType.mountainHigh;

		return typeOfTerrain;
	}
}
