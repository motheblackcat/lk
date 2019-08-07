using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeEventHandler : MonoBehaviour {
    private void Update() {
        if (Input.GetButtonDown("Jump") && GameObject.Find("DialogBox").GetComponent<DialogManager>().inDialog) {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    // NEED A MORE GENERAL WAY TO HANDLE NPC EVENTS
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            GetComponent<Animator>().enabled = false;
        }
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}