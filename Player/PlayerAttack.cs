using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public bool isAttacking;
	float timeBtwAttack;
	public float cdBtwAttack;
	float atkPosX;
	public Transform atkPos;
	public float atkRange;
	public LayerMask whatIsEnemies;
	public int damage;

	void Start() {
		atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
		atkPosX = atkPos.localPosition.x;
	}

	// TODO: Make the way to lower enemy health generic (now it use a specific enemy script)
	void Update () {
		Attack();
	}

	void Attack() {
		atkPos.localPosition = GetComponent<SpriteRenderer>().flipX ? new Vector2(-atkPosX, atkPos.localPosition.y) : new Vector2(atkPosX, atkPos.localPosition.y);

		if (timeBtwAttack <= 0) {
			if (Input.GetButtonDown("Attack") && !GetComponent<PlayerHealth>().tookDamage && !GetComponent<PlayerHealth>().isDead) {
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
