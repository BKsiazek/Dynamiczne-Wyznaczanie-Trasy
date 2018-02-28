using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTOldMap : MonoBehaviour {

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
}