using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; private set; }
    public List<GameObject> sWeapons;
    PlayerHealth playerHealthScript;
    PlayerSWeapons playerSWeaponsScript;
    public float playerHealth = 100;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Save() {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        sWeapons = GameObject.Find("Player").GetComponent<PlayerSWeapons>().sWeapons;
    }
}