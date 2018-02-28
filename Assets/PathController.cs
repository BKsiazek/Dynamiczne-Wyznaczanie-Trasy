using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

	public List<Spot> path;
	[Range(1, 15)]
	public int spotsRange = 1;
	public float minCost;

	void Start () {
		path = new List<Spot> ();
	}

	//TODO bardziej wydajnie byłoby z kolejkami i tylko usuwać przy normalnym przejściu jeden element z początku RACZEJ JEDNAK NIE

	//wpisuje do path pola od tego następnego po pozycji gracza, aż do celu
	public void SetCostPath(){

		path.Clear ();

		if (PlayerController.player.currentRobotSpot.b == null)
			return;

		Spot currentSpot = PlayerController.player.currentRobotSpot.b;

		while (currentSpot != DStar._this.goal) {
			path.Add (currentSpot);
			currentSpot = currentSpot.b;
		}

		path.Add (DStar._this.goal);
	}

	//jeśli pole na końcu zasięgu ma wysoki koszt to wywołuje DStar
	public void CheckSurroundings(){
		if (path.Count > spotsRange - 1) {
			if (path [spotsRange - 1].cost > minCost)
				DStar._this.OnMapModified (path [spotsRange - 1]);
		}
	}

	//przekręca tyle spotów, ile wynosi spotsRange
	public void ChangeIlluminatedFragment(){

		foreach (Spot spot in SimpleMap.map.spots) {
			spot.transform.localEulerAngles = Vector3.zero;
		}

		if (path.Count > spotsRange - 1) {
			for (int i = 0; i < spotsRange; i++) {
				if (path [i] != null)
					path [i].transform.localEulerAngles = new Vector3 (0f, 45f, 0f);
			}
		} else {
			for (int i = 0; i < path.Count; i++) {
				if (path [i] != null)
					path [i].transform.localEulerAngles = new Vector3 (0f, 45f, 0f);
			}
		}
	}
}
