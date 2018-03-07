using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	
	Direction location;

	void Start () {
		if (name == "LeftTrigger")
			location = Direction.Left;
		else if (name == "RightTrigger")
			location = Direction.Right;
		else if (name == "BackTrigger")
			location = Direction.Back;
		else if (name == "FrontTrigger")
			location = Direction.Front;
		else
			Debug.LogError ("Trigger: Name of trigger isn't correct");
	}

	void OnTriggerExit (Collider other) {
		if (!MapBuilder._instance.IsHeroInsideCenterChunk ()) {
			MapBuilder._instance.Generate (location);
			MapBuilder._instance.SetTriggers ();
		}
	}
}
