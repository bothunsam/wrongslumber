using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainmenuscreen : MonoBehaviour {
	public GameObject bed;
	public GameObject player;
	public GameObject ui;
	public GameObject infotext;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().gamestarted = false;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().inverter = false;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().post = true;

	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find ("sensdisplay").GetComponent<Text> ().text = "sensitivity " + GameObject.Find ("Sens").GetComponent<Slider> ().value.ToString ("F2");
		GameObject.Find ("globalevents").GetComponent<globalevents> ().sensmultiplier = GameObject.Find ("Sens").GetComponent<Slider> ().value;
		
	}

	public void StartGame() {
		GameObject.Find ("globalevents").GetComponent<globalevents> ().gamestarted = true;
		Instantiate (bed, new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject playerspawn = Instantiate (player, new Vector3 (-48, 5, 40), Quaternion.Euler(0, -230, 0));
		playerspawn.name = "player";
		GameObject uispawn = Instantiate (ui, new Vector3 (0, 0, 0), Quaternion.identity);
		uispawn.name = "ui";
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnamt = 1;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnercounter = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower = 100;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().powervaluechanger = 100;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().waveno = 0;
		Destroy (this.gameObject);

	}

	public void InfoActive() {
		infotext.SetActive (true);
	}
	public void InfoDeactive() {
		infotext.SetActive (false);
	}

	public void MouseInverter() {
		GameObject.Find("globalevents").GetComponent<globalevents>().inverter = !GameObject.Find("globalevents").GetComponent<globalevents>().inverter;
	}

	public void PostToggler() {
		GameObject.Find("globalevents").GetComponent<globalevents>().post = !GameObject.Find("globalevents").GetComponent<globalevents>().post;
	}
}
