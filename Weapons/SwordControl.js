// This script control the sword behaviour.
var swordDamage = 1;

function Update (){
    SwordAnimation();
}

function SwordAnimation() {
    var animator = GetComponent(Animator);
    var player = GameObject.Find("Player");
    var getPlayerState = player.GetComponent(Animator).GetCurrentAnimatorStateInfo(0);
    var playerSprite = GameObject.FindWithTag("Player").GetComponent(SpriteRenderer);

    if (playerSprite.flipX) {
        transform.localScale.x = -1;
        transform.localPosition.x = -0.75;
    }
    else {
        transform.localScale.x = 1;
        transform.localPosition.x = 0.75;
    }

    if (Input.GetAxis("Horizontal") && player.GetComponent(PlayerControl).playerCanMove) {
        animator.SetBool("run", true);
    }
    else {
        animator.SetBool("run", false);
    }

    if (player.GetComponent(PlayerControl).isGrounded) {
        animator.SetBool("air", false);
    }
    else {
        animator.SetBool("air", true);
    }

    if (player.GetComponent(PlayerControl).attacking) {
        animator.SetBool("attack", true);
    }
    else {
        animator.SetBool("attack", false);
    }

    if (getPlayerState.IsName("Player_Hurt")) {
        animator.SetBool("hurt", true);
    }
    else {
        animator.SetBool("hurt", false);
    }

    if (getPlayerState.IsName("Player_Die")) {
        animator.SetTrigger("die");
    }

    if (getPlayerState.IsName("Player_Throw")) {
        animator.SetBool("throw", true);
    }
    else {
        animator.SetBool("throw", false);
    }

    if (getPlayerState.IsName("Player_JumpAtk")) {
        animator.SetBool("attack", true);
    }
}