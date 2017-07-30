using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemylookat : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player").gameObject;	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform.position);
	}
}
