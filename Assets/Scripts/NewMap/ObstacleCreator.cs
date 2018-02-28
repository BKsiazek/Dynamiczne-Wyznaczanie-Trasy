using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour {

	void Update () {
		if (Input.GetMouseButtonUp (0))
			ShootRayFromMouse ();
	}

	void ShootRayFromMouse(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if(Physics.Raycast(ray, out hit)){
			GameObject hitObject = hit.transform.gameObject;

			if (hitObject.GetComponent<Spot> ()) {
				AddObstacle (hitObject.GetComponent<Spot> ());
			}
		}
	}

	void AddObstacle(Spot spot){
		spot.cost = 1000;
		spot.SetColor ();
	}
}
