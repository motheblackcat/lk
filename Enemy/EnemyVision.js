// This script enable the enemy's sound and movements if the player get into its vision field.
// The enemy's movement are disabled at the start so it will have to see the player before moving.
function Start () {
    GetComponentInParent(EnemyControlSlime).enabled = false;
}

// In this part we make the box collider that is used as a vision field face the same way as the enemy's sprite.
function Update () {
    var sprite = GetComponentInParent(SpriteRenderer);

    if (sprite.flipX){
        transform.localScale.x = -1;
    }
    else {
        transform.localScale.x = 1;
    }
}

function OnTriggerStay2D (other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GetComponentInParent(EnemyControlSlime).enabled = true;
    }
}

function OnTriggerExit2D (other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        GetComponentInParent(EnemyControlSlime).enabled = false;
    }
}