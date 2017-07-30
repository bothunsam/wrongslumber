using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour {
	private float health;
	public GameObject powerspawn;
	public GameObject enemyprojectile;
	private float movespeed;
	private float scale;
	private bool isColliding;
	public Sprite red;
	public Sprite blue;
	public Sprite oj;
	public Sprite flash;
	public Sprite currentsprite;
	public GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player").gameObject;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnamt++;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount++;
		GameObject.Find("globalevents").GetComponent<globalevents>().spawnercounter = 0;
		scale = Random.Range (1.5f, 5.5f);
		if (1 < scale && scale < 2.3f) {
			transform.localScale = new Vector3 (1, 1, 1);
			health = 25;
			movespeed = Random.Range (10, 15);
			StartCoroutine (EnemyShootSlow ());
			GetComponentInChildren<SpriteRenderer> ().sprite = blue;
			currentsprite = blue;
		}
		if (2.3f < scale && scale < 3.6f) {
			transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			health = 75;
			movespeed = Random.Range (6, 8);
			StartCoroutine (EnemyShootMed ());
			GetComponentInChildren<SpriteRenderer> ().sprite = oj;
			currentsprite = oj;
		}
		if (3.6f < scale && scale < 5.5f) {
			transform.localScale = new Vector3 (3.5f, 3.5f, 3.5f);
			health = 150;
			movespeed = Random.Range (2, 4);
			StartCoroutine (EnemyShootFast ());
			GetComponentInChildren<SpriteRenderer> ().sprite = red;
			currentsprite = red;
		}
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
		transform.Translate (Vector3.forward * Time.deltaTime * movespeed);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.transform.position - transform.position), 0.005f);
		transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
		if (health <= 0) {
			Instantiate (powerspawn, transform.position, Quaternion.identity);
			//GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnercounter += 1;
			Destroy (this.gameObject);
			GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount--;
			GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount++;
			GameObject.Find ("globalevents").GetComponent<globalevents> ().killtime = Time.time;

		}

		if (transform.position.x > 80 || transform.position.x < (-32 + player.GetComponent<player>().xmovemin)  || transform.position.z > (40 + player.GetComponent<player>().zmovemax)  || transform.position.z < -80) {
			Destroy (this.gameObject);
			GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount--;
		}
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "cig") {
			if (isColliding)
				return;
			isColliding = true;
			health -= 25 * GameObject.Find("player").GetComponent<player>().damagemultiplier;
			Destroy (collision.gameObject);
			StartCoroutine (HitFlash ());
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.clip = GameObject.Find("globalevents").GetComponent<globalevents>().hit;
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.Play ();
		}
	}


	public IEnumerator EnemyShootSlow() {
		while (true) {
			Instantiate (enemyprojectile, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (5);
		}
	}
	public IEnumerator EnemyShootMed() {
		while (true) {
			Instantiate (enemyprojectile, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (3);
		}
	}
	public IEnumerator EnemyShootFast() {
		while (true) {
			Instantiate (enemyprojectile, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (0.5f);
		}
	}

	public IEnumerator HitFlash(){
		GetComponentInChildren<SpriteRenderer> ().sprite = flash;
		yield return new WaitForSeconds (0.1f);
		GetComponentInChildren<SpriteRenderer> ().sprite = currentsprite;
	}
		
}
