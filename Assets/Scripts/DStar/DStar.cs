﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DStar : MonoBehaviour {

	public static DStar _this;

	public List<Spot> OPENSet, CLOSEDSet, path;

	List<GameObject> pathElements;
	public GameObject pathElPrefab;
	public Transform PATHObjects;

	[HideInInspector]
	public Spot start, goal;

	void Start () {
		_this = this;
		OPENSet = new List<Spot> ();
		CLOSEDSet = new List<Spot> ();
		path = new List<Spot> ();

		pathElements = new List<GameObject> ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.F1)) {
			SetStartAndGoal ();
			SimpleMap.map.SetSpotsColor ();
			RunDStar();
			PlayerController.player.SetPlayerOnStartPoint ();
			PlayerController.player.GetComponent<PathController> ().SetCostPath ();
			PlayerController.player.GetComponent<PathController> ().ChangeIlluminatedFragment ();
		}

		if (Input.GetKeyDown (KeyCode.F2))
			ResetAll ();
	}

	void SetStartAndGoal(){
		start = SimpleMap.map.spots [0, 0];
		goal = SimpleMap.map.spots [SimpleMap.map.spots.GetLength(0) - 1, SimpleMap.map.spots.GetLength(1) - 1];
	}

	void ResetAll(){
		for (int i = 0; i < SimpleMap.map.spots.GetLength (0); i++) {
			for (int j = 0; j < SimpleMap.map.spots.GetLength (1); j++) {
				Destroy(SimpleMap.map.spots [i, j].gameObject);
			}
		}

		SimpleMap.map.Start();

		OPENSet.Clear ();
		CLOSEDSet.Clear ();
		path.Clear ();

		foreach(GameObject pathEl in pathElements){
			Destroy (pathEl);
		}
		pathElements.Clear ();

		SetStartAndGoal ();		//if there is other method of choosing these spots
	}

	void SaveAndShowPath (Spot robotPos = null){
		
		path.Clear ();

		//TEST
		foreach(GameObject pathEl in pathElements){
			Destroy (pathEl);
		}
		pathElements.Clear ();

		if (robotPos != null)
			path.Add (robotPos);
		else path.Add (start);

		Spot currentSpot = path[0].b;

		while (currentSpot != goal) {
			path.Add (currentSpot);

			//if(currentSpot.cost < 10)
				//currentSpot.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			//else currentSpot.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.cyan);
			//TODO dodawać pathElementy

			GameObject pathEl = (GameObject)Instantiate (pathElPrefab, new Vector3 (currentSpot.transform.position.x, currentSpot.transform.position.y + 0.02f, currentSpot.transform.position.z), Quaternion.identity);
			pathEl.transform.parent = PATHObjects;
			pathElements.Add (pathEl);

			currentSpot = currentSpot.b;
		}

		path.Add (goal);
	}

	void RunDStar(){

		goal.h = 0;
		goal.k = 0;
		goal.t = Spot.State.OPEN;
		OPENSet.Add (goal);

		float kmin;

		do {
			kmin = ProcessState();
		} while(start.t != Spot.State.CLOSED && kmin != -1f);

		SaveAndShowPath();
	}

	float ProcessState(){

		Spot X = MinState ();

		if (X == null)
			return -1f;

		float kOld = GetKMin ();
		Delete (X);

		if (kOld < X.h) {
			foreach (Spot Y in X.neighbours) {
				if (Y.h <= kOld && X.h > (Y.h + ArcCostXY(Y, X))) {
					X.b = Y;
					X.h = Y.h + ArcCostXY(Y, X);
				}
			}
		}

		if (kOld == X.h) {
			foreach (Spot Y in X.neighbours) {
				if (Y.t == Spot.State.NEW || (Y.b == X && Y.h != X.h + ArcCostXY(X, Y))
					|| (Y.b != X && Y.h > X.h + ArcCostXY(X, Y))) {
					Y.b = X;
					Insert (Y, X.h + ArcCostXY(X, Y));
				}
			}
		} else {
			foreach (Spot Y in X.neighbours) {
				if (Y.t == Spot.State.NEW || (Y.b == X && Y.h != X.h + ArcCostXY(X, Y))) {
					Y.b = X;
					Insert (Y, X.h + ArcCostXY(X, Y));
				} else {
					if (Y.b != X && Y.h > (X.h + ArcCostXY(X, Y))) {
						Insert (X, X.h);
					} else {
						if (Y.b != X && X.h > (Y.h + ArcCostXY(Y, X)) && Y.t == Spot.State.CLOSED && Y.h > kOld)
							Insert (Y, Y.h);
					}
				}
			}
		}

		return GetKMin ();
	}
		
	Spot MinState(){	//Return X if k(X) is minimum for all states on open list

		if (OPENSet.Count < 1)
			return null;

		Spot minKSpot = OPENSet [0];
		foreach (Spot spot in OPENSet) {
			if (spot.k < minKSpot.k)
				minKSpot = spot;
		}

		return minKSpot;
	}
		
	float GetKMin(){	//Return the minimum value of k for all states on open list
		
		if (OPENSet.Count < 1)
			return -1f;

		float minK = OPENSet [0].k;
		foreach (Spot spot in OPENSet) {
			if (spot.k < minK)
				minK = spot.k;
		}

		return minK;
	}

	void Delete(Spot X) {
		OPENSet.Remove (X);
		X.t = Spot.State.CLOSED;
		CLOSEDSet.Add (X);
	}

	void Insert(Spot X, float HNew){

		if (X.t == Spot.State.NEW)
			X.k = HNew;

		if (X.t == Spot.State.OPEN)
			X.k = getMin (X.k, HNew);

		if (X.t == Spot.State.CLOSED)
			X.k = getMin (X.h, HNew);

		X.h = HNew;
		X.t = Spot.State.OPEN;
		OPENSet.Add (X);
	}

	float getMin(float a, float b){
		return a < b ? a : b;
	}

	float ModifyCost(Spot X, float cval){

		if (X.t == Spot.State.CLOSED) {
			X.cost = cval;
			Insert (X, cval);
		}

		return GetKMin();
	}

	public void OnMapModified(Spot modified){

		Spot robotPos = PlayerController.player.currentRobotSpot;

		ModifyCost (modified, 1000f);

		float kmin;

		do {
			kmin = ProcessState();
		} while(modified.k < modified.h && kmin != -1f);

		SimpleMap.map.SetSpotsColor ();
		SaveAndShowPath(robotPos);

	}

	//traversing cost from Y to X
	public float ArcCostXY(Spot to, Spot from){	
		//TODO check diagonals
		//Debug.Log ("From cost: " + from.cost);
		//if (to.x == from.x || to.y == from.y)
			return 1f + from.cost;
		//else return 1.4f * from.cost;

		//TODO spróbować mnożyć


		//poprawna wersja bez diagonali
		//return 1f + from.cost;
	}
}
