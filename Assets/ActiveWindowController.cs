using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWindowController : MonoBehaviour {

	public float[,] rememberedCostMap;
	public int spotsRange = 1;

	void Start () {
		//rememberedCostMap = new float[SimpleMap.map.sizeX, SimpleMap.map.sizeY];
	}

	public void SetCostMap(){
		
		for (int i = 0; i < SimpleMap.map.spots.GetLength(0); i++) {
			for (int j = 0; j < SimpleMap.map.spots.GetLength(1); j++) {
				rememberedCostMap [i, j] = SimpleMap.map.spots [i, j].cost;
			}
		}
	}

	public void CheckSurroundings(){
		
	}

	public void ChangeIlluminatedFragment(){
		
	}
}
