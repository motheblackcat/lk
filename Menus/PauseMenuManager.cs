using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
	Image gamepadUI;
	Image keyboardUI;
	Canvas pauseUI;
	AudioSource audioSource;
	PlayerInputActions playerInputs;
	public bool paused = false;

	void Awake() {
		playerInputs = new PlayerInputActions();
		playerInputs.Player.Pause.performed += ctx => Pause();
		playerInputs.Player.Quit.performed += ctx => Quit();
	}

	void Start() {
		gamepadUI = GameObject.Find("GamepadUI").GetComponent<Image>();
		keyboardUI = GameObject.Find("KeyboardUI").GetComponent<Image>();
		pauseUI = GameObject.Find("PauseUI").GetComponent<Canvas>();
		audioSource = GetComponent<AudioSource>();
		audioSource.ignoreListenerPause = true;
	}

	void Update() {
		gamepadUI.enabled = PlayerState.Instance.isGamepad;
		keyboardUI.enabled = !PlayerState.Instance.isGamepad;
	}

	void Pause() {
		paused = !paused;
		Time.timeScale = paused ? 0 : 1;
		AudioListener.pause = paused;
		audioSource.enabled = paused;
		pauseUI.enabled = paused;
	}

	void Quit() {
		if (paused) Application.Quit();
	}

	void OnEnable() {
		playerInputs.Enable();
	}

	void OnDisable() {
		playerInputs.Disable();
	}
}