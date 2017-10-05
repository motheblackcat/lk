// This script manage all the audio of the player.
var mainAS : AudioSource;
//var runAS : AudioSource;

private var clip : AudioClip;

var jumpSound : AudioClip;
var hurtSound : AudioClip;
var deathSound : AudioClip;

var isPlayed : boolean;

function Update() {
    var isGrounded = GetComponent(PlayerControl).isGrounded;
    var playerCanMove = GetComponent(PlayerControl).playerCanMove;
    var isDead = GetComponent(PlayerHealth).isDead;

    if (playerCanMove) {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            mainAS.PlayOneShot(jumpSound);
        }
        /*
        // Running sound work but the sound overlap, only one sound with variation should be played.
        if (Input.GetAxis("Horizontal") != 0 && isGrounded) {
            if (!runAS.isPlaying) {
                runAS.Play();
            }
        }*/
    }

    if (isDead && !isPlayed) {
        Camera.main.GetComponent(AudioSource).enabled = false;
        clip = deathSound;
        PlaySound(clip);
    }
}

function OnTriggerEnter2D (other : Collider2D) {
    if (!other.isTrigger) {
        if (other.gameObject.tag == "Enemy") {
            if(!GetComponent(PlayerHealth).isDead && GetComponent(PlayerHealth).playerHealth > 0.3) {
                mainAS.PlayOneShot(hurtSound);
            }
        }
    }
}

function PlaySound(clip : AudioClip) {
    mainAS.PlayOneShot(clip);
    isPlayed = true;
    yield WaitForSeconds(2); // When the clip played fully change the isPlayed state.
    isPlayed = false;
}