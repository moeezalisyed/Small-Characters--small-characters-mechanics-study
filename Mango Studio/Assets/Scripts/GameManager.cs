using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int boardWidth, boardHeight; // board size init is in Unity editor
    private GameObject enemyFolder;// folders for object organization

    private List<Enemy> enemies; // list of all placed towers
	private Enemy currentenemy;

    // Beat tracking
    private float clock;
    private float startTime;
    private float BEAT = .5f;
    private int numBeats = 0;
    int enemybeaten = 0;
    int enemynum = 0;
	private int enemytype;
	public Text HealthText;
	private List<Vector3> shadow;
	private int shadowiterator;
	private Boolean startitr;

    // Level number

    private int level = 99;


    //button locations
    float trayx = 0;
    float traywidth = 0;
    float trayspace = 0;

    // Sound stuff
    public AudioSource music;
    public AudioSource sfx;

    // Music clips
    private AudioClip idle;
    private AudioClip gametrack;
    private AudioClip winmusic;

    // Sound effect clips
    private AudioClip enemyDead;
    private AudioClip enemyHit;
    private AudioClip click;

    // Use this for initialization
    void Start()
	{
		//set up folder for enemies
		enemyFolder = new GameObject ();
		enemyFolder.name = "Enemies";
		enemies = new List<Enemy> ();
		enemytype = 1;
		addEnemy(0, 1, 0, 0);
		addEnemy(enemytype, 1, -4, -4);
		addEnemy(enemytype, 1, -4, -4);
		currentenemy = enemies [2];
		currentenemy.setCD (0.8f);
		setHealthText ();
		clock = 0f;
		shadow = new List<Vector3> ();
		shadowiterator = 0;
		startitr = false;

	}
        
    // Update is called once per frame
    void Update()
    {
		clock += Time.deltaTime;

		shadow.Add (currentenemy.model.transform.localPosition);
		if (Input.GetKey (KeyCode.RightArrow) && currentenemy.transform.position.x < 3) {
			currentenemy.move(1, 0);
		} 
		if (Input.GetKey (KeyCode.UpArrow) && currentenemy.transform.position.y < 3) {
			currentenemy.move(0, 1);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && currentenemy.transform.position.x > -8){
			currentenemy.move(-1, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow) && currentenemy.transform.position.y > -8) {
			currentenemy.move(0, -1);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			currentenemy.shoot();
		}
		setHealthText ();
		if (currentenemy.getHealth () == 0) {
			currentenemy.destroy();
			enemies.Remove(currentenemy);
			startitr = true;

			if (enemytype < 4) {
				enemytype++;
			}
			addEnemy (enemytype, 1, -4, -4);
			currentenemy = enemies [2];
			if (enemytype == 3) {
				currentenemy.setCD (1.0f);
			}
		}
		if (startitr){
			


			print ("we got here!" + shadow[0]);

			enemies [1].model.transform.localPosition = shadow [shadowiterator];
			shadowiterator++;
			}
    }

	public void addEnemy(int enemyType, int initHealth, int x, int y)
	{
		GameObject enemyObject = new GameObject();
		Enemy enemy = enemyObject.AddComponent<Enemy>();
		enemy.transform.parent = enemyFolder.transform;
		enemy.transform.position = new Vector3(x, y, 0);
		enemy.init(enemyType, initHealth, this, x, y);
		enemies.Add(enemy);
		enemynum++;
		enemy.name = "Enemy " + enemies.Count;
	}

	void setHealthText (){
		HealthText.text = "Health: "+currentenemy.getHealth();
	}



}
