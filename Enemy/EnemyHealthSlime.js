var health = 4;

function Update () {
    if (health <= 0) {
        Death();
    }
	
}

function Death() {
    GetComponent(EnemyMovementSlime).canEnemyMove = false;
}