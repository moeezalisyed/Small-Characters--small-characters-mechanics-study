﻿using UnityEngine;
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
	private float cd;
	private float cdbuf;


	public void init(int enemyType, int initHealth, Enemy owner) {
		this.owner = owner;
		this.enemyType = enemyType;
		movex = 0;
		movey = 0;
		healthval = 40;
		damagebuf = 0;
		cd = 0;
		cdbuf = 0;

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
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Triangle");
			mat.color = new Color (5, 1, 1, 1);
		} else if (enemyType == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/wdot.jpg");
			mat.color = new Color (1, 5, 1, 1);
			transform.eulerAngles = new Vector3 (0, 0, -45);
		}

	}

	void Start(){
		clock = 0f;
		speed = 0.1f;
	}

	void Update(){
		clock += Time.deltaTime;
		if (enemyType == 2) {
			if (movex > 0) {
				transform.position = new Vector3 (transform.position.x + speed * Mathf.Sqrt (3) / 2, transform.position.y - speed / 2, 0);
			} else if (movex < 0) {
				transform.position = new Vector3 (transform.position.x - speed * Mathf.Sqrt (3) / 2, transform.position.y - speed/ 2, 0);
			} else if (movey > 0) {
				transform.position = new Vector3 (transform.position.x, transform.position.y + speed, 0);
			} else if (movey < 0) {
				transform.position = new Vector3 (transform.position.x + speed / 2, transform.position.y - speed * Mathf.Sqrt (3) / 2, 0);
			}
		} else if (enemyType == 1) {
			transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
		} else if (enemyType == 3) {
			transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
		}
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

		//the cd bar
		GUI.color = Color.red;
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		string t = "";
		/*for (int i = 0; i < (int)(cd-clock+cdbuf)*100; i++) {
			t += cd-clock+cdbuf;
		}*/
		t = String.Format("{0:0,0.0000000}", cd-clock+cdbuf);
		GUI.Box(new Rect (Screen.width / 2 - 200, Screen.height / 2 + 200, 150, 100), t);
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

	public int getType(){
		return enemyType;
	}

	public void shoot(){
		if (clock - cdbuf > cd) {
			if (enemyType == 1) {
				addBullet (0, 0, 0);
			} else if (enemyType == 2) {
				addBullet (0, 1, 0);
				addBullet (Mathf.Sqrt (3) * 1 / 2, -0.5f, -120);
				addBullet (-Mathf.Sqrt (3) * 1 / 2, -0.5f, 120);
			} else if (enemyType == 3) {
				addBullet (1, 0, 0);
				addBullet (-1, 0, 0);
				addBullet (0, 1, 0);
				addBullet (0, -1, 0);
			}
			cdbuf = clock;
		}
	}

	public void addBullet(float x, float y, int angle)
	{
		GameObject bulletObject = new GameObject();
		Bullet bullet = bulletObject.AddComponent<Bullet>();
		bullet.transform.position = new Vector3(x, y, 0);
		bullet.init(this, x, y, angle);
	}
		

	public void setCD(float a){
		cd = a;
	}

	void OnBecameInvisible() {
		print ("went off screen");
	}





}
