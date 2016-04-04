using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class enemyModel : MonoBehaviour
{
	private float clock;	// to keep track of the time(not used for now)
	private Enemy owner;	// object that created it
	private Material mat;	// material (for texture)
	private int enemyType;	// the type of the enemy(0, 1, 2)
	private int movex;
	private int movey;
	private float speed;
	private int healthval;
	private float damagebuf;

	// sfx
	private AudioClip endsound;

	public void init(int enemyType, int initHealth, Enemy owner) {
		this.owner = owner;
		this.enemyType = enemyType;
		movex = 0;
		movey = 0;
		healthval = 100;
		damagebuf = 0;

		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);
		name = "Enemy Model";
		mat = GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");
		if (enemyType == 1) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			mat.color = new Color (1, 1, 1, 1);
		} else if (enemyType == 0) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");
			mat.color = new Color (1, 1, 1, 1);
		} else if (enemyType == 2) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			mat.color = new Color (5, 1, 1, 1);
		} else if (enemyType == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			mat.color = new Color (1, 5, 1, 1);
		} else if (enemyType == 4) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			mat.color = new Color (1, 1, 5, 1);
		}

	}

	void Start(){
		clock = 0f;
		speed = 0.1f;
	}

	void Update(){
		clock += Time.deltaTime;
		transform.position = new Vector3 (transform.position.x+movex*speed, transform.position.y+movey*speed, 0);
		movex = 0;
		movey = 0;
		if (clock - damagebuf > 3) {
			damage();
			damagebuf = clock;
		}

	}

	void OnGUI(){
		GUI.color = Color.green;
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		string s = "";
		for (int i = 0; i < healthval / 10; i++) {
			s += "I";
		}
		GUI.Box(new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 150, 100), s);
		GUI.color = Color.white;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	
	
	}


	public void move(int x, int y){
		movex = x;
		movey = y;
	}

	public int getHealth(){
		return healthval;
	}

	public void damage(){
		healthval -= 20;
	}

	public void destroy(){
		DestroyImmediate (gameObject);
	}

	public void shoot(){
		
	}


}
