// This script control the behaviour of the secondary weapon Daggers.
var speed = 20.0;
var isThrown = false;

function Update () {
    var sprite = GetComponent(SpriteRenderer);
    var playerSprite = GameObject.Find("Player").GetComponent(SpriteRenderer);
    
    if (playerSprite.flipX && !isThrown){
        sprite.flipX = true;
        GetComponent(Rigidbody2D).velocity = -transform.right * speed;
        isThrown = true;
    }

    if (!playerSprite.flipX && !isThrown){
        GetComponent(Rigidbody2D).velocity = transform.right * speed;
        isThrown = true;
    }

    LifeSpan ();
}

function LifeSpan () {
    yield WaitForSeconds (0.5);
    Destroy(gameObject);
}

function OnTriggerEnter2D (co2d : Collider2D) {
    if (co2d.gameObject.tag == "Enemy") {
        Destroy(gameObject);
    }
}