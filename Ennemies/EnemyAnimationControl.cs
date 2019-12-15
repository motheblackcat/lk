using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour {
	Animator animator;
	EnemyHealthControl enemyHealthControl;

	void Start() {
		animator = GetComponent<Animator>();
		enemyHealthControl = GetComponent<EnemyHealthControl>();
	}

	void Update() {
		animator.SetBool("hurt", enemyHealthControl.isStunned);
		if (enemyHealthControl.isDead)animator.SetTrigger("die");
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player")
			if (!enemyHealthControl.isStunned)animator.Play("Atk");
	}
}