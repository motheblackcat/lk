using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public List<GameObject> sWeapons;
    PlayerHealth playerHealthScript;
    PlayerSWeapons playerSWeaponsScript;
    public float playerHealth = 100;

    private void Start() {
        // if (GameObject.Find("PlayerState"))Destroy(GameObject.Find("PlayerState"));
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerSWeaponsScript = GameObject.Find("Player").GetComponent<PlayerSWeapons>();
        DontDestroyOnLoad(gameObject);
    }

    // TODO: Will be refactored with a flexible playerHealthMax var
    private void Update() {
        if (playerHealthScript.isDead) {
            playerHealth = 100;
        }
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