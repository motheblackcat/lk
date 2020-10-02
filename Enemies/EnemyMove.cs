using UnityEngine;

public class EnemyMove : StateMachineBehaviour {
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Transform player;
    public float moveSpeed = 5f;
    public float moveRange = 20f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        /** TODOL: Set enemy speed, move & attack ranges according to type */
        rb = animator.GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bool playerDead = player.GetComponent<PlayerHealth>().isDead;
        if (Vector2.Distance(player.position, rb.position) <= moveRange && !playerDead) {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            spriteRenderer.flipX = player.position.x > rb.transform.position.x;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
}