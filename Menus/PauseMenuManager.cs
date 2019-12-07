using UnityEngine;

public class PauseMenuManager : MonoBehaviour {
    public bool paused = false;

    void Update() {
        if (Input.GetButtonDown("Start"))paused = !paused;
        if (paused && Input.GetButtonDown("Select"))Application.Quit();

        Time.timeScale = paused ? 0 : 1;
        AudioListener.pause = paused;
        GetComponent<AudioSource>().enabled = paused;
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = paused;
    }
}