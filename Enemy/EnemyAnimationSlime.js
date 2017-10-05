// This script control the slime type enemy animations.
var enemyAtk = false;

function Update() {
    if (GameObject.Find("Player").GetComponent(PlayerHealth).playerHurt && !enemyAtk) {
        PlayAtkAnim();
        enemyAtk = true;
    }
}

function PlayAtkAnim(){
    GetComponent(Animator).SetTrigger("atk");
    yield WaitForSeconds(0.8);
    enemyAtk = false;
}