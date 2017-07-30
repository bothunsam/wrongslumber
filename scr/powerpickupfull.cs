using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerpickupfull : MonoBehaviour {
	public float timespawnedat;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		timespawnedat = Time.time;
		transform.LookAt (GameObject.Find ("player").transform.position);
		StartCoroutine (Late (0.1f));
	}
		
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timespawnedat > 15) {
			Destroy (transform.parent.gameObject);
		}

		if (Vector3.Distance (transform.position, GameObject.Find ("player").transform.position) < 30) {
			transform.LookAt (GameObject.Find ("player").transform);
			transform.Translate (Vector3.forward * Time.deltaTime * 10);
		}


	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower += 100;
			Destroy (transform.parent.gameObject);
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.clip = GameObject.Find("globalevents").GetComponent<globalevents>().fluids;
			GameObject.Find("globalevents").GetComponent<globalevents>().audios.Play ();
		}
		if (collision.gameObject.tag == "cig") {
			Physics.IgnoreCollision (collision.collider, GetComponent<Collider> ());

		}
	}

	public IEnumerator Late(float time) {
		yield return new WaitForSeconds (time);
		rb.AddForce (transform.forward * Vector3.Distance (transform.position, GameObject.Find ("player").transform.position) * 100);
		rb.AddForce (transform.up * 20);

	}
	}
