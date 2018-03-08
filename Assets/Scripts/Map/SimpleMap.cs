﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMap : MonoBehaviour {

	public static SimpleMap map;

	public Spot[,] spots;
	public GameObject SpotPrefab;

	public int range = 1;

	public void Start(){
		map = this;

		spots = new Spot[Environment.env.xSize, Environment.env.zSize];
		for (int i = 0; i < spots.GetLength(0); i++) {
			for (int j = 0; j < spots.GetLength(1); j++) {
				spots [i, j] = Instantiate (SpotPrefab, new Vector3 (i - (spots.GetLength(0)/2) + 0.5f, 0f, j - (spots.GetLength(1)/2) + 0.5f), Quaternion.identity, this.transform).GetComponent<Spot> ();

				//to not be seen in the preview camera
				spots[i, j].gameObject.layer = 10;

				spots [i, j].x = i;
				spots [i, j].y = j;
				spots [i, j].cost = TerrainType.grass.cost;
			}
		}

		SetNeighbours ();
		SetSpotsColor ();
	}

	void SetNeighbours(){
		for (int i = 0; i < spots.GetLength(0); i++) {
			for (int j = 0; j < spots.GetLength(1); j++) {
				if (i > 0)	//left
					spots [i, j].neighbours.Add (spots [i - 1, j]);
				if (i < (spots.GetLength(0)-1))	//right
					spots [i, j].neighbours.Add (spots [i + 1, j]);
				if (j > 0)	//down
					spots [i, j].neighbours.Add (spots [i, j - 1]);
				if (j  < (spots.GetLength(1)-1))	//up
					spots [i, j].neighbours.Add (spots [i, j + 1]);



				/*if(i > 0 && j > 0)	//left-down
					spots [i, j].neighbours.Add (spots [i - 1, j - 1]);
				if(i > 0 && j  < (spots.GetLength(1)-1))	//left-up
					spots [i, j].neighbours.Add (spots [i - 1, j + 1]);
				if (i < (spots.GetLength(0)-1) && j > 0)	//right-down
					spots [i, j].neighbours.Add (spots [i + 1, j - 1]);
				if (i < (spots.GetLength(0)-1) && j  < (spots.GetLength(1)-1))	//right-up
					spots [i, j].neighbours.Add (spots [i + 1, j + 1]);*/
			}
		}
	}

	public void SetSpotsColor(){
		for (int i = 0; i < spots.GetLength(0); i++) {
			for (int j = 0; j < spots.GetLength(1); j++) {
				spots [i, j].SetColor ();
			}
		}
	}

	public void ActualizeVisibleFragment(){

		int firstPosX = PlayerController.player.currentRobotSpot.x - range;
		int firstPosY = PlayerController.player.currentRobotSpot.y - range;

		int lastPosX = firstPosX + (2 * range);
		int lastPosY = firstPosY + (2 * range);

		for (int x = firstPosX; x <= lastPosX; x++) {
			for (int y = firstPosY; y <= lastPosY; y++) {
				if (x >= 0 && x < Environment.env.xSize && y >= 0 && y < Environment.env.zSize) {
					spots [x, y].cost = Environment.env.values [x, y].cost;
					spots [x, y].SetColor ();
				}
			}
		}
	}
}
