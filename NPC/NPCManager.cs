using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour {
	bool introDone = false;
	
	// Check this script, it needs refactoring.
	void FixedUpdate() {
		if (Time.time > GameObject.Find("MainCamera").GetComponent<IntroSceneManager>().startTimer) {
			introDone = true;
		}
	}

	void DoorManager() {
		if (this.name == "Door") {
			GetComponentInChildren<SpriteRenderer>().enabled = true;
			if (Input.GetAxis("Vertical") > 0) {
				SceneManager.LoadScene("Scene_1_RoadtoForest");
			}
		}
	}

	void BartenderManager(GameObject player) {
		if (this.name == "Bartender") {
			GetComponent<Animator>().SetBool("watch", true);
			GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
			
			if (player.transform.position.x > transform.position.x) {
				GetComponent<SpriteRenderer>().flipX = true;
			} else {
				GetComponent<SpriteRenderer>().flipX = false;				
			}
			
			if (Input.GetAxis("Vertical") > 0) {
				GetComponent<Animator>().SetBool("talk", true);
			}
		}
	}

	void DrinkerManager() {
		if (this.name == "Drinker1") {
			GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
			
			if (Input.GetAxis("Vertical") > 0) {
				GetComponent<Animator>().SetBool("talk", true);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && introDone) {
			DoorManager();
			BartenderManager(other.gameObject);
			DrinkerManager();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (this.name == "Door") {
				GetComponentInChildren<SpriteRenderer>().enabled = false;
			} else {
				GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
			}

			if (this.name == "Bartender") {
				GetComponent<Animator>().SetBool("watch", false);
				GetComponent<Animator>().SetBool("talk", false);
			}

			if (this.name == "Drinker1") {
				GetComponent<Animator>().SetBool("talk", false);
			}
		}
	}
}
