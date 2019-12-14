using UnityEngine;

public class SimpleWarp : MonoBehaviour {
    GlobalManager globalManager;
    SceneLoader sceneLoader;

    private void Start() {
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons) {
                button.enabled = button.name == (globalManager.isGamepad ? "ButtonA" : "SpaceBar");
            }

            if (Input.GetButtonDown("Jump")) {
                sceneLoader.sceneIndex = 2;
                sceneLoader.StartLoadScene();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons)button.enabled = false;
        }
    }
}