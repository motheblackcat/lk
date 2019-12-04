using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; private set; }
    public List<GameObject> sWeapons;
    PlayerHealth playerHealthScript;
    PlayerSWeapons playerSWeaponsScript;
    public float playerHealth = 100;

    private void Awake() {
        Debug.Log(Instance ? Instance.playerHealth : 0);
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerSWeaponsScript = GameObject.Find("Player").GetComponent<PlayerSWeapons>();
    }

    public void Save() {
        playerHealth = playerHealthScript.playerHealth;
        sWeapons = playerSWeaponsScript.sWeapons;
    }

    // TOFIX: This method is not working (PlayerHealth.Start() always executes last)
    public void Load() {
        playerHealthScript.playerHealth = playerHealth;
        if (sWeapons.Count > playerSWeaponsScript.sWeapons.Count) {
            playerSWeaponsScript.sWeapons = sWeapons;
        }
    }
}