using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour {
    bool dialogOpen;
    SpriteRenderer dialogArrow;

    void Update() {
        dialogOpen = GameObject.Find("DialogBox").GetComponent<Image>().enabled;
        dialogArrow = GameObject.Find(this.name + "/ArrowUp").GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && Camera.main.GetComponent<IntroSceneManager>().introDone) {
            dialogArrow.enabled = !dialogOpen;
            if (GetComponent<Animator>()) {
                GetComponent<Animator>().SetBool("watch", true);
                GetComponent<Animator>().SetBool("talk", dialogOpen);
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
            dialogArrow.enabled = false;
            Debug.Log(dialogArrow.enabled);
            if (GetComponent<Animator>()) {
                GetComponent<Animator>().SetBool("watch", false);
                GetComponent<Animator>().SetBool("talk", false);
            }
        }
    }
}