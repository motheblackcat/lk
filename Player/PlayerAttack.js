/*
// These variables are used in the Attack management part.
var attacking = false;
var attackTimer = 0;
var attackCd = 10;
var attackTrigger : Collider2D;

// These variables are used by the enemies to control the amount of damage taken per time.
var attackRate = 0.3;
var nextAttack : float;

function Start() {
// The sword collider is disabled at the start and will be managed by the attack system below.
    attackTrigger.enabled = false;
}

function Update() {
    // This part control the player's attack.
    if (Input.GetButtonDown("Attack") && !attacking && !gameObject.GetComponent(PlayerHealth.playerHurt) && !gameObject.GetComponent(PlayerHealth.isDead) && Time.time > !gameObject.GetComponent(PlayerAnimation.nextSwordAtk)) {
        attacking = true;
        attackTimer = attackCd;
        attackTrigger.enabled = true;
    }

    if (attacking) {
        if (attackTimer > 0){
            attackTimer -= Time.deltaTime;
        }
        else {
            attacking = false;
            attackTrigger.enabled = false;
        }
    }
}*/