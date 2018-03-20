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

	MeshRenderer mRend;

	//TEST
	public bool visited = false;

	void Start(){

		t = State.NEW;
		SetColor ();
	}

	public void SetColor(){

		if(mRend == null)
			mRend = GetComponent<MeshRenderer> ();

		if (cost == TerrainType.waterDeep.cost)
			mRend.material.SetColor ("_Color", new Color(0.13F, 0.15F, 0.48F));
		else if (cost == TerrainType.waterShallow.cost)
			mRend.material.SetColor ("_Color", new Color(0.19F, 0.22F, 0.68F));
		else if (cost == TerrainType.grass.cost)
			mRend.material.SetColor ("_Color", new Color(0.12F, 0.62F, 0.27F));
		else if (cost == TerrainType.mountainLow.cost)
			mRend.material.SetColor ("_Color", new Color(0.71F, 0.44F, 0.3F));
		else if (cost == TerrainType.mountainMedium.cost)
			mRend.material.SetColor ("_Color", new Color(0.53F, 0.33F, 0.21F));
		else if (cost == TerrainType.mountainHigh.cost)
			mRend.material.SetColor ("_Color", new Color(0.29F, 0.29F, 0.29F));
		else mRend.material.SetColor ("_Color", Color.white);

		//TODO to niepotrzebne, tylko do testów, czy modyfikuje odpowiednio wartość
		if (cost == 1000f)
			mRend.material.SetColor ("_Color", Color.red);


		if(DStar._this.goal == this)
			mRend.material.SetColor("_Color", Color.yellow);
		if(DStar._this.start == this)
			mRend.material.SetColor("_Color", Color.magenta);	

		//TEST
		if(visited)
			mRend.material.SetColor("_Color", new Color(1F, 0.37F, 0.07F));		//orange
			
	}

	public enum State{
		NEW,
		OPEN,
		CLOSED,
	};
}
