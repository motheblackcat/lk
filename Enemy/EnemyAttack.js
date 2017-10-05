/*var enemyDamage = 0.25;
var attackRate = 1.0;
var nextAttack : float;
var playerGetDamaged = false;

function OnTriggerStay2D (other : Collider2D) {
    var player = GameObject.FindWithTag("Player");
    if (other.gameObject.tag == "Player" && Time.time > nextAttack) {
        GetComponent(Animator).SetBool("atk", true);
        player.GetComponent(PlayerControl).playerHealth -= enemyDamage;
        playerGetDamaged = true;
        nextAttack = attackRate + Time.time;
        yield WaitForSeconds(0.2);
        GetComponent(Animator).SetBool("atk", false);
    }
}*/