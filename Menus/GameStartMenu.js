// This script manage the game start menu.
import UnityEngine.SceneManagement;
import UnityEngine.EventSystems;
import UnityEngine.UI;
var audioSource : AudioSource;

function Start() {
    Cursor.visible = false;
}
function Update(){
    if (EventSystem.current.currentSelectedGameObject == null) {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
}

function DemoStart() {
    GameStart();
}

function DemoQuit() {
    Application.Quit();
}

function GameStart() {
    audioSource.Play();
    GameObject.Find("StartButton").GetComponent(Button).interactable = false;
    GameObject.Find("QuitButton").GetComponent(Button).interactable = false;
    yield WaitForSeconds(0.5);
    SceneManager.LoadScene("Scene_0_Intro");
}