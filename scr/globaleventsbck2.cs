using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globaleventsbck2 : MonoBehaviour {
	public GameObject enemy;
	public GameObject enemy2;
	public float playerpower;
	public float powervaluechanger;
	public float spawnercounter;
	public float spawnamt;
	public Vector3 storeenemyspawnloc;
	public Vector3 randomloc;
	public float enemycount;
	public float killcount;
	private float valuestorer;
	public AudioClip hit;
	public AudioSource audios;
	public AudioClip fluids;
	void Awake() {
		Application.targetFrameRate = -1;
		spawnamt = 1;
		enemycount = 0;
		killcount = 0;
	}

	// Use this for initialization
	void Start () {
		audios = GetComponent<AudioSource> ();
		spawnercounter = 0;
		playerpower = 100;
		powervaluechanger = 100;
		
	}

	// Update is called once per frame
	void Update () {
		GameObject.Find ("enemiesremaining").GetComponent<Text> ().text = "Enemies Remaining: " + enemycount.ToString();
		GameObject.Find ("killcount").GetComponent<Text> ().text = "Kills: " + killcount.ToString();
		//playerpower = powervaluechanger - Time.time * 4;
		if (enemycount == 0) {
			spawnercounter++;
			GameObject.Find("player").GetComponent<player>().audios.clip = GameObject.Find("player").GetComponent<player>().enemyspawn;
			GameObject.Find("player").GetComponent<player>().audios.Play ();
			valuestorer = Random.Range (0, 100);
			if (valuestorer > 80) {
				Instantiate (enemy2, transform.position, Quaternion.identity);
			}
				
				for (int i = 0; i < spawnamt; i++) {
					randomspawnloc ();
					Instantiate (enemy, randomloc, Quaternion.identity);
				}
			}

		playerpower -= Time.deltaTime * 5;
		playerpower = Mathf.Clamp (playerpower, 0, 100);
		//GameObject.Find ("valuedisplay").GetComponent<Text> ().text = playerpower.ToString ("F2");
		GameObject.Find ("pwrbar").GetComponent<RectTransform> ().localScale = new Vector3 (playerpower / 100, 1, 1);
		if (playerpower <= 0) {
			GameObject.Find ("valuedisplay").GetComponent<Text> ().text = "DEAD";
		}
		
	}

	void randomspawnloc(){
		randomloc = new Vector3 (Random.Range (-60, 60), Random.Range(6, 13) , Random.Range (-60, 60));
	}
}
