using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public Transform atkPos;
    public LayerMask enemyLayer;
    GameObject weapon;
    public bool isAttacking = false;
    public float timeBtwAtk = 0.3f;
    public float atkRange = 0.7f;
    // TODO: damage should come from the player's weapon (by type with tags or script)
    public int damage = 1;
    float timeBtwAtkTemp;
    float atkPosX;

    public Collider2D[] enemyHits;

    void Start() {
        weapon = GameObject.FindWithTag("Weapon");
        atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
        atkPosX = atkPos.localPosition.x;
        timeBtwAtkTemp = timeBtwAtk;
    }

    // TODO: Check animation as it doesn't seems synchronous and refactor this (button detection in Update)
    void Update() {
        if (weapon)Attack();
    }

    void Attack() {
        atkPos.localPosition = new Vector2(GetComponent<SpriteRenderer>().flipX ? -atkPosX : atkPosX, atkPos.localPosition.y);
        if (timeBtwAtk <= 0 && Input.GetButtonDown("Attack") && GetComponent<PlayerControl>().canMove) {
            isAttacking = true;
            enemyHits = Physics2D.OverlapCircleAll(atkPos.position, atkRange, enemyLayer);
            foreach (Collider2D enemy in enemyHits)StartCoroutine(enemy.GetComponent<EnemyHealthControl>().TakeDamage(damage));
            timeBtwAtk = timeBtwAtkTemp;
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