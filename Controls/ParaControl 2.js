function OnTriggerEnter2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GameObject.Find("Plains 2").GetComponent(Parallaxing).enabled = true;
    }
}

    function OnTriggerExit2D(other : Collider2D) {
        if (other.gameObject.tag == "Player") {
            GameObject.Find("Plains 2").GetComponent(Parallaxing).enabled = false;
        }
    }