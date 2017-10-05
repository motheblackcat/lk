// This part flip the player's BoxCollider2D Trigger in the direction he move
function Update() {
    if (GetComponentInParent(SpriteRenderer).flipX) {       
        GetComponent(BoxCollider2D).offset.x = -0.06;
    }

    else {
        GetComponent(BoxCollider2D).offset.x = 0.06;
    }
}