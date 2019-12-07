using UnityEngine;

public class PauseMenuManager : MonoBehaviour {
    public bool paused = false;

    // TODO: Pausing listener prevent pause sound from being played
    void Update() {
        if (Input.GetButtonDown("Start")) { paused = !paused; }
        Time.timeScale = paused ? 0 : 1;
        AudioListener.pause = paused;
        GetComponent<Canvas>().enabled = paused;
        GetComponent<AudioSource>().enabled = paused;
        if (paused && Input.GetButtonDown("Select"))Application.Quit();
    }
}