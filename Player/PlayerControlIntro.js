// This script manage the player controls in the tavern intro.
import UnityEngine.SceneManagement;
import UnityEngine.UI;

//  These variables control the player's moving anility and speed.
var speed = 7;
var playerMove : boolean;

var NPC : GameObject;

function Update () {
// This part define the character's movement.
    var sprite = GetComponent(SpriteRenderer);
    var move = Vector2(Input.GetAxis("Horizontal"), 0);
    if (playerMove) {
        transform.position += move * speed * Time.deltaTime;

// This part flip the player's sprite and BoxCollider2D in the direction he moves.
        if ((Input.GetAxis("Horizontal") > 0)) {
            sprite.flipX = false;
        }

        if ((Input.GetAxis("Horizontal") < 0)) {
            sprite.flipX = true;
        }

// This part control the player's animations.
        var animator = GameObject.FindWithTag("Player").GetComponent(Animator);

        if (Input.GetAxis("Horizontal")) {
            animator.SetBool("run", true);
        }
        else {
            animator.SetBool("run", false);
        }
    }
}

function OnTriggerEnter2D (other : Collider2D) {
        if (other.gameObject.tag == "NPC") {
            NPC = other.gameObject;
        }
    }