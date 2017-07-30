using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnsafety : MonoBehaviour {
	public float timespawnedat;

	// Use this for initialization
	void Start () {
		timespawnedat = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timespawnedat > 0.2f) {
			Destroy (this.gameObject);
		}
	}
}
