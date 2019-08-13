using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject lastselect;
    public float startTimer = 0.5f;
    bool startGame = false;

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start() {
        lastselect = new GameObject();
    }
    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastselect);
        } else {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.GetButtonDown("Accept")) {
            GetComponent<AudioSource>().Play();
        }

        if (startGame) {
            startTimer -= Time.deltaTime;
            if (startTimer <= 0) {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void StartGame() {
        startGame = true;
        GameObject.Find("QuitButton").GetComponent<Button>().interactable = false;
    }
}