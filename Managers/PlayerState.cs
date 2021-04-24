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
		PlayerHealth playerHealthScript = GameObject.FindObjectOfType<PlayerHealth>();
		if (playerHealthScript) {
			float playerHealth = playerHealthScript.playerHealth;
			playerMaxHealth = playerHealthScript.playerMaxHealth;
			playerCurrentHealth = playerHealth > 0 ? playerHealth : playerMaxHealth;
		}

		PlayerSWeapons playerSWeapons = GameObject.FindObjectOfType<PlayerSWeapons>();
		if (playerSWeapons) {
			sWeapons = playerSWeapons.sWeapons;
			sWeapon = playerSWeapons.sWeapon;
		}
	}
}