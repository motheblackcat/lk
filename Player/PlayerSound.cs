using UnityEngine;

public class PlayerSound : MonoBehaviour {
    AudioSource audioSource;
    PlayerControl playerControl;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public bool deathSoundPlayed = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update() {
        // TODO: Jump sound can be heard without jumping when spamming with Sweapons
        if (Input.GetButtonDown("Jump") && playerControl.isGrounded && playerControl.canMove)audioSource.PlayOneShot(jumpSound);

        if (playerAttack.isAttacking)audioSource.PlayOneShot(attackSound);

        if (playerHealth.tookDamage)audioSource.PlayOneShot(hurtSound);

        if (playerHealth.isDead && !deathSoundPlayed) {
            audioSource.PlayOneShot(deathSound);
            deathSoundPlayed = true;
        }
    }
}