var SSword : GameObject;

function OnTriggerEnter2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GameObject.Find("Sword").SetActive(false);
        SSword.SetActive(true);
        GameObject.Find("Player").GetComponent(PlayerControl).attackTrigger = SSword.GetComponent(Collider2D);
        Destroy(gameObject);
    }
}