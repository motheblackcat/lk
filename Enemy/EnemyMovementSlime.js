// This script control the slime type enemy movements.
var speed = 3;
var enemyCanMove : boolean;

function Update () {
    EnemyMove();
}

function OnTriggerStay2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        enemyCanMove = true;
    }
}

function OnTriggerExit2D(other : Collider2D) {
    if (other.gameObject.tag == "Player") {
        enemyCanMove =false;
    }
}

function EnemyMove() {
    var playerTransform = GameObject.Find("Player").GetComponent(Transform);
    var playerIsDead = GameObject.Find("Player").GetComponent(PlayerHealth).isDead;
    var sprite = GetComponent(SpriteRenderer);
    var collider = GetComponent(BoxCollider2D);

    if (enemyCanMove && !playerIsDead) {
        if (playerTransform.transform.position.x > transform.position.x) {      
            transform.Translate(-Vector2.left * speed * Time.deltaTime);
            sprite.flipX = true;
        }
        else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            sprite.flipX = false;
        }
    }

    if (sprite.flipX) {
        collider.offset.x = 3;
    }

    else {
        collider.offset.x = -3;        
    }
}