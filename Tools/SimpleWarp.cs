﻿using UnityEngine;

public class SimpleWarp : MonoBehaviour {
    GlobalManager globalManager;
    SceneLoader sceneLoader;
    bool canWarp = false;

    private void Start() {
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump") && canWarp) {
            canWarp = false;
            sceneLoader.StartLoadScene(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        canWarp = true;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons)button.enabled = button.name == (globalManager.isGamepad ? "ButtonA" : "SpaceBar");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons)button.enabled = false;
            canWarp = false;
        }
    }
}