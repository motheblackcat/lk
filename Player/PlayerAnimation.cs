using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	Animator animator;
	PlayerControl playerControl;
	PlayerHealth playerHealth;
	PlayerAttack playerAttack;
	PlayerSWeapons playerSWeapons;
	GameObject weapon;

	void Start() {
		animator = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		playerHealth = GetComponent<PlayerHealth>();
		playerAttack = GetComponent<PlayerAttack>();
		playerSWeapons = GetComponent<PlayerSWeapons>();
		weapon = GameObject.FindGameObjectsWithTag("Weapon").Length > 0 ? GameObject.FindGameObjectsWithTag("Weapon")[0] : null;
	}

	void Update() {
		PlayerAnimate();
		if (weapon)SwordPosition();
	}

	void PlayerAnimate() {
		animator.SetBool("run", GetComponent<Rigidbody2D>().velocity.x != 0 && playerControl.canMove);
		animator.SetBool("air", !playerControl.isGrounded);
		animator.SetBool("throw", playerSWeapons ? playerSWeapons.throwWeapon : false);
		if (playerAttack)animator.SetBool("attack", playerAttack.isAttacking);
		if (playerHealth)animator.SetBool("hurt", playerHealth.isInv);
		if (playerHealth && playerHealth.isDead)animator.SetTrigger("die");
	}

	void SwordPosition() {
		bool flipX = GetComponent<SpriteRenderer>().flipX;
		weapon.GetComponent<SpriteRenderer>().flipX = flipX;
		float y = weapon.transform.localPosition.y;
		weapon.transform.localPosition = flipX ? new Vector2(-0.74f, y) : new Vector2(0.74f, y);
	}
}