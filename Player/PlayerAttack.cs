using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] Collider2D[] enemyHits;
	[SerializeField] LayerMask enemyLayer;
	[SerializeField] bool atkStart = false;
	[SerializeField] float atkCd = 0.3f;
	[SerializeField] float atkRange = 0.7f;
	[SerializeField] int damage = 1;

	PlayerInputActions playerInputs;
	Transform atkPos;
	Animator animator;
	AudioSource audioSource;
	GameObject weapon;
	PlayerSound playerSound;

	float atkCdTimer = 0;
	float weaponPosX = 0;
	float atkPosX = 0;

	void Start() {
		weapon = GameObject.FindWithTag("Weapon");
		atkPos = GameObject.Find("AttackPos").GetComponent<Transform>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		playerSound = GetComponent<PlayerSound>();
		atkCdTimer = atkCd;
		weaponPosX = weapon.transform.localPosition.x;
		atkPosX = atkPos.localPosition.x;
	}

	void Update() {
		if (weapon) {
			WeaponPosition();
			Attack();
		}
	}

	/**
	 *  Play attack animation and sound then reset timer and trigger
	 **/
	void Attack() {
		if (atkStart && atkCdTimer <= 0 && GetComponent<PlayerControl>().canMove) {
			animator.SetTrigger("attack");
			audioSource.PlayOneShot(playerSound.attackSound);
			atkCdTimer = atkCd;
			atkStart = false;
		} else atkCdTimer -= Time.deltaTime;
	}

	/**
	 *  Damage the enemy
	 **/
	public void DamageEnemy() {
		enemyHits = Physics2D.OverlapCircleAll(atkPos.position, atkRange, enemyLayer);
		foreach (Collider2D enemy in enemyHits) enemy.GetComponent<EnemyControl>().TakeDamage(damage);
	}

	/**
	 *  Position the weapon depending if the player is facing left or right
	 **/
	void WeaponPosition() {
		bool playerFlipX = GetComponent<SpriteRenderer>().flipX;
		weapon.GetComponent<SpriteRenderer>().flipX = playerFlipX;
		float weaponPosY = weapon.transform.localPosition.y;
		weapon.transform.localPosition = new Vector2(playerFlipX ? -weaponPosX : weaponPosX, weaponPosY);
		atkPos.localPosition = new Vector2(GetComponent<SpriteRenderer>().flipX ? -atkPosX : atkPosX, atkPos.localPosition.y);
	}

	/**
	 *  From MonoBehaviour: draw a gizmo when the player is selected in the editor, the gizmo represents the weapon's radius
	 **/
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(GameObject.Find("AttackPos").GetComponent<Transform>().position, atkRange);
	}

	/**
	 *   From MonoBehaviour: this is called when the attached GameObject is enabled
	 **/
	void OnEnable() {
		playerInputs = new PlayerInputActions();
		playerInputs.Player.Attack.performed += ctx => atkStart = true;
		playerInputs.Enable();
	}

	/**
	 *   From MonoBehaviour: this is called when the attached GameObject is disabled
	 **/
	void OnDisable() {
		playerInputs.Disable();
	}
}