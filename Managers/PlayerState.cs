using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; private set; }
    public List<GameObject> sWeapons;
    public GameObject sWeapon;
    public List<Quest> quests;
    public bool isGamepad = false;
    public bool introDone = false;
    public float playerMaxHealth = 4f;
    public float playerCurrentHealth = 4f;
    public int lastSceneIndex = 0;

    void Awake() {
        CheckInstance();
    }

    void CheckInstance() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        /** TODO: Check if this way of checking for a gamepad changes with the new input system */
        foreach (string gamepad in Input.GetJoystickNames()) {
            isGamepad = gamepad != "";
        }
    }

    public void Save() {
        playerMaxHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerMaxHealth;
        playerCurrentHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
        sWeapon = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapon;
    }

    public void Reset() {
        playerCurrentHealth = playerMaxHealth;
    }
}