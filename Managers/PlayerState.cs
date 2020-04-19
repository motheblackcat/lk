using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        isGamepad = Gamepad.current != null;
    }

    public void Save() {
        float playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        playerMaxHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerMaxHealth;
        playerCurrentHealth = playerHealth > 0 ? playerHealth : playerMaxHealth;
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
        sWeapon = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapon;
    }
}