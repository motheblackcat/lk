using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool paused = false;
	Image joyCon;
	Image keyCon;
    GameObject pauseUI;
	GameObject player;
	PlayerControl playerControl;
	PlayerAnimation playerAnimation;
	PlayerAttack playerAttack;
	// PlayerAudio	playerAudio;

	void Start() {
		player = GameObject.Find("Player");
		playerControl = player.GetComponent<PlayerControl>();
		playerAnimation = player.GetComponent<PlayerAnimation>();
		playerAttack = player.GetComponent<PlayerAttack>();
		// playerAudio = player.GetComponent<PlayerAudio>();
        pauseUI = GameObject.Find("PauseUI");
        pauseUI.GetComponent<AudioSource>().ignoreListenerPause = true;
        joyCon = GameObject.Find("JoyCon Text").GetComponent<Image>();
        keyCon = GameObject.Find("KeyCon Text").GetComponent<Image>();
	}

	void Update() {
		GamepadDetection();
		PauseDetection();
	}

	void GamepadDetection() {
		// Gamepad detection is clunky
		foreach (string gamePad in Input.GetJoystickNames()) {
			joyCon.enabled = gamePad == "" ? false : true;
			keyCon.enabled = gamePad == "" ? true : false;
		}
	}

	void PauseDetection() {
		if (Input.GetButtonDown("Start")) { paused = !paused; }
        if (paused) { Pause(); } else { Resume(); }
	}

	void Pause() {
		Time.timeScale = 0;
		AudioListener.pause = true;
		pauseUI.SetActive(true);
		playerControl.enabled = false;
		playerAnimation.enabled = false;
		playerAttack.enabled = false;
		// playerAudio.enabled = false;
		if (Input.GetButtonDown("Select")) { Application.Quit(); }
	}

	void Resume() {
		Time.timeScale = 1;
		AudioListener.pause = false;
		pauseUI.SetActive(false);
		playerControl.enabled = true;
		playerAnimation.enabled = true;
		playerAttack.enabled = true;
		// playerAudio.enabled = true;
	}
}
