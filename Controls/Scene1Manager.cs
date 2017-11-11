using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour {
	void Start() {
		Cursor.visible = false;
		GameObject.Find("Sky").transform.parent = GameObject.Find("MainCamera").transform;		
	}

	void Update () {
	}

	void OnTriggerStay2D(Collider2D other) {
		GameObject.Find("MainCamera").GetComponent<CameraFollowPlayer>().enabled = false;
	}

	void OnTriggerExit2D(Collider2D other) {
		GameObject.Find("MainCamera").GetComponent<CameraFollowPlayer>().enabled = true;
	}
}
