using UnityEngine;

public class EnemyMoveBehaviour : StateMachineBehaviour {
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float moveRange = 20f;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Transform player;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
}