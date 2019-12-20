using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
    Image gamepadUI;
    Image keyboardUI;
    Canvas pauseUI;
    AudioSource audioSource;
    GlobalManager globalManager;
    public bool paused = false;

    void Start() {
        gamepadUI = GameObject.Find("GamepadUI").GetComponent<Image>();
        keyboardUI = GameObject.Find("KeyboardUI").GetComponent<Image>();
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
        pauseUI = GameObject.Find("PauseUI").GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
    }

    void Update() {
        if (Input.GetButtonDown("Start"))Pause();
        if (paused && Input.GetButtonDown("Select"))Application.Quit();
        gamepadUI.enabled = globalManager.isGamepad;
        keyboardUI.enabled = !globalManager.isGamepad;
    }

    void Pause() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        AudioListener.pause = paused;
        audioSource.enabled = paused;
        pauseUI.enabled = paused;
    }
}