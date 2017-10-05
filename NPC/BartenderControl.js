//This script control the bartender behaviour and dialogs at the intro scene.
function Update () {
    var player = GameObject.FindWithTag("Player").GetComponent(Transform);
    var sprite = GetComponent(SpriteRenderer);
    if (player.transform.position.x > transform.position.x) {      
        sprite.flipX = true;
    }
    if (player.transform.position.x < transform.position.x) {
        sprite.flipX = false;
    }
    if (GameObject.Find("TextBoxManager").GetComponent(NPCDialog).isActive) {
        GameObject.Find("BarArrowUp").GetComponent(SpriteRenderer).enabled = false;
    }
}

function OnTriggerStay2D(other : Collider2D) {
    if (other.tag == "Player") {
        GameObject.Find("BarArrowUp").GetComponent(SpriteRenderer).enabled = true;
        GetComponent(Animator).SetBool("watch", true);
    }
}

function OnTriggerExit2D(other : Collider2D) {
    if (other.tag == "Player") {     
        GameObject.Find("BarArrowUp").GetComponent(SpriteRenderer).enabled = false;
        GetComponent(Animator).SetBool("watch", false);
    }
}