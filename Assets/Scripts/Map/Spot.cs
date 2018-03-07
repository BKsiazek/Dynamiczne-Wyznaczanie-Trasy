using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {
	
	public int x, y;	//TODO maybe unnecessary (check it later)

	public float cost;
	public float h;
	public float k;
	public Spot b;
	public State t;

	public List<Spot> neighbours;

	void Start(){
		t = State.NEW;
		SetColor ();
	}

	public void SetColor(){

		/*if (cost > 900f)	//obstacle
			GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.black);
		else if (cost > 500f)
			GetComponent<MeshRenderer> ().material.SetColor ("_Color", new Color32 (158, 158, 158, 255));
		else if (cost > 200f)
			GetComponent<MeshRenderer> ().material.SetColor ("_Color", new Color32 (201, 201, 201, 255));
		else
			GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);*/

		if(cost > 10f)
			GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.black);
		else GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);


		if(DStar._this.goal == this)	//goal
			GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
		if(DStar._this.start == this)	//start
			GetComponent<MeshRenderer>().material.SetColor("_Color", Color.magenta);		
	}

	public enum State{
		NEW,
		OPEN,
		CLOSED,
	};
}
