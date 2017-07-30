using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jpnoise : MonoBehaviour {
	public AudioSource audios;

	// Use this for initialization
	void Start () {
		audios = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && GameObject.Find("player").GetComponent<player>().jpmeter > 1) {
			audios.volume = 0.2f;
		} else {
			audios.volume = 0;
		}
	}
}
