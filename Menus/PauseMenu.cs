using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool paused = false;
	public bool gamePad = false;
	public GameObject pauseUI;
	Image joyCon;
	Image keyCon;
	GameObject player;
	PlayerControl playerControl;

	void Start() {
		player = GameObject.Find("Player");
		playerControl = player.GetComponent<PlayerControl>();
		pauseUI.GetComponent<AudioSource>().ignoreListenerPause = true;
	}

	void Update() {
		JoystickDetection();
		PauseDetection();
	}

	void JoystickDetection() {
		if(Input.GetJoystickNames().Length > 0) {
			if(Input.GetJoystickNames()[0].Length > 0) {
				gamePad = true;
			}
			else {
				gamePad = false;
			}
		}
	}

	void PauseDetection() {
		if (Input.GetButtonDown("Start")) {
			paused = !paused;
		}

		if (paused) {
			Pause();
		}
		else {
			Resume();
		}
	}

	void Pause() {
		// TOFIX: Camera position change during pause, on resume it lags before following the player again
		Time.timeScale = 0;
		AudioListener.pause = true;
		pauseUI.SetActive(true);
		playerControl.canMove = false;

		if (Input.GetButtonDown("Select")) {
			Application.Quit();
		}

		// Refactor this
		joyCon = GameObject.Find("JoyCon Text").GetComponent<Image>();
		keyCon = GameObject.Find("KeyCon Text").GetComponent<Image>();
		if (gamePad) {
			joyCon.enabled = true;
			keyCon.enabled = false;
		}
		else {
			joyCon.enabled = false;
			keyCon.enabled = true;
		}
	}

	void Resume() {
		Time.timeScale = 1; 
		AudioListener.pause = false;
		pauseUI.SetActive(false);
		playerControl.canMove = true;
	}
}
