// This script control the player's animations.
function Update() {
    var animator = GetComponent(Animator);
    var playerCanMove = GetComponent(PlayerControl).playerCanMove;
    var isGrounded = GetComponent(PlayerControl).isGrounded;
    var isHurt = GetComponent(PlayerHealth).playerHurt;
    var isDead = GetComponent(PlayerHealth).isDead;
    var isAttacking = GetComponent(PlayerControl).isAttacking;

    if (playerCanMove) {
        if (Input.GetAxis("Horizontal")) {
            animator.SetBool("run", true);
        }
        else {
            animator.SetBool("run", false);
        }
    }

    if (isGrounded) {
        animator.SetBool("air", false);
    }
    else {
        animator.SetBool("air", true);
    }
    
    if (isHurt) {
        animator.SetBool("hurt", true);
    }
    else {
        animator.SetBool("hurt", false);
    }

    if (isDead) {
        animator.SetBool("hurt", false);
        GetComponent(Animator).SetTrigger("die");
    }
}