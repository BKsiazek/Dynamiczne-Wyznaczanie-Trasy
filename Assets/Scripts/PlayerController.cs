using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController player;

	public Spot currentRobotSpot;

	void Start()
	{
		player = this;
	}

	void Update () {

		if (Input.GetKeyUp (KeyCode.Space))
			MakeNextMove ();
	}

	public void MakeNextMove(){

		if (currentRobotSpot == DStar._this.goal)
			return;

		//if next spot is a obstacle
		//if (currentRobotSpot.b != null && currentRobotSpot.b.cost > 10f)
			//DStar._this.OnMapModified (currentRobotSpot.b);

		//TODO jeszcze przed ruchem trzeba uruchomić ten PathController

		GetComponent<PathController> ().CheckSurroundings ();

		currentRobotSpot = currentRobotSpot.b;
		MoveToSpot (currentRobotSpot);

		//GetComponent<PathController> ().CheckSurroundings ();
		//GetComponent<PathController> ().SetCostPath ();
		//GetComponent<PathController> ().CheckSurroundings ();
		GetComponent<PathController> ().SetCostPath ();
		GetComponent<PathController> ().ChangeIlluminatedFragment ();

	}

	void MoveToSpot(Spot spot){
		transform.position = new Vector3 (spot.transform.position.x, transform.position.y, spot.transform.position.z);
	}

	public void SetPlayerOnStartPoint(){
		MoveToSpot (DStar._this.start);
		currentRobotSpot = DStar._this.start;
	}

}
