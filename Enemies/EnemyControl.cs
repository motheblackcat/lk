using UnityEngine;

public class EnemyControl : MonoBehaviour {
    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rb;
    public AudioClip hit;
    public GameObject corpse;
    public float health = 2;
    public float damages = 25;
    public float pushX = 10;
    public float pushY = 10;
    void Start() {
        /** TODOL: Set health, damages and pushes according to type */
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
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
            bool pos = GameObject.Find("Player").transform.position.x > rb.transform.position.x;
            rb.AddForce(new Vector2(pos ? -pushX : pushX, pushY), ForceMode2D.Impulse);
            animator.SetTrigger("hurt");
            audioSource.PlayOneShot(hit);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") animator.SetTrigger("atk");
    }
}