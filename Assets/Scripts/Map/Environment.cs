using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	public static Environment environment;

	public GameObject EnvSpotPrefab;
	public int sizeX, sizeY;
	public EnvironmentMapTile[,] environmentMap;

	[Range(0f, 1f)]
	public float obstacleDensity = 0.15f;

	public void Start(){
		
		environment = this;
		environmentMap = new EnvironmentMapTile[sizeX, sizeY];

		for (int i = 0; i < environmentMap.GetLength(0); i++) {
			for (int j = 0; j < environmentMap.GetLength(1); j++) {
				environmentMap [i, j] = new EnvironmentMapTile ();
				environmentMap [i, j].spot = (GameObject)Instantiate (EnvSpotPrefab, new Vector3 (i - (environmentMap.GetLength (0) / 2), -0.02f, j - (environmentMap.GetLength (1) / 2)), Quaternion.identity, this.transform);
				environmentMap [i, j].cost = 1f;
			}
		}

		SetObstacles ();
		SetSpotsColor ();
	}

	void SetObstacles(){

		for (int i = 0; i < environmentMap.GetLength (0); i++) {
			for (int j = 0; j < environmentMap.GetLength (1); j++) {
				if (Random.value < obstacleDensity)
					environmentMap [i, j].cost = 1000f;
			}
		}

		//TODO usunąć ZABEZPIECZENIE, ŻEBY START I GOAL NIE BYŁY PRZESZKODAMI
		environmentMap [0, 0].cost = 1f;
		environmentMap [environmentMap.GetLength (0) - 1, environmentMap.GetLength (1) - 1].cost = 1f;
	}

	public void SetSpotsColor(){
		for (int i = 0; i < environmentMap.GetLength(0); i++) {
			for (int j = 0; j < environmentMap.GetLength(1); j++) {
				environmentMap [i, j].SetColor ();
			}
		}
	}

	public class EnvironmentMapTile{
		public GameObject spot;
		public float cost;

		public void SetColor(){
			if(cost > 10f)
				spot.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.black);
			else spot.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
		}
	}
}
