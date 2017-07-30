using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialdimmer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("player") != null) {
			transform.position = GameObject.Find ("player").transform.position;
		}
		transform.RotateAround (new Vector3 (0, 0, 0), Vector3.up, 3 * Time.deltaTime);
		transform.RotateAround (new Vector3 (0, 0, 0), Vector3.forward, 0.5f * Time.deltaTime);
		transform.RotateAround (new Vector3 (0, 0, 0), Vector3.right, 0.3f * Time.deltaTime);
		Renderer renderer = GetComponent<Renderer> ();
		Material dimmat = renderer.material;
		if (GameObject.Find ("globalevents").GetComponent<globalevents> ().gamestarted == true) {
			dimmat.SetColor ("_EmissionColor", Color.Lerp (Color.black, Color.white, GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower / 100));
		} else {
			dimmat.SetColor ("_EmissionColor", Color.white);
		}
	}
		
}
