using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundie : MonoBehaviour {
	private float health;
	public GameObject powerspawn;
	private bool isColliding;
	public GameObject player;
	private float valuestorer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player").gameObject;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnamt++;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount++;
		health = 50;
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
		transform.Translate (Vector3.forward * Time.deltaTime * 24);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.transform.position - transform.position), 0.04f);
		if (health <= 0) {
			Instantiate (powerspawn, transform.position, Quaternion.identity);
			GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount--;
			GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount++;
			GameObject.Find ("globalevents").GetComponent<globalevents> ().killtime = Time.time;
			valuestorer = Random.Range (0, 100);
			print (valuestorer);
			if (valuestorer >= 80) {
				GameObject.Find ("player").GetComponent<player> ().fourtimesstarted = Time.time;
				GameObject.Find ("globalevents").GetComponent<globalevents> ().audios.clip = GameObject.Find ("globalevents").GetComponent<globalevents> ().fourx;
				GameObject.Find ("globalevents").GetComponent<globalevents> ().audios.Play ();
			}
			Destroy (this.gameObject);

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
		//	StartCoroutine (HitFlash ());
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.clip = GameObject.Find("globalevents").GetComponent<globalevents>().hit;
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.Play ();
		}

	
	}

//	public IEnumerator HitFlash(){
		//GetComponentInChildren<SpriteRenderer> ().sprite = flash;
		//yield return new WaitForSeconds (0.1f);
		//GetComponentInChildren<SpriteRenderer> ().sprite = currentsprite;
//	}
}
