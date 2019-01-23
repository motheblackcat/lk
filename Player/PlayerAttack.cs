using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
	public bool isAttacking;
	float timeBtwAttack;
	public float cdBtwAttack;
	float atkPosX;
	public Transform atkPos;
	public float atkRange;
	public LayerMask whatIsEnemies;
	public int damage;
	PlayerHealth playerHealth;

	void Start() {
		playerHealth = GetComponent<PlayerHealth>();
		atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
		atkPosX = atkPos.localPosition.x;
	}

	// TODO: Make the way to lower enemy health generic (now it use the specific EnemyHealthControl script)
	void Update () {
		Attack();
	}

	void Attack() {
		atkPos.localPosition = GetComponent<SpriteRenderer>().flipX ? new Vector2(-atkPosX, atkPos.localPosition.y) : new Vector2(atkPosX, atkPos.localPosition.y);

		if (timeBtwAttack <= 0) {
			if (Input.GetButtonDown("Attack") && !playerHealth.tookDamage && !playerHealth.isDead && GetComponent<PlayerControl>().canMove) {
				isAttacking = true;
				Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemies);
				for (int i = 0; i < ennemiesToDamage.Length; i++) {
					ennemiesToDamage[i].GetComponent<EnemyHealthControl>().TakeDamage(damage);
				}
				timeBtwAttack = cdBtwAttack;
			}
		} else {
			isAttacking = false;
			timeBtwAttack -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(atkPos.position, atkRange);
	}
}
