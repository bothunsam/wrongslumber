using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathscreen : MonoBehaviour {
	public GameObject bed;
	public GameObject player;
	public GameObject ui;
	public GameObject mainmenu;

	// Use this for initialization
	void Start () {
		GameObject.Find ("globalevents").GetComponent<globalevents> ().gamestarted = false;
		Destroy (GameObject.Find ("ui").gameObject);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		GameObject.Find ("killdisplay").GetComponent<Text> ().text = GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		GameObject uispawn = Instantiate (ui, new Vector3 (0, 0, 0), Quaternion.identity);
		uispawn.name = "ui";
		GameObject.Find ("globalevents").GetComponent<globalevents> ().gamestarted = true;
		Instantiate (bed, new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject playerspawn = Instantiate (player, new Vector3 (-48, 5, 40), Quaternion.Euler(0, -230, 0));
		playerspawn.name = "player";
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnamt = 1;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().enemycount = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().killcount = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().spawnercounter = 0;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower = 100;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().powervaluechanger = 100;
		GameObject.Find ("globalevents").GetComponent<globalevents> ().waveno = 0;
		Destroy (this.gameObject);

	}

	public void MainMenu(){
		Destroy (this.gameObject);
		Instantiate (mainmenu, new Vector3 (0, 0, 0), Quaternion.identity);
	}
}
