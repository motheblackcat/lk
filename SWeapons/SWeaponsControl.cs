using UnityEngine;

public class SWeaponsControl : MonoBehaviour {
    public int throwForceX = 0;
    public int throwForceY = 0;
    public int weaponDamage = 0;
    public float throwTimerCd = 0;

    private void Awake() {
        SetWeaponType();
    }

    void SetWeaponType() {
        // This method is called everytime an Sweapon is thrown, should only set those once when changing subweapon except for flip?
        name = name.Replace("(Clone)", "");
        bool playerFlip = GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX;
        switch (name) {
            case "Axe":
                GetComponent<Animator>().SetFloat("speed", playerFlip ? -1 : 1);
                throwForceX = 8;
                throwForceY = 20;
                weaponDamage = 5;
                throwTimerCd = 1f;
                break;

            case "Dagger":
                GetComponent<SpriteRenderer>().flipX = playerFlip;
                throwForceX = 20;
                throwForceY = 0;
                weaponDamage = 1;
                throwTimerCd = 0.5f;
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
            other.GetComponent<EnemyHealthControl>().TakeDamage(weaponDamage);
            Destroy(gameObject);
        }
    }
}