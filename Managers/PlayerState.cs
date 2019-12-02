using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public List<GameObject> sWeapons;
    PlayerHealth playerHealthScript;
    PlayerSWeapons playerSWeaponsScript;
    public float playerHealth = 100;

    private void Awake() {
        GameObject[] instances = GameObject.FindGameObjectsWithTag("PlayerState");
        if (instances.Length > 1)Destroy(instances[1]);
    }

    private void Start() {
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerSWeaponsScript = GameObject.Find("Player").GetComponent<PlayerSWeapons>();
        DontDestroyOnLoad(gameObject);
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