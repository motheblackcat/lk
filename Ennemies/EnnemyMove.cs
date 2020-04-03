using UnityEngine;

public class EnnemyMove : StateMachineBehaviour {
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Transform player;
    public float moveSpeed = 5f;
    public float moveRange = 20f;
    public float attackRange = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        /** TODO: Set enemy speed, move & attack ranges according to type */
        /** TODO: Keep in mind evolution for ranged enemies */
        rigidbody = animator.GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bool playerDead = player.GetComponent<PlayerHealth>().isDead;
        if (Vector2.Distance(player.position, rigidbody.position) <= moveRange && !playerDead) {
            Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
            Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
            rigidbody.MovePosition(newPos);
            spriteRenderer.flipX = player.position.x > rigidbody.transform.position.x;
            if (Vector2.Distance(player.position, rigidbody.position) <= attackRange) animator.SetTrigger("atk");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("atk");
    }
}