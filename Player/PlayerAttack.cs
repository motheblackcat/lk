using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
    public bool isAttacking;
    public float timeBtwAtk;
    public float timeBtwAtkTemp;
    float atkPosX;
    public Transform atkPos;
    public float atkRange;
    public LayerMask whatIsEnemies;
    public int damage;
    PlayerHealth playerHealth;

    public Collider2D[] ennemiesToDamage;

    void Start() {
        playerHealth = GetComponent<PlayerHealth>();
        atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
        atkPosX = atkPos.localPosition.x;
    }

    // TODO: Test atk detection with continus otherwise refactor with FixedUpdate()
    void Update() {
        Attack();
    }

    void Attack() {
        atkPos.localPosition = GetComponent<SpriteRenderer>().flipX ? new Vector2(-atkPosX, atkPos.localPosition.y) : new Vector2(atkPosX, atkPos.localPosition.y);

        if (timeBtwAtk <= 0) {
            if (Input.GetButtonDown("Attack") && GetComponent<PlayerControl>().canMove) {
                isAttacking = true;
                ennemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemies);
                for (int i = 0; i < ennemiesToDamage.Length; i++) {
                    ennemiesToDamage[i].GetComponent<EnemyHealthControl>().TakeDamage(damage);
                }
                timeBtwAtk = timeBtwAtkTemp;
            }
        } else {
            isAttacking = false;
            timeBtwAtk -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }
}