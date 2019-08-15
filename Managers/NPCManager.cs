using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour {
    SpriteRenderer dialogArrow;
    Animator animator;
    SceneLoader sceneLoader;
    bool dialogOpen = false;

    void Start() {
        animator = GetComponent<Animator>();
        sceneLoader = GameObject.Find("Transition").GetComponent<SceneLoader>();
    }

    void Update() {
        dialogOpen = GameObject.Find("DialogBox").GetComponent<Image>().enabled;
        dialogArrow = GameObject.Find(this.name + "/ArrowUp").GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && Camera.main.GetComponent<IntroSceneManager>().introDone) {
            dialogArrow.enabled = !dialogOpen;
            if (animator) {
                animator.SetBool("watch", true);
                animator.SetBool("talk", dialogOpen);
            }
            if (tag == "NPC") {
                GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
            }
            // This is specific to the door of the intro
            if (tag == "Door" && Input.GetAxis("Vertical") > 0) {
                sceneLoader.loadScene = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            dialogArrow.enabled = false;
            if (animator) {
                animator.SetBool("watch", false);
                animator.SetBool("talk", false);
            }
        }
    }
}