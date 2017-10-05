// This script allow the player to start the game by pressing the space key or the start button of a game pad.
import UnityEngine.SceneManagement;
var fadeSpeed = 0.4;
var delay = 2;
var delay2 = 4;
var delay3 = 6;

function Start() {
    Cursor.visible = false;
}

function Update() {
    var time = Time.timeSinceLevelLoad;
    if (time > delay) {
        FadeIn();
    }

    if (time > delay2) {
        FadeOut();
    }

    if (time > delay3) {
        SceneManager.LoadScene("Scene_0_Tavern");
    }
}

function FadeIn() {
    var screen = GameObject.Find("BlackScreen").GetComponent(Image);
    screen.color = Color.Lerp(Color.black, Color.clear, Time.time * fadeSpeed);
}

function FadeOut() {
    var screen = GameObject.Find("BlackScreen").GetComponent(Image);
    screen.color = Color.Lerp(Color.clear, Color.black, Time.time * fadeSpeed);
}