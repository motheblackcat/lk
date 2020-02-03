using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
    Image gamepadUI;
    Image keyboardUI;
    Canvas pauseUI;
    AudioSource audioSource;
    PlayerState playerState;
    public bool paused = false;

    void Start() {
        gamepadUI = GameObject.Find("GamepadUI").GetComponent<Image>();
        keyboardUI = GameObject.Find("KeyboardUI").GetComponent<Image>();
        playerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        pauseUI = GameObject.Find("PauseUI").GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }

    void Update() {
        if (Input.GetButtonDown("Start")) Pause();
        if (paused && Input.GetButtonDown("Select")) Application.Quit();
        gamepadUI.enabled = playerState.isGamepad;
        keyboardUI.enabled = !playerState.isGamepad;
    }

    void Pause() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        AudioListener.pause = paused;
        audioSource.enabled = paused;
        pauseUI.enabled = paused;
    }
}