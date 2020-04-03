using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    Animator animator;
    AudioSource audioSource;
    public AudioClip hit;
    public GameObject corpse;
    public float health = 2;
    public float damages = 25;
    void Start() {
        /** TODO: Set health & damages according to type */
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (health <= 0) {
            Instantiate(corpse, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health > 0) {
            animator.SetTrigger("hurt");
            audioSource.PlayOneShot(hit);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") animator.SetTrigger("atk");
    }
}