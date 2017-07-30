using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globalevents : MonoBehaviour {
	public GameObject enemy;
	public GameObject enemy2;
	public float playerpower;
	public float powervaluechanger;
	public float spawnercounter;
	public float spawnamt;
	public Vector3 storeenemyspawnloc;
	public Vector3 randomloc;
	public Vector3 randomgroundieloc;
	public float enemycount;
	public float killcount;
	private float valuestorer;
	public AudioClip hit;
	public AudioSource audios;
	public AudioClip fluids;
	public GameObject bed;
	public GameObject groundie;
	public float waveno;
	public bool bed2coroutinestart;
	public bool bed4coroutinestart;
	public bool gamestarted;
	public GameObject deathscreen;
	public float timespawnedat;
	public float killtime;
	public float groundiespawnedat;
	public float groundiespawntimer;
	public bool inverter;
	public float invert;
	public bool post;
	public float sensmultiplier;
	public AudioClip fourx;

	void Awake() {
		gamestarted = false;
		Application.targetFrameRate = -1;
		spawnamt = 1;
		enemycount = 0;
		killcount = 0;
	}

	// Use this for initialization
	void Start () {
		post = true;
		inverter = false;
		groundiespawnedat = 0;
		groundiespawntimer = 20;
		bed2coroutinestart = false;
		bed4coroutinestart = false;
		audios = GetComponent<AudioSource> ();
		spawnercounter = 0;
		playerpower = 100;
		powervaluechanger = 100;
		waveno = 0;
		killtime = -100;


	}

	// Update is called once per frame
	void Update () {
		if (!inverter) {
			invert = -1;
		}
		if (inverter) {
			invert = 1;
		}
		spawnamt = Mathf.Clamp (spawnamt, 0, 64);
		if (waveno > 5 && Time.time - groundiespawnedat > groundiespawntimer) {
			groundiespawntimer = Random.Range (10, 30);
			Instantiate (groundie, new Vector3(-42, 30, 44), Quaternion.identity);
			groundiespawnedat = Time.time;
		}
		if (GameObject.Find ("ui") != null) {
			if (Time.time - killtime < 4) {
				GameObject.Find ("killcount").GetComponent<Text> ().text = GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount.ToString ();
			} else {
				GameObject.Find ("killcount").GetComponent<Text> ().text = "";
			}

			GameObject.Find ("enemiesremaining").GetComponent<Text> ().text = enemycount.ToString ();
			//GameObject.Find ("killcount").GetComponent<Text> ().text = "Kills: " + killcount.ToString ();
		}
		//playerpower = powervaluechanger - Time.time * 4;
		if (enemycount == 0 && gamestarted == true) {
			waveno++;
			if (GameObject.Find ("player") != null) {
				GameObject.Find ("player").GetComponent<player> ().audios.clip = GameObject.Find ("player").GetComponent<player> ().enemyspawn;
				GameObject.Find ("player").GetComponent<player> ().audios.Play ();
			}
			valuestorer = Random.Range (0, 100);
			if (valuestorer > 80) {
				Instantiate (enemy2, transform.position, Quaternion.identity);
			}

			for (int i = 0; i < spawnamt; i++) {
				randomspawnloc ();
				Instantiate (enemy, randomloc, Quaternion.identity);
			}
		}
		if (waveno == 6) {
			if (!bed2coroutinestart) {
				StartCoroutine (bedtwo ());
				waveno++;
			}
		}
		if (waveno == 8) {
			if (!bed4coroutinestart) {
				StartCoroutine (bedfour ());
				waveno++;
			}
		}



		playerpower -= Time.deltaTime * 5;
		playerpower = Mathf.Clamp (playerpower, 0, 100);
		//GameObject.Find ("valuedisplay").GetComponent<Text> ().text = playerpower.ToString ("F2");
		if (GameObject.Find ("ui") != null) {
			GameObject.Find ("pwrbar").GetComponent<RectTransform> ().localScale = new Vector3 (playerpower / 100, 1, 1);
			GameObject.Find ("jpbar").GetComponent<RectTransform> ().localScale = new Vector3 (GameObject.Find("player").GetComponent<player>().jpmeter / 100, 1, 1);
			if (playerpower <= 0 && gamestarted == true) {
				GameObject[] gameObjects;
				gameObjects = GameObject.FindGameObjectsWithTag("powerpickup");
				for(int i = 0; i < gameObjects.Length ; i ++) {
					Destroy(gameObjects[i]);
				}
				gameObjects = GameObject.FindGameObjectsWithTag("groundie");
				for(int i = 0; i < gameObjects.Length ; i ++) {
					Destroy(gameObjects[i]);
				}
				gameObjects = GameObject.FindGameObjectsWithTag("enemy");
				for(int i = 0; i < gameObjects.Length ; i ++) {
					Destroy(gameObjects[i]);
				}
				gameObjects = GameObject.FindGameObjectsWithTag("enemyprojectile");
				for(int i = 0; i < gameObjects.Length ; i ++) {
					Destroy(gameObjects[i]);
				}
				gameObjects = GameObject.FindGameObjectsWithTag("bed");
				for(int i = 0; i < gameObjects.Length ; i ++) {
					Destroy(gameObjects[i]);
				}
				Destroy (GameObject.Find ("player").gameObject);
				Instantiate (deathscreen, new Vector3 (0, 0, 0), Quaternion.identity);

			}
		}

	}

	public IEnumerator bedtwo() {
		bed2coroutinestart = true;
		GameObject secondbed = Instantiate (bed, new Vector3 (0, 0, 100), Quaternion.Euler (0, 180, 0));
		GameObject.Find ("projectilekillbox1").transform.position += new Vector3 (0, 0, 100);
		GameObject.Find ("player").GetComponent<player> ().zmovemax += 100;
		GameObject.Find ("player").GetComponent<player> ().movespeedmultiplier = new Vector3 (21.6f,21.6f, 21.6f);
		yield return null;
	}

	public IEnumerator bedfour() {
		bed4coroutinestart = true;
		GameObject thirdbed = Instantiate (bed, new Vector3 (-100, 0, 100), Quaternion.Euler (0, 180, 0));
		GameObject fourthbed = Instantiate (bed, new Vector3 (-100, 0, 0), Quaternion.Euler (0, 0, 0));
		GameObject.Find ("projectilekillbox3").transform.position -= new Vector3 (100, 0, 0);
		GameObject.Find ("player").GetComponent<player> ().xmovemin -= 100;
		GameObject.Find ("player").GetComponent<player> ().movespeedmultiplier = new Vector3 (36, 36, 36);
		yield return null;
	}

	void randomspawnloc(){
		if (Time.time - timespawnedat < 1) {
			randomloc = new Vector3 (Random.Range (-25, 60), Random.Range (6, 13), Random.Range (-60, 30));
		} else {
			if (waveno < 6) {
				randomloc = new Vector3 (Random.Range (-60, 60), Random.Range (6, 13), Random.Range (-60, 60));
			}
			/*if (waveno == 7) {
				randomloc = new Vector3 (Random.Range (-60, 60), Random.Range (6, 13), Random.Range (-60, 160));
			}*/
			if (waveno >= 8) {
				randomloc = new Vector3 (Random.Range (-160, 60), Random.Range (6, 13), Random.Range (-60, 160));
			}
		}
	}

	void randomgroundiespawnloc() {
			if (waveno < 6) {
			randomgroundieloc = new Vector3 (Random.Range (-35, 35), 2.2f, Random.Range (-30, 40));
			}
			/*if (waveno == 7) {
				randomloc = new Vector3 (Random.Range (-60, 60), Random.Range (6, 13), Random.Range (-60, 160));
			}*/
			if (waveno >= 8) {
			randomgroundieloc = new Vector3 (Random.Range (-135, 35), 2.2f, Random.Range (-30, 140));
			}
		}
	}

