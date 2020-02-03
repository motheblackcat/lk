using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; private set; }
    public bool isGamepad = false;
    public float playerHealth = 100;
    public List<GameObject> sWeapons;
    public bool introDone = false;
    public bool bobQuest = false;
    public int lastSceneIndex = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        foreach (string gamepad in Input.GetJoystickNames()) {
            isGamepad = gamepad != "" ? true : false;
        }
    }

    public void Save() {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
    }
}