using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public bool paused = false;
	public bool gamePad = false;
	public GameObject pauseUI;
	public Image joyCon;
	public Image keyCon;
	GameObject player;
	PlayerControl playerControl;
	PlayerAnimation playerAnimation;
	PlayerAttack playerAttack;
	PlayerAudio	playerAudio;

	void Start() {
		player = GameObject.Find("Player");
		playerControl = player.GetComponent<PlayerControl>();
		playerAnimation = player.GetComponent<PlayerAnimation>();
		playerAttack = player.GetComponent<PlayerAttack>();
		playerAudio = player.GetComponent<PlayerAudio>();
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

		// Refactor this
		if (gamePad) {
			joyCon.enabled = true;
			keyCon.enabled = false;
		}
		else {
			joyCon.enabled = false;
			keyCon.enabled = true;
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
		Time.timeScale = 0;
		AudioListener.pause = true;
		pauseUI.SetActive(true);
		playerControl.enabled = false;
		playerAnimation.enabled = false;
		playerAttack.enabled = false;
		playerAudio.enabled = false;

		if (Input.GetButtonDown("Select")) {
			Application.Quit();
		}
	}

	void Resume() {
		Time.timeScale = 1; 
		AudioListener.pause = false;
		pauseUI.SetActive(false);
		playerControl.enabled = true;
		playerAnimation.enabled = true;
		playerAttack.enabled = true;
		playerAudio.enabled = true;
	}
}
