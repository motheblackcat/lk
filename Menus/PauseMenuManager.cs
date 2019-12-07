using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
    Image gamepadUI;
    Image keyboardUI;
    Canvas pauseUI;
    GlobalManager globalManager;
    public bool paused = false;

    void Start() {
        gamepadUI = GameObject.Find("GamepadUI").GetComponent<Image>();
        keyboardUI = GameObject.Find("KeyboardUI").GetComponent<Image>();
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
        pauseUI = GameObject.Find("PauseUI").GetComponent<Canvas>();
    }

    void Update() {
        if (Input.GetButtonDown("Start"))paused = !paused;
        if (paused && Input.GetButtonDown("Select"))Application.Quit();
        gamepadUI.enabled = globalManager.isGamepad;
        keyboardUI.enabled = !globalManager.isGamepad;
        Time.timeScale = paused ? 0 : 1;
        AudioListener.pause = paused;
        GetComponent<AudioSource>().enabled = paused;
        pauseUI.enabled = paused;
    }
}