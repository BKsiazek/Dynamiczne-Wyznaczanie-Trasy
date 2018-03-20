using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController player;
	[HideInInspector]
	public Spot currentRobotSpot;
	public float secondsBetweenMoves = 1f;
	[HideInInspector]
	public PathController pathCtrl;

	void Start(){
		player = this;
		pathCtrl = GetComponent<PathController> ();
	}

	void Update (){

		if (Input.GetKeyUp (KeyCode.Space)) {
			MakeNextMove ();
		}

		if (Input.GetMouseButtonDown (1))
			StartCoroutine (StartTraversing());
	}

	public void MakeNextMove(){

		if (currentRobotSpot == DStar._this.goal)
			return;

		pathCtrl.CheckSurroundings ();

		currentRobotSpot = currentRobotSpot.b;
		MoveToSpot (currentRobotSpot);

		//TEST
		currentRobotSpot.visited = true;
		currentRobotSpot.SetColor ();

		//aktualizacja listy spotów path z DStar
		DStar._this.SaveActualizedPath();

		//pokazuje nową ścieżkę z kółek
		pathCtrl.ShowPath ();
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
			MakeNextMove();
			StartCoroutine (StartTraversing ());
		}
	}
}
