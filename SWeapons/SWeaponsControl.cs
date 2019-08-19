using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWeaponsControl : MonoBehaviour {
    public AudioClip sWeaponSound = null;
    public int throwForceX = 0;
    public int throwForceY = 0;
    public int weaponDamage = 0;
    public float throwTimerCd = 0;
    AudioClip[] sounds = sWeaponSound[];

    private void Awake() {
        CheckWeaponType();
        sounds = GameObject.Find('sWeaponsSounds').GetComponent<Sounds>().sounds;
    }

    void CheckWeaponType() {
        // Prolly need a better to handle names (string manip at clone creation in PlayerSWeapons?)
        switch (name) {
            case "Axe(Clone)":
                throwForceX = 8;
                throwForceY = 20;
                weaponDamage = 5;
                // How to get soundclip (ressources.load or array of sounds)?
                // sWeaponSound = null;
                // sWeaponSound = GetSound();
                throwTimerCd = 0.5f;
                break;
            default:
                break;
        }
    }
    
    AudioClip GetSound() {
        // Check string operation in C#
        string sWeaponName = name.toLowerCase();
        sounds.find(sWeaponName + "ThrowSound");
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
