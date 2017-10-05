// This script control the slime type enemy.
// 2 - HEALTH
// 3 - AUDIO
// 4 - FLICK
// 5 - PUSHBACK
// 6 - DEATH

// These variables are used in the health part.
var enemyHealth = 2.0;
var gotHurt = false;
// These variables are used in the audio part.
var deathSound : AudioClip;
var hitSound : AudioClip;
var moveSound : AudioClip;
var isPlayed = false;
// These variables are used in the attack part.
var attackRate = 0.5;
var nextAttack : float;
// These variables are used in the push back part.
var pushUp = 300;
var pushBack = 300;

// 2 - HEALTH
// This function allow the enemy to receive damages and die.
function OnTriggerEnter2D (other: Collider2D) {
    var swordControl = GameObject.FindWithTag("Weapon").GetComponent(SwordControl);
    var playerControl = GameObject.FindWithTag("Player").GetComponent(PlayerControl);
    var swControl = GameObject.Find("ShotSpawn").GetComponent(SWShotControl);
    
    if (other.gameObject.tag == "Weapon" && Time.time > playerControl.nextAttack) {
        gotHurt = true;
        move = false;
        enemyHealth -= swordControl.swordDamage;
        playerControl.nextAttack = Time.time + playerControl.attackRate;
        Audio ();
        Flick ();
        PushBack ();
        yield WaitForSeconds (0.5);
        gotHurt = false;
        move = true;
    }

    if (other.gameObject.tag == "SWeapon" && Time.time > playerControl.nextAttack) {
        move = false;
        enemyHealth -= swControl.swDamage;
        playerControl.nextAttack = Time.time + playerControl.attackRate;
        Audio ();
        Flick ();
        PushBack ();
        yield WaitForSeconds (0.5);
        move = true;
    }
}

// 3 - AUDIO
// This function control the enemy audio.
function Audio () {
    var audioSource = GetComponent(AudioSource);
    if (enemyHealth <= 0 && !isPlayed) {
        audioSource.PlayOneShot(deathSound);
        isPlayed = true;
    }
    if (enemyHealth >= 0.1 && GameObject.FindWithTag("Weapon")) {
        audioSource.PlayOneShot(hitSound);
    }
}

// 4 - FLICK
// This function make the enemy sprite flicker to indicate he received damage.
function Flick () {
    var sprite = GetComponent(SpriteRenderer);
    sprite.enabled = false;
    yield WaitForSeconds (0.05);
    sprite.enabled = true;
    yield WaitForSeconds (0.05);
    sprite.enabled = false;
    yield WaitForSeconds (0.05);
    sprite.enabled = true;
    yield WaitForSeconds (0.05);
    sprite.enabled = false;
    yield WaitForSeconds (0.05);
    sprite.enabled = true;
}

// 5 - PUSHBACK
// This function push the enemy back away from the player when he takes damages.
function PushBack () {
    var playerTransform = GameObject.FindWithTag("Player").GetComponent(Transform);
    var relativePoint = transform.InverseTransformPoint(playerTransform.position);
    if (relativePoint.x < 0.0) {
        GetComponent(EnemyControlSlime).enabled = false;
        GetComponent(Rigidbody2D).AddForce(transform.up * pushUp);
        GetComponent(Rigidbody2D).AddForce(transform.right * pushBack);
        yield WaitForSeconds(0.5);
        GetComponent(EnemyControlSlime).enabled = true;
    }

    if (relativePoint.x > 0.0) {
        GetComponent(EnemyControlSlime).enabled = false;
        GetComponent(Rigidbody2D).AddForce(transform.up * pushUp);
        GetComponent(Rigidbody2D).AddForce(-transform.right * pushBack);
        yield WaitForSeconds(0.5);
        GetComponent(EnemyControlSlime).enabled = true;;
    }
}

// 6 - DEATH
// This function play the death sound and animation when the enemy health reach zero and then destroys it.
function Death () {
    GetComponent(Animator).SetTrigger("die");
    Destroy(GetComponent(BoxCollider2D));
    GetComponent(Rigidbody2D).simulated = false;
    Audio ();
    yield WaitForSeconds(1);
    Destroy(gameObject);
}