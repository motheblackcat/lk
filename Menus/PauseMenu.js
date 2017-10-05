// This Script control the Pause Screen .
var pauseUI : GameObject;
var paused : boolean;
var gamePad = false;

function Update() {
    if(Input.GetJoystickNames().Length > 0) {
        if(Input.GetJoystickNames()[0].Length > 0) {
            gamePad = true;
        }
        else {
            gamePad = false;
        }
    }

    pauseUI.GetComponent(AudioSource).ignoreListenerPause = true;
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

function Pause() {
    Time.timeScale = 0;
    AudioListener.pause = true;
    pauseUI.SetActive(true);
    if(GameObject.Find("Player") != null) {
        GameObject.Find("Player").GetComponent(PlayerControl).enabled = false;
    }
    if(GameObject.Find("Player Intro") != null) {
        GameObject.Find("Player Intro").GetComponent(PlayerControlIntro).enabled = false;
    }
    if (Input.GetButtonDown("Select")) {
        Application.Quit();
    }
    if (gamePad) {
        GameObject.Find("JoyCon Text").GetComponent(Image).enabled = true;
        GameObject.Find("KeyCon Text").GetComponent(Image).enabled = false;
    }
    else {
        GameObject.Find("JoyCon Text").GetComponent(Image).enabled = false;
        GameObject.Find("KeyCon Text").GetComponent(Image).enabled = true;
    }
}

function Resume() {
    Time.timeScale = 1; 
    AudioListener.pause = false;
    pauseUI.SetActive(false);
    if(GameObject.Find("Player") != null) {
        GameObject.Find("Player").GetComponent(PlayerControl).enabled = true;
    }
    if(GameObject.Find("Player Intro") != null) {
        GameObject.Find("Player Intro").GetComponent(PlayerControlIntro).enabled = true;
    }
}