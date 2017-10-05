// This script control the behaviour of the secondary weapon Axes.
var speed = 300;
var height = 600;
var isThrown = false;
var test = true;

function Update () {
    var sprite = GetComponent(SpriteRenderer);
    var playerSprite = GameObject.Find("Player").GetComponent(SpriteRenderer);
    var anim = GetComponent(Animator);
    
    if (playerSprite.flipX && !isThrown){
        sprite.flipX = true;
        GetComponent(Rigidbody2D).AddForce(-transform.right * speed);
        if (Input.GetAxis("Horizontal")) {
            GetComponent(Rigidbody2D).AddForce(-transform.right * speed * 1);
        }
        GetComponent(Rigidbody2D).AddForce(transform.up * height);
        anim.SetTrigger("flipx");
        isThrown = true;
    }

    if (!playerSprite.flipX && !isThrown){
        GetComponent(Rigidbody2D).AddForce(transform.right * speed);
        if (Input.GetAxis("Horizontal")) {
            GetComponent(Rigidbody2D).AddForce(transform.right * speed * 1);
        }
        GetComponent(Rigidbody2D).AddForce(transform.up * height);
        isThrown = true;
    }

    LifeSpan ();
}

function LifeSpan () {
    yield WaitForSeconds (2);
    Destroy(gameObject);
}

function OnTriggerEnter2D (co2d : Collider2D) {
    if (co2d.gameObject.tag == "Enemy" || co2d.gameObject.tag == "Ground") {
        Destroy(gameObject);
    }
}