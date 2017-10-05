function OnTriggerEnter2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GameObject.Find("Plains").GetComponent(Parallaxing).enabled = false;
    }
}

function OnTriggerExit2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GameObject.Find("Plains").GetComponent(Parallaxing).enabled = true;
    }
}