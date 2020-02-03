using UnityEngine;

public class SWeaponsControl : MonoBehaviour {
    public int throwForceX = 0;
    public int throwForceY = 0;
    public int weaponDamage = 0;
    public float throwTimerCd = 0;
    public float lifeSpan = 0;
    public string animType;

    private void Awake() {
        SetWeaponAnimType();
    }

    private void FixedUpdate() {
        lifeSpan -= Time.fixedDeltaTime;
        if (lifeSpan <= 0) Destroy(gameObject);
    }

    void SetWeaponAnimType() {
        bool playerFlip = GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
        switch (animType) {
            case "speed":
                GetComponent<Animator>().SetFloat("speed", playerFlip ? -1 : 1);
                break;

            case "flip":
                GetComponent<SpriteRenderer>().flipX = playerFlip;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy") {
            StartCoroutine(other.GetComponent<EnemyHealthControl>().TakeDamage(weaponDamage));
            Destroy(gameObject);
        }
    }
}