// This script disable the camera script when the player reach the border of the screen
function OnTriggerStay2D(other : Collider2D) {
    if (other.tag == "Player" ){
        GameObject.Find("MainCamera").GetComponent(CameraControl).enabled = false;
    }
}

function OnTriggerExit2D(other : Collider2D) {
    if (other.tag == "Player" ){
        GameObject.Find("MainCamera").GetComponent(CameraControl).enabled = true;
    }
}