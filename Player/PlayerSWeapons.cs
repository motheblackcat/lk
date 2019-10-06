using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSWeapons : MonoBehaviour {
    public List<GameObject> sWeapons;
    public GameObject sWeapon;
    public Image sWeaponsIcon;
    public bool throwWeapon = false;
    public float throwTimer = 0;

    void Start() {
        sWeaponsIcon = GameObject.Find("SWeaponIcon") ? GameObject.Find("SWeaponIcon").GetComponent<Image>() : null;
        // These Sweapons will be added from elswhere (npc event or shop)
        sWeapons.Add(Resources.Load("Sweapons/Axe")as GameObject);
        sWeapons.Add(Resources.Load("Sweapons/Dagger")as GameObject);
        sWeapon = sWeapons[0];
    }

    void Update() {
        if (sWeapon && GetComponent<PlayerControl>().canMove) {
            if (Input.GetButtonDown("SWeapon")) {
                if (throwTimer <= 0) {
                    throwWeapon = true;
                }
            }

            SwitchWeapon();
        }
        if (GameObject.Find("SWeaponUI")) {
            GameObject.Find("SWeaponUI").GetComponent<Canvas>().enabled = sWeapon;
            sWeaponsIcon.sprite = sWeapon ? sWeapon.GetComponent<SpriteRenderer>().sprite : null;
        }
    }

    // Lazy way of handling input > update / physics > fixedupdate
    void FixedUpdate() {
        if (throwWeapon) {
            ThrowWeapon();
        } else {
            throwTimer -= Time.fixedDeltaTime;
        }
    }

    // Should this be in PlayerControl instead?
    void SwitchWeapon() {
        if (sWeapon) {
            int index = sWeapons.FindIndex(s => s == sWeapon);
            if (Input.GetButtonDown("Right SW")) {
                sWeapon = (index + 1) > sWeapons.Count - 1 ? sWeapons[0] : sWeapons[index + 1];
            }
            if (Input.GetButtonDown("Left SW")) {
                sWeapon = (index - 1) < 0 ? sWeapons[sWeapons.Count - 1] : sWeapons[index - 1];
            }
        }
    }

    void ThrowWeapon() {
        if (throwTimer <= 0) {
            // Get player's sprite flip and velocity
            bool playerFlip = GetComponent<SpriteRenderer>().flipX;
            Vector2 playerVel = GetComponent<Rigidbody2D>().velocity;
            // Instanciate clone
            GameObject clone = Instantiate(sWeapon, transform)as GameObject;
            // Get reference to the clone's SWeaponsControl script to set the object's values
            SWeaponsControl sWeaponControl = clone.GetComponent<SWeaponsControl>();
            clone.GetComponent<Rigidbody2D>().AddForce(
                new Vector2((playerFlip ? -sWeaponControl.throwForceX : sWeaponControl.throwForceX) + playerVel.x, sWeaponControl.throwForceY),
                ForceMode2D.Impulse
            );
            throwTimer = sWeaponControl.throwTimerCd;
        } else {
            throwWeapon = false;
        }
    }
}