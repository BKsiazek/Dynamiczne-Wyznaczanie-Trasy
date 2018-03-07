using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController player;
	public Spot currentRobotSpot;
	public float secondsBetweenMoves = 1f;

	void Start(){ player = this; }

	void Update () {

		if (Input.GetKeyUp (KeyCode.Space)) {
			MakeNextMove ();
		}

		if (Input.GetMouseButtonDown (1))
			StartCoroutine (StartTraversing());
	}

	//TYLKO TO NIEJASNE
	public void MakeNextMove(){

		if (currentRobotSpot == DStar._this.goal)
			return;

		//SimpleMap.map.ActualizeVisibleFragment ();

		GetComponent<PathController> ().CheckSurroundings ();

		currentRobotSpot = currentRobotSpot.b;
		MoveToSpot (currentRobotSpot);

		DStar._this.SaveActualizedPath();
		GetComponent<PathController> ().ShowPath ();

		SimpleMap.map.ActualizeVisibleFragment ();
	}

	void MoveToSpot(Spot spot){
		transform.position = new Vector3 (spot.transform.position.x, transform.position.y, spot.transform.position.z);
	}

	public void SetPlayerOnStartPoint(){
		MoveToSpot (DStar._this.start);
		currentRobotSpot = DStar._this.start;
	}

	IEnumerator StartTraversing(){
		yield return new WaitForSeconds (secondsBetweenMoves);
		if (Input.GetMouseButton (1)) {

			//move robot
			MakeNextMove();

			StartCoroutine (StartTraversing ());
		}
	}
}
