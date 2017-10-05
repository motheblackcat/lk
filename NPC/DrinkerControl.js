//This script control the bartender behaviour and dialogs at the intro scene.
function Update () {
    if (GameObject.Find("TextBoxManager").GetComponent(NPCDialog).isActive) {
        GameObject.Find("DrinkArrowUp").GetComponent(SpriteRenderer).enabled = false;
    }
}

function OnTriggerStay2D(other : Collider2D) {
    if (other.tag == "Player") {
        GameObject.Find("DrinkArrowUp").GetComponent(SpriteRenderer).enabled = true;
    }
}

    function OnTriggerExit2D(other : Collider2D) {
        if (other.tag == "Player") {     
            GameObject.Find("DrinkArrowUp").GetComponent(SpriteRenderer).enabled = false;
        }
    }