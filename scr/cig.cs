using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cig : MonoBehaviour {
	public Rigidbody rb;
	private float timespawnedat;

	// Use this for initialization
	void Start () {
		timespawnedat = Time.time;
		transform.RotateAround (transform.position, Vector3.up, GameObject.Find ("player").transform.eulerAngles.y);
		transform.RotateAround (transform.position, GameObject.Find("player").transform.right, GameObject.Find ("Main Camera").transform.eulerAngles.x);
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (GameObject.Find ("cigspawner").transform.forward * 10000);
		rb.AddForce (GameObject.Find ("cigspawner").transform.up * 300);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timespawnedat > 5) {
			Destroy (this.gameObject);
		}
		
	}
}
