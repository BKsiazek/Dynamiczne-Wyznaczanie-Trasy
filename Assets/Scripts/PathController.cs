using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

	[Range(1, 15)]
	public int range = 1;

	//Visible Path
	public List<GameObject> visiblePath;
	public GameObject visiblePathPrefab;
	public Transform PATHObjects;

	void Start(){
		visiblePath = new List<GameObject> ();
	}

	public void ShowPath(){

		DestroyPath ();

		foreach(Spot spotFromPath in DStar._this.path) {

			GameObject pathEl = (GameObject)Instantiate (
					visiblePathPrefab, 
					new Vector3 (
							spotFromPath.transform.position.x, 
							spotFromPath.transform.position.y + 0.02f, 
							spotFromPath.transform.position.z
							),
					Quaternion.identity
				);

			pathEl.transform.parent = PATHObjects;
			visiblePath.Add (pathEl);
		}
	}

	public void DestroyPath(){
		
		foreach(GameObject pathEl in visiblePath){
			Destroy (pathEl);
		}
		visiblePath.Clear ();
	}








	public void CheckSurroundings(){

		List<Spot> spotsToModify = GetSpotsToModify (GetVisibleSpots ());

		foreach (Spot spot in spotsToModify) {
			DStar._this.OnMapModified (spot);
			spot.SetColor ();
		}
	}

	List<Spot> GetVisibleSpots(){

		List<Spot> visibleSpots = new List<Spot>();

		int firstPosX = PlayerController.player.currentRobotSpot.x - range;
		int firstPosY = PlayerController.player.currentRobotSpot.y - range;
		int lastPosX = firstPosX + (2 * range);
		int lastPosY = firstPosY + (2 * range);

		for (int x = firstPosX; x <= lastPosX; x++) {
			for (int y = firstPosY; y <= lastPosY; y++) {
				if (x >= 0 && x < Environment.env.xSize && y >= 0 && y < Environment.env.zSize) {
					visibleSpots.Add (SimpleMap.map.spots[x, y]);
				}
			}
		}

		return visibleSpots;
	}
		
	List<Spot> GetSpotsToModify(List<Spot> visibleSpots){

		List<Spot> spotsToModify = new List<Spot> ();

		foreach (Spot spot in visibleSpots) {
			if (spot.cost != Environment.env.values [spot.x, spot.y].cost) {
				spotsToModify.Add (spot);
			}
		}

		return spotsToModify;
	}

	//used at start, after setting position of the robot
	public void UpdateSpotsInRange(){
		List<Spot> visibleSpots = GetVisibleSpots ();

		foreach (Spot visibleSpot in visibleSpots) {
			visibleSpot.cost = Environment.env.values [visibleSpot.x, visibleSpot.y].cost;
			visibleSpot.SetColor ();
		}
	}
}
