using UnityEngine;

public class EnemyMoveControl : MonoBehaviour {
    GameObject player;
    SpriteRenderer sprite;
    BoxCollider2D box;
    Rigidbody2D rb;
    EnemyHealthControl enemyHealthControl;
    public bool canSee = false;
    public float moveSpeed = 3f;
    float boxOffsetX;

    void Start() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemyHealthControl = GetComponent<EnemyHealthControl>();
        box = GetComponentsInChildren<BoxCollider2D>()[0];
        boxOffsetX = box.offset.x;
    }

    void Update() {
        Move();
    }

    void Move() {
        bool enemyCanMove = canSee && !enemyHealthControl.isStunned && !player.GetComponent<PlayerHealth>().isDead;

        if (enemyCanMove)rb.velocity = new Vector2(player.transform.position.x > transform.position.x ? moveSpeed : -moveSpeed, 0);

        if (!enemyHealthControl.isDead) {
            sprite.flipX = player.transform.position.x > transform.position.x;
            box.offset = new Vector2(sprite.flipX ? -boxOffsetX : boxOffsetX, box.offset.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")canSee = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")canSee = false;
    }
}