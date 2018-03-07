using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public TerrainType[,] values;
	public Vector3 chunkPosition;

	public void Initialize(Vector3 position, TerrainType[,] values = null)
	{
		chunkPosition = position;
		transform.position = chunkPosition;

		if(values == null)
			this.values = new TerrainType[MapBuilder._instance.chunkXSize, MapBuilder._instance.chunkZSize];
		else this.values = values;
	}

	public void GeneratePerlinNoiseValues()
	{
		for (int x = 0; x < values.GetLength(0); x++) {
			for (int z = 0; z < values.GetLength(1); z++) {
				float generatedValue;
				generatedValue = 
					Mathf.PerlinNoise (
						((chunkPosition.x + x) / MapBuilder._instance.noiseDivider * MapBuilder._instance.noiseXOffset) + MapBuilder._instance.seed, 
						((chunkPosition.z + z) / MapBuilder._instance.noiseDivider * MapBuilder._instance.noiseZOffset) + MapBuilder._instance.seed
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

		if(noiseValue < (0.66f * MapBuilder._instance.water))
			typeOfTerrain = TerrainType.waterDeep;
		else if(noiseValue < MapBuilder._instance.water)
			typeOfTerrain = TerrainType.waterShallow;
		else if(noiseValue < 0.6f)
			typeOfTerrain = TerrainType.grass;
		else if (noiseValue < 0.7f)
			typeOfTerrain = TerrainType.mountainLow;
		else if (noiseValue < 0.85f)
			typeOfTerrain = TerrainType.mountainMedium;
		else typeOfTerrain = TerrainType.mountainHigh;

		return typeOfTerrain;
	}
}
