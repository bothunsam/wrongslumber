using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyprojectile : MonoBehaviour {
	public float GE;
	public float Px;
	public float Pz;



	// Use this for initialization
	void Start () {
		transform.LookAt (GameObject.Find ("player").transform.position);
		GE = GameObject.Find ("globalevents").GetComponent<globalevents> ().waveno;
		Px = GameObject.Find ("player").GetComponent<player>().xmovemin;
		Pz = GameObject.Find ("player").GetComponent<player>().zmovemax;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (GE < 6) {
			transform.Translate (Vector3.forward * Time.deltaTime * 8);
		} else {
			transform.Translate (Vector3.forward * Time.deltaTime * 25);
		}

		if (transform.position.x > 80 || transform.position.x < (-32 + Px)  || transform.position.z > (40 + Pz)  || transform.position.z < -80) {
			Destroy (this.gameObject);
		}
	
		
	}

	void OnCollisionEnter(Collision collision) {
		
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "projectilekillbox") {
			Destroy (this.gameObject);
		}
		/*if (collision.gameObject.tag == "Player") {
			GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower -= 25;
			Camera.main.GetComponent<camshaker> ().shakeDuration = 0.5f;
			Destroy (this.gameObject);
			GameObject.Find("player").GetComponent<player>().audios.clip = GameObject.Find("player").GetComponent<player>().playerhit;
			GameObject.Find("player").GetComponent<player>().audios.Play ();
		}*/
	}
}
