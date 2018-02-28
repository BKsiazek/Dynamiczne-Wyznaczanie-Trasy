using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTNewMap : MonoBehaviour {

	public float secondsBetweenMoves = 1f;

	void Update () {
		if (Input.GetMouseButtonDown (1))
			StartCoroutine (StartTraversing());
	}

	IEnumerator StartTraversing(){
		yield return new WaitForSeconds (secondsBetweenMoves);
		if (Input.GetMouseButton (1)) {

			//move robot
			PlayerController.player.MakeNextMove();

			StartCoroutine (StartTraversing ());
		}
	}

	//TODO spróbować dodać jakiś połysk do ścieżki zamiast zmieniać kolor
	//TODO zrobić dalsze pole widzenia
	//TODO zrobić różne rodzaje terenu
}
