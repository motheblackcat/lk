using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameObject.Find("ArrowUp").GetComponent<SpriteRenderer>().enabled = true;
			if (this.name == "Door" && Input.GetAxis("Vertical") > 0) {
				SceneManager.LoadScene("Scene_1_RoadtoForest");
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameObject.Find("ArrowUp").GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
