using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {
	
	public int x, y;

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
		//TODO tutaj ustawić te kolory
		//pewnie dodać materiał do prefaba itd

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
