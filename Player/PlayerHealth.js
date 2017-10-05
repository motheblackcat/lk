import UnityEngine.SceneManagement;
import UnityEngine.UI;

// This variable is a trigger used for audio and animation.
var isDead : boolean;

// This variable is used by the health bar.
var playerHealth = 1.0;

// Theses variable are used by the Flick() function.
var playerHurt = false;
var timer = 0.6;

// This variable is used in the PushBack() function.
var pushUp = 300;
var pushBack = 300;

var enemyTouched : GameObject;

function Update() {
// This part links the health bar image to a numerical amount and define the death condition.
    var playerHealthBar : Image = GameObject.Find("LifeBarFill").GetComponent(Image);
    playerHealthBar.fillAmount = playerHealth;
    if (playerHealth <= 0) {
        Death();
    }
}

function OnTriggerEnter2D (other : Collider2D) {
    if (!other.isTrigger) {
        if (other.gameObject.tag == "Enemy") {
            enemyTouched = other.gameObject;
            TakeDamage(enemyTouched);
        }
    }
}

// This function manage how the player behave when he receives damage.
function TakeDamage(enemyTouched) {
    playerHealth -= 0.25;
    if (playerHealth > 0) {
        playerHurt = true;
        Flick();
        PushBack(enemyTouched);
        yield WaitForSeconds (timer);
        playerHurt = false;
    }    
}

// This function make the player's sprite flicker.
function Flick() {
    var sprite = GetComponent(SpriteRenderer);
    var swordSprite = GameObject.FindWithTag("Weapon").GetComponent(SpriteRenderer);
    while (playerHurt) {
        yield WaitForSeconds(0.1);
        sprite.enabled = !sprite.enabled;
        swordSprite.enabled = !swordSprite.enabled;
    }
    sprite.enabled = true;
    swordSprite.enabled = true;
}

// This function push the player back away from the enemy when he takes damages.
function PushBack (enemy : GameObject) {
    var enemyTransform = enemy.GetComponent(Transform);
    var rigidbody = GetComponent(Rigidbody2D);
    var playerCanMove = GetComponent(PlayerControl).playerCanMove;
    
    if (enemyTransform.position.x > GetComponent(Transform).position.x) {
        rigidbody.AddForce(transform.up * pushUp);
        rigidbody.AddForce(-transform.right * pushBack);
    }

    else {
        rigidbody.AddForce(transform.up * pushUp);
        rigidbody.AddForce(transform.right * pushBack);
    }

    GetComponent(PlayerControl).playerCanMove = false;
    yield WaitForSeconds(timer);
    GetComponent(PlayerControl).playerCanMove = true;
}

// This function manage the player's death.
function Death () {
    isDead = true;
    GetComponent(PlayerControl).playerCanMove = false;
    yield WaitForSeconds (2);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}