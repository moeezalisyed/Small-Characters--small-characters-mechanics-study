
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public playerModel model;		// The model object.
	private int playerType;
	//private int initHealth;
	public GameManager m;		// A pointer to the manager (not needed here, but potentially useful in general).

	public void init(int playerType, GameManager m) {
		this.playerType = playerType;
		//this.initHealth = initHealth;
		this.m = m;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<playerModel>();						// Add an playerModel script to control visuals of the gem.
		model.init(playerType, this);						
	}

	public void move(int x, int y){
		model.move (x, y);
	}

//	public int getHealth(){
//		return model.getHealth();
//	}

//	public void damage(){
//		model.damage ();
//	}

	public void
	 destroy(){
		model.destroy();
	}

	public void shoot(){
		model.shoot ();
	}

	public void setCD(float a){
		model.setCD (a);
	}

	public int getType(){
		return model.getType();
	}
}

