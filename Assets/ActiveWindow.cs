﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWindow : MonoBehaviour {

	LineRenderer lineRend;
	public float lineHeight = -0.4f;

	void Start(){
		lineRend = GetComponent<LineRenderer> ();
	}

	void Update () {

		if (Input.GetKeyUp (KeyCode.K))
			SetLineRenderer ();

		if (PlayerController.player != null) {
			SetLineRenderer ();
			transform.position = PlayerController.player.transform.position;

		}
	}

	void SetLineRenderer(){
		//set vertices
		int range = SimpleMap.map.range;
		Vector3[] points = new Vector3[5];

		points [0] = new Vector3 (-range - 0.5f, lineHeight, -range - 0.5f);
		points [1] = new Vector3 (-range - 0.5f, lineHeight, range + 0.5f);
		points [2] = new Vector3 (range + 0.5f, lineHeight, range + 0.5f);
		points [3] = new Vector3 (range + 0.5f, lineHeight, -range - 0.5f);
		points [4] = new Vector3 (-range - 0.57f, lineHeight, -range - 0.5f);

		lineRend.SetPositions (points);

		//enable component
		//lineRend.enabled = true;
	}
}
