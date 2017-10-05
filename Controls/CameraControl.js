// This script manage the camera.
function Update() {
    if (GameObject.FindWithTag("Player") != null){
        var player = GameObject.FindWithTag("Player");
        transform.position.x = player.transform.position.x;
        //transform.position.y = player.transform.position.y + 3;
    }
}