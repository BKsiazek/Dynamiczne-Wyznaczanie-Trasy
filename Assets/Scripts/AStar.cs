using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

	public List<Spot> openSet;
	public List<Spot> closedSet;
	public Spot start, goal;
	public SimpleMap mapController;

	public bool pathFound = false;

	public static AStar _this;

	void Start () {
		_this = this;
		openSet = new List<Spot> ();
		closedSet = new List<Spot> ();

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			SetStartAndGoal ();
			mapController.SetSpotsColor ();
		}

		if(Input.GetKeyDown (KeyCode.F))
			StartCoroutine(FindAPath ());
		
		if (Input.GetKeyDown (KeyCode.R))
			ResetAll ();
	}

	IEnumerator FindAPath(){

		openSet.Add (goal);
		goal.h = 0;
		goal.k = 0;

		Debug.Log ("Pathfinding started");

		while (openSet.Count > 0) {
			
			if (pathFound)
				break;
			
			yield return new WaitForEndOfFrame ();
			ExpandTheSpot (minFromOpenSet ());
		}

		if(pathFound){
			Debug.Log ("Path found");
			StartCoroutine(ShowPath ());
		} else Debug.Log ("Path NOT found!!!");

	}

	Spot minFromOpenSet(){
		if (openSet.Count == 0)
			return null;

		Spot min = openSet [0];
		foreach (Spot spot in openSet) {
			if (spot.k < min.k)
				min = spot;
		}

		return min;
	}


	void ExpandTheSpot(Spot spot){
		
		foreach (Spot neighbour in spot.neighbours) {
			if (neighbour.t == Spot.State.NEW) {
				neighbour.b = spot;
				neighbour.h = spot.h + neighbour.cost;
				neighbour.k = spot.k + neighbour.cost;
				neighbour.t = Spot.State.OPEN;
				openSet.Add (neighbour);
			}
		}

		spot.t = Spot.State.CLOSED;
		openSet.Remove (spot);
		closedSet.Add (spot);

		if (spot == start)
			pathFound = true;
	}

	IEnumerator ShowPath (){

		Spot currentSpot = start.b;

		while (currentSpot != goal) {
			yield return new WaitForSeconds (0.02f);
			currentSpot.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			currentSpot = currentSpot.b;
		}
	}

	void SetStartAndGoal(){

		start = mapController.spots [Random.Range(0, mapController.spots.GetLength(0)), Random.Range(0, mapController.spots.GetLength(1))];
		goal = mapController.spots [Random.Range(0, mapController.spots.GetLength(0)), Random.Range(0, mapController.spots.GetLength(1))];
	}


	void ResetAll(){
		for (int i = 0; i < mapController.spots.GetLength (0); i++) {
			for (int j = 0; j < mapController.spots.GetLength (1); j++) {
				Destroy(mapController.spots [i, j].gameObject);
			}
		}

		mapController.Start();

		openSet.Clear ();
		closedSet.Clear ();
		pathFound = false;
	}
}
