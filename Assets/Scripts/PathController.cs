using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

	[Range(1, 15)]
	public int spotsRange = 1;
	public float minCost;

	//Visible Path
	public List<GameObject> visiblePath;
	public GameObject visiblePathPrefab;
	public Transform PATHObjects;

	void Start(){
		visiblePath = new List<GameObject> ();
	}

	//jeśli pole na końcu zasięgu ma wysoki koszt to wywołuje DStar
	public void CheckSurroundings(){
		if (DStar._this.path.Count > spotsRange - 1) {
			if (DStar._this.path [spotsRange - 1].cost > minCost)
				DStar._this.OnMapModified (DStar._this.path [spotsRange - 1]);
		}
	}

	public void ShowPath(){

		foreach(GameObject pathEl in visiblePath){
			Destroy (pathEl);
		}
		visiblePath.Clear ();

		foreach(Spot spotFromPath in DStar._this.path){

			GameObject pathEl = (GameObject)Instantiate (visiblePathPrefab, new Vector3 (spotFromPath.transform.position.x, spotFromPath.transform.position.y + 0.02f, spotFromPath.transform.position.z), Quaternion.identity);

			pathEl.transform.parent = PATHObjects;
			visiblePath.Add (pathEl);
		}

		ShowRangeOnPath ();
	}

	//podświetla te kółka trasy, które są w zasięgu sprawdzania
	void ShowRangeOnPath(){
		for (int i = 0; i < spotsRange; i++) {
			if(visiblePath.Count >= (i+1))
				visiblePath [i].GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.red);
		}
	}

	public void Reset(){
		
		foreach(GameObject pathEl in visiblePath){
			Destroy (pathEl);
		}
		visiblePath.Clear ();
	}
}
