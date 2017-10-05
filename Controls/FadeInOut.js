// FadeInOut effect
import UnityEngine.SceneManagement;

var fadeTexture : Texture2D;
var fadeSpeed = 0.2;
var drawDepth = -1000;
 
private var alpha = 1.0; 
private var fadeDir = -1;

function Start() {
    Cursor.visible = false;
    alpha=1;
    fadeIn();
}

function Update(){
    Intro();
}

function Intro(){
    yield WaitForSeconds(8);
    fadeOut();
    yield WaitForSeconds(3);
    SceneManager.LoadScene("Scene_0_Tavern");
}

function OnGUI(){
    alpha += fadeDir * fadeSpeed * Time.deltaTime;  
    alpha = Mathf.Clamp01(alpha);
    GUI.color.a = alpha;
    GUI.depth = drawDepth;
    GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), fadeTexture);
}

function fadeIn(){
    fadeDir = -1;	
}
 
function fadeOut(){
    fadeDir = 1;	
}