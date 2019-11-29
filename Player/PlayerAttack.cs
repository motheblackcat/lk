using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public bool isAttacking = false;
    public float timeBtwAtk = 0.3f;
    float timeBtwAtkTemp;
    float atkPosX;
    public Transform atkPos;
    public float atkRange = 0.7f;
    public LayerMask whatIsEnemies;
    // TOFIX: damage should come from the player's weapon
    public int damage = 1;

    public Collider2D[] ennemiesToDamage;

    void Start() {
        atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
        atkPosX = atkPos.localPosition.x;
        timeBtwAtkTemp = timeBtwAtk;
    }

    // TODO: Atk detection seems better with continus but keep an eye on it
    void Update() {
        Attack();
    }

    void Attack() {
        atkPos.localPosition = new Vector2(GetComponent<SpriteRenderer>().flipX ? -atkPosX : atkPosX, atkPos.localPosition.y);

        if (timeBtwAtk <= 0) {
            if (Input.GetButtonDown("Attack") && GetComponent<PlayerControl>().canMove) {
                isAttacking = true;
                // TODO: Check that this Array is correctly filled
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