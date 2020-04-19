using UnityEngine;

public enum WeaponTypes { Sword }

public class PlayerAttack : MonoBehaviour {
    public Collider2D[] enemyHits;
    public Transform atkPos;
    public LayerMask enemyLayer;
    public WeaponTypes weaponType;
    Animator animator;
    AudioSource audioSource;
    GameObject weapon;
    PlayerSound playerSound;
    PlayerInputActions playerInputs;
    public float timeBtwAtk = 0.3f;
    public float atkRange = 0.7f;
    public int damage = 0;
    float timeBtwAtkTemp = 0;
    float atkPosX = 0;
    float weaponPosX = 0;
    bool attack = false;

    void Awake() {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Attack.performed += ctx => attack = true;
        playerInputs.Player.Attack.canceled += ctx => attack = false;
    }

    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerSound = GetComponent<PlayerSound>();
        weapon = GameObject.FindWithTag("Weapon");
        weaponPosX = weapon.transform.localPosition.x;
        atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
        atkPosX = atkPos.localPosition.x;
        timeBtwAtkTemp = timeBtwAtk;
        SetWeaponDamage(weaponType);
    }

    void Update() {
        WeaponPosition();
        atkPos.localPosition = new Vector2(GetComponent<SpriteRenderer>().flipX ? -atkPosX : atkPosX, atkPos.localPosition.y);
        if (attack && weapon && timeBtwAtk <= 0 && GetComponent<PlayerControl>().canMove) {
            animator.SetTrigger("attack");
            audioSource.PlayOneShot(playerSound.attackSound);
            enemyHits = Physics2D.OverlapCircleAll(atkPos.position, atkRange, enemyLayer);
            foreach (Collider2D enemy in enemyHits) enemy.GetComponent<EnemyControl>().TakeDamage(damage);
            timeBtwAtk = timeBtwAtkTemp;
            attack = false;
        } else timeBtwAtk -= Time.deltaTime;
    }

    void WeaponPosition() {
        bool flipX = GetComponent<SpriteRenderer>().flipX;
        float weaponPosY = weapon.transform.localPosition.y;
        weapon.GetComponent<SpriteRenderer>().flipX = flipX;
        weapon.transform.localPosition = new Vector2(flipX ? -weaponPosX : weaponPosX, weaponPosY);
    }

    void SetWeaponDamage(WeaponTypes weaponType) {
        switch (weaponType) {
            case WeaponTypes.Sword:
                damage = 1;
                break;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }

    void OnEnable() {
        playerInputs.Enable();
    }

    void OnDisable() {
        playerInputs.Disable();
    }
}