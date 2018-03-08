using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {

	public float mouseSensitivity = 10f;

	void Update () {
		MakeItScroll ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			Environment.env.BuildTheMap ();
		}

	}

	void MakeItScroll(){
		float fov = Camera.main.fieldOfView;
		fov -= Input.GetAxis ("Mouse ScrollWheel") * mouseSensitivity;
		Camera.main.fieldOfView = fov;
	}

	//TODO poprawne działanie przy zamknięciu dookoła
	//TODO jakieś to aktywne okno chyba, żeby się dało zrobić te różne rodzaje terenu
}
