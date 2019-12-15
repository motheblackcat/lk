using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public Transform atkPos;
    public LayerMask whatIsEnemies;
    GameObject weapon;
    public bool isAttacking = false;
    public float timeBtwAtk = 0.3f;
    public float atkRange = 0.7f;
    // TODO: damage should come from the player's weapon (by type with tags or script)
    public int damage = 1;
    float timeBtwAtkTemp;
    float atkPosX;

    public Collider2D[] ennemiesToDamage;

    void Start() {
        atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
        atkPosX = atkPos.localPosition.x;
        timeBtwAtkTemp = timeBtwAtk;
    }

    void Update() {
        weapon = GameObject.FindWithTag("Weapon");
        if (weapon)Attack();
    }

    // TODO: Check animation as it doesn't seems synchronous
    void Attack() {
        atkPos.localPosition = new Vector2(GetComponent<SpriteRenderer>().flipX ? -atkPosX : atkPosX, atkPos.localPosition.y);

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