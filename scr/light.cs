using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			GetComponent<Light> ().intensity = Mathf.Lerp (0, 1f, GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower / 100);
		}
		//GetComponent<Light> ().range = Mathf.Lerp (25, 35, GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower/100);
		//GetComponent<Light> ().spotAngle = Mathf.Lerp (100, 120, GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower/100);
		
	}

