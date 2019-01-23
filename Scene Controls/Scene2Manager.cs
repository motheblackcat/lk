using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            SceneManager.LoadScene(1);
        }
    }
}
