import UnityEngine.SceneManagement;

function OnTriggerStay2D () {
    GameObject.Find("ArrowUp").GetComponent(SpriteRenderer).enabled = true;
    if (Input.GetAxis("Vertical") > 0) {      
        SceneManager.LoadScene("Scene_1_RoadtoForest");
    }
}

function OnTriggerExit2D () {
    GameObject.Find("ArrowUp").GetComponent(SpriteRenderer).enabled = false;
}