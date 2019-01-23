using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBobMeeting : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject.Find("Axe").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        };
        if (GameObject.Find("Axe").transform.position.y < 1.4) {
            GameObject.Find("Axe").GetComponent<Animator>().enabled = false;
            GameObject.Find("Axe").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
