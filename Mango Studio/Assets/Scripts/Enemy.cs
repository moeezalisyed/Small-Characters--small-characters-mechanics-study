
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	private enemyModel model;		// The model object.
	private int enemyType;
	private int initHealth;
	public float enemyx;
	private float enemyy;
	public GameManager m;		// A pointer to the manager (not needed here, but potentially useful in general).

	public void init(int enemyType, int initHealth, GameManager m, float x, float y) {
		this.enemyType = enemyType;
		this.initHealth = initHealth;
		this.m = m;
		this.enemyx = x;
		this.enemyy = y;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<enemyModel>();						// Add an enemyModel script to control visuals of the gem.
		model.init(enemyType, initHealth, this);						
	}

	public void move(int x, int y){
		model.move (x, y);
	}

	public int getHealth(){
		return model.getHealth();
	}

	public void damage(){
		model.damage ();
	}

	public void destroy(){
		model.destroy();
	}

	public void shoot(){
		model.shoot ();
	}
}

