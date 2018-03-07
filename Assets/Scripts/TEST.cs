using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {

	public float mouseSensitivity = 10f;

	void Update () {
		MakeItScroll ();

//		if (Input.GetKeyDown (KeyCode.Space)) {
//			MapBuilder._instance.ClearAll ();
//			MapBuilder._instance.BuildTheMap (Vector3.zero);
//		}

	}

	void MakeItScroll(){
		float fov = Camera.main.fieldOfView;
		fov -= Input.GetAxis ("Mouse ScrollWheel") * mouseSensitivity;
		Camera.main.fieldOfView = fov;
	}

	//Old map
	/*
	public Vector3 cameraOffset;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			MapBuilder._instance.ClearAll ();
			MapBuilder._instance.BuildTheMap (Vector3.zero);
		}

		CameraMove ();
	}

	void CameraMove()
	{
		Camera.main.transform.position = PlayerController.player.transform.position + cameraOffset;
	}
	 */


	//TODO poprawne działanie przy zamknięciu dookoła
	//TODO jakieś to aktywne okno chyba, żeby się dało zrobić te różne rodzaje terenu
	//TODO zrobić różne rodzaje terenu
	//TODO najpierw na wprost
}
