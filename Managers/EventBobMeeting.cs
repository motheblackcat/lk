using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBobMeeting : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D other) {
        GameObject axe = GameObject.Find("Axe");
        if (other.gameObject.tag == "Player" && axe) {
            axe.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        };
        if (axe && axe.transform.position.y < 1.4) {
            axe.GetComponent<Animator>().enabled = false;
            axe.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
