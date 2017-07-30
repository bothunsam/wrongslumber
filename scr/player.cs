using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class player : MonoBehaviour {
	private float lookclamper;
	public Rigidbody rb;
	private Vector3 prevpos;
	private Vector3 movement;
	public GameObject cig;
	private float timefiredat;
	private float timesincefire;
	public Sprite holding;
	public Sprite throwing;
	public Sprite viewmodel;
	public Sprite holdingcolor;
	public Sprite throwingcolor;
	public Sprite viewmodelcolor;
	public Sprite flame1;
	public Sprite flame2;
	public Sprite flame3;
	public Sprite flame4;
	public Sprite flame5;
	public Sprite flame6;
	public Sprite flame7;
	public bool animstart;
	public AudioSource audios;
	public AudioClip spawn;
	public AudioClip hit;
	public AudioClip playerhit;
	public AudioClip enemyspawn;
	public AudioClip fluids;
	public float zmovemin;
	public float zmovemax;
	public float xmovemin;
	public float xmovemax;
	public Vector3 movespeedmultiplier;
	public float jpmeter;
	public PostProcessingBehaviour post;
	public float damagemultiplier;
	public float fourtimesstarted;

	// Use this for initialization
	void Start () {
		fourtimesstarted = -696969;
		damagemultiplier = 1;
		post = Camera.main.GetComponent <PostProcessingBehaviour> ();
		if (GameObject.Find ("globalevents").GetComponent<globalevents> ().post == true) {
			post.enabled = true;
		}
		if (GameObject.Find ("globalevents").GetComponent<globalevents> ().post == false) {
			post.enabled = false;
		}
		GameObject.Find("globalevents").GetComponent<globalevents>().bed2coroutinestart = false;
		GameObject.Find("globalevents").GetComponent<globalevents>().bed4coroutinestart = false;
		jpmeter = 100;
		zmovemin = -42;
		zmovemax = 40;
		xmovemin = -48;
		xmovemax = 48;
		audios = GetComponent<AudioSource> ();
		audios.clip = spawn;
		audios.Play ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		timefiredat = 0;
		GameObject.Find ("valuedisplay").GetComponent<Text> ().text = "";
		animstart = true;
		movespeedmultiplier = new Vector3 (12, 12, 12);
		GameObject.Find ("globalevents").GetComponent<globalevents> ().timespawnedat = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - fourtimesstarted < 10) {
			damagemultiplier = 4;
			GameObject.Find ("fourtimes").GetComponent<Text> ().text = "4x damage";
			GameObject.Find ("viewmodel").GetComponent<Image> ().sprite = viewmodelcolor;
		} else {
			GameObject.Find ("viewmodel").GetComponent<Image> ().sprite = viewmodel;
			GameObject.Find ("fourtimes").GetComponent<Text> ().text = "";
			damagemultiplier = 1;

		}
		if (animstart == true) {
			StartCoroutine(FlameAnim());
		}
		timesincefire = Time.time - timefiredat;
		float Speed = Vector3.Distance (transform.position, prevpos);
		Speed = Speed * 15;
		prevpos = transform.position;

		movement = (Vector3.forward * Input.GetAxisRaw ("Vertical") * 10) + (Vector3.right * Input.GetAxisRaw ("Horizontal") * 10);
		movement = Vector3.ClampMagnitude (movement, 0.05f);
		transform.Rotate (0, Input.GetAxisRaw ("Mouse X") * GameObject.Find ("globalevents").GetComponent<globalevents>().sensmultiplier, 0);

		lookclamper += Input.GetAxisRaw ("Mouse Y") * GameObject.Find ("globalevents").GetComponent<globalevents>().invert * GameObject.Find ("globalevents").GetComponent<globalevents>().sensmultiplier;
		lookclamper = Mathf.Clamp (lookclamper, -90, 90);
		GameObject.Find ("Main Camera").GetComponent<Camera>().transform.localEulerAngles = new Vector3 (lookclamper, 0, 0);
		if (timesincefire > 0.2f) {
			if (Time.time - fourtimesstarted < 10) {
				GameObject.Find ("throwinghand").GetComponent<Image> ().sprite = holdingcolor;
			} else {
				GameObject.Find ("throwinghand").GetComponent<Image> ().sprite = holding;

			}
		} else {
			if (Time.time - fourtimesstarted < 10) {
				GameObject.Find ("throwinghand").GetComponent<Image> ().sprite = throwingcolor;
			} else {
				GameObject.Find ("throwinghand").GetComponent<Image> ().sprite = throwing;
			}
		}

		if (GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower >= 80) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				if (timesincefire > 0.1f) {
					Instantiate (cig, GameObject.Find ("cigspawner").transform.position, Quaternion.Euler (79, 0, 0));
					timefiredat = Time.time;
				}
			}
		} else {
				
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (timesincefire > 0.2f) {
				Instantiate (cig, GameObject.Find ("cigspawner").transform.position, Quaternion.Euler (79, 0, 0));
				timefiredat = Time.time;
			}
		}

		//GameObject.Find ("arm").transform.SetPositionAndRotation (
	}
	}
	void LateUpdate() {
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, xmovemin, xmovemax), transform.position.y, Mathf.Clamp (transform.position.z, zmovemin, zmovemax));
		jpmeter = Mathf.Clamp (jpmeter, 0, 100);
	}

	public IEnumerator FlameAnim() {
		animstart = false;
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame1;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame2;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame3;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame4;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame5;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame6;
		yield return new WaitForSeconds (0.1f);
		GameObject.Find ("flameanimation").GetComponent<Image> ().sprite = flame7;
		yield return new WaitForSeconds (0.1f);
		animstart = true;

	}

	void FixedUpdate() {
		transform.Translate (Vector3.Scale(Vector3.Scale(movement, movespeedmultiplier), new Vector3 (Time.deltaTime * 20, Time.deltaTime * 20, Time.deltaTime * 20)));

		if (Input.GetKey (KeyCode.Space)) {
			if (jpmeter > 0) {
				rb.velocity = Vector3.up * 5;
			}
			jpmeter -= 0.3f;
		} else {
			jpmeter += 0.1f;
		}

	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "enemyprojectile") {
			GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower -= 25;
			Camera.main.GetComponent<camshaker> ().shakeDuration = 0.5f;
			Destroy (collider.gameObject);
			GameObject.Find("player").GetComponent<player>().audios.clip = GameObject.Find("player").GetComponent<player>().playerhit;
			GameObject.Find("player").GetComponent<player>().audios.Play ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "groundie") {
			GameObject.Find ("globalevents").GetComponent<globalevents> ().playerpower -= 50;
			GameObject.Find("player").GetComponent<player>().audios.clip = GameObject.Find("player").GetComponent<player>().playerhit;
			GameObject.Find("player").GetComponent<player>().audios.Play ();
		}
	}
			


}
