using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBobMeeting : MonoBehaviour {
    GameObject player;

    private void Start() {
        player = GameObject.Find("Player");
    }

    // This function needs to be refactored to make it generic for all npcs
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !GameObject.Find("DialogBox").GetComponent<DialogManager>().eventDone) {
            player.GetComponent<PlayerControl>().canMove = false;
            player.GetComponent<Animator>().SetBool("run", false);
        }
        if (GameObject.Find("DialogBox").GetComponent<Image>().enabled && Input.GetButtonDown("Jump")) {
            GameObject.Find("DialogBox").GetComponent<DialogManager>().eventDone = true;
            player.GetComponent<PlayerControl>().canMove = true;
        }
    }
}
