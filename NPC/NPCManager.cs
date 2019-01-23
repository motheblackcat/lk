using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour {
    bool introDone = false;

    void FixedUpdate() {
        if (Time.time > GameObject.Find("MainCamera").GetComponent<IntroSceneManager>().startTimer) {
            introDone = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && introDone) {
			GameObject.Find(this.name + "/ArrowUp").GetComponent<SpriteRenderer>().enabled = true;
			if (GetComponent<Animator>()) {
				GetComponent<Animator>().SetBool("watch", true);
				GetComponent<Animator>().SetBool("talk", GameObject.Find("DialogBox").GetComponent<Image>().enabled);
			}
			if (tag == "NPC") {
				GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
			}
			if (tag == "Door" && Input.GetAxis("Vertical") > 0) {
				SceneManager.LoadScene(1);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (GetComponent<Animator>()) { GetComponent<Animator>().SetBool("watch", false); }
			GameObject.Find(this.name + "/ArrowUp").GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
