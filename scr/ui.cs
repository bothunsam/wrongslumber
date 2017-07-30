using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Canvas thiscanvas = gameObject.GetComponentInChildren<Canvas> ();
		thiscanvas.renderMode = RenderMode.ScreenSpaceCamera;
		thiscanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		thiscanvas.planeDistance = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
