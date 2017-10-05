// This script control the player's movements.
// These variables control the player's moving and jumping speed.
var runSpeed = 7;
var jumpSpeed = 10;

// This variable is used to control if the player is allowed to move or not.
var playerCanMove = true;

// This variable is used to check if the player is in contact with the ground.
var isGrounded : boolean;

// This variable is used by the attack script and for animation.
var isAttacking : boolean;

// This variable is used to control the orientation of the trigger collider.
var triggerCol : Collider2D;

// This variable is used in the NPCs Dialog management part.
var NPC : GameObject;

// This part define the character's controls.
function Update () {
    PlayerMove();
}

function PlayerMove(){
    var rb2d = GetComponent(Rigidbody2D); 
    var sprite = GetComponent(SpriteRenderer);
    var move = Vector2(Input.GetAxis("Horizontal"), 0);

    if (playerCanMove) {
        transform.position += move * runSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb2d.velocity = new Vector2(0.0f, jumpSpeed);
        }

        if (Input.GetButtonDown("Attack")) {
            Debug.Log("Player attacked.");
            isAttacking = true;
        }

    // This part flip the player's sprite and the two BoxCollider2D in the direction he moves.
        if ((Input.GetAxis("Horizontal") > 0)) {
            sprite.flipX = false;
            GetComponent(BoxCollider2D).offset.x = 0.06;
            triggerCol.offset.x = 0.06;  
        }

        if ((Input.GetAxis("Horizontal") < 0)) {
            sprite.flipX = true;
            GetComponent(BoxCollider2D).offset.x = -0.06;
            triggerCol.offset.x = -0.06;
        }
    }

    if (sprite.flipX) {
        GameObject.Find("Ghost").GetComponent(SpriteRenderer).flipX = true;        
    }
    else {
        GameObject.Find("Ghost").GetComponent(SpriteRenderer).flipX = false;        
    }
}

function OnTriggerStay2D (other : Collider2D) {
// This part acquire the NPC who's in contact with the player.
    if (other.gameObject.tag == "NPC") {
        NPC = other.gameObject;
    }

// This part check if the player is on the ground.
    if (other.gameObject.tag == "Ground") {
        isGrounded = true;       
    }
}

function OnTriggerExit2D (other: Collider2D) {
// This part check if the player left the ground.    
    if (other.gameObject.tag == "Ground") {
        isGrounded = false;
    }
}