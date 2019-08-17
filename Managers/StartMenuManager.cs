using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour {
    GameObject lastSelect;
    AudioSource audioSource;
    float audioTimer;
    bool startGame;
    bool quitGame;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lastSelect = new GameObject("lastSelect");
        audioSource = GetComponent<AudioSource>();
        audioTimer = audioSource.clip.length;
    }

    void Update() {
        CancelMouseFocus();
        CheckOptions();
        if (Input.GetButtonDown("Accept")) {
            audioSource.Play();
        }
    }

    void CancelMouseFocus() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        } else {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    void CheckOptions() {
        if (startGame || quitGame) {
            audioTimer -= Time.deltaTime;
            if (audioTimer <= 0) {
                if (startGame) { SceneManager.LoadScene(1); } else { Application.Quit(); }
            }
        }
    }

    public void StartGame() {
        startGame = true;
        GameObject.Find("QuitButton").GetComponent<Button>().interactable = false;
    }

    public void QuitGame() {
        quitGame = true;
        GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
    }
}