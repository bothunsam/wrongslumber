using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textblinker : MonoBehaviour {
	public bool blinkstart;

	// Use this for initialization
	void Start () {
		blinkstart = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower >= 80) {
			if (blinkstart == true) {
				StartCoroutine (Blinker ());
			}
		
		}
	}

	public IEnumerator Blinker() {
		blinkstart = false;
		GetComponent<Text> ().text = "RAPID FIRE";
		yield return new WaitForSeconds (0.1f);
		GetComponent<Text> ().text = "";
		yield return new WaitForSeconds (0.1f);
		blinkstart = true;
	}
}
