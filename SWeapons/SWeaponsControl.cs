using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWeaponsControl : MonoBehaviour {
    public int throwForceX = 0;
    public int throwForceY = 0;
    public int weaponDamage = 0;
    public AudioClip sWeaponSound = null;
    public float throwTimerCd = 0;

    private void Awake() {
        CheckWeaponType();
    }

    void CheckWeaponType() {
        // Prolly need a better to handle names (string manip at clone creation in PlayerSWeapons?)
        switch (name) {
            case "Axe(Clone)":
                throwForceX = 8;
                throwForceY = 20;
                weaponDamage = 5;
                // How to get soundclip?
                // sWeaponSound = null;
                throwTimerCd = 0.3f;
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