using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour {
	private bool isColliding;
	private float health;
	public GameObject powerspawn;
	public GameObject enemyprojectile;

	// Use this for initialization
	void Start () {
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnamt++;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount++;
		transform.localScale = new Vector3 (3, 3, 3);
		health = 25;
	}
	
	// Update is called once per frame
	void Update() {
		isColliding = false;
		if (health <= 0) {
			Instantiate (powerspawn, transform.position, Quaternion.identity);
			GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnercounter += 1;
			Destroy (this.gameObject);
			GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount--;
			GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount++;
		}
	}

	void FixedUpdate () {
		transform.position = (GameObject.Find ("player").transform.position + GameObject.Find ("player").transform.forward * 15) + new Vector3(0, Mathf.Lerp(5, 12, (Mathf.Sin(Time.time * 2) + 1)/2));
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "cig") {
			if (isColliding)
				return;
			isColliding = true;
			health -= 25;
			Destroy (collision.gameObject);
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.clip = GameObject.Find("globalevents").GetComponent<globalevents>().hit;
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.Play ();
		}
	}

	public void EnemyShoot() {
		Instantiate (enemyprojectile, transform.position, Quaternion.identity);
	}
}
