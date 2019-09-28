using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSWeapons : MonoBehaviour {
    public List<GameObject> sWeapons;
    public GameObject sWeapon;
    public bool throwWeapon = false;
    public float throwTimer = 0;
    Image sWeaponsIcon;

    void Start() {
        sWeaponsIcon = GameObject.Find("SWeaponIcon").GetComponent<Image>();
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

            if (!throwWeapon) {
                throwTimer -= Time.fixedDeltaTime;
            }

            SwitchWeapon();
        }
        GameObject.Find("SWeaponUI").GetComponent<Canvas>().enabled = sWeapon;
        sWeaponsIcon.sprite = sWeapon ? sWeapon.GetComponent<SpriteRenderer>().sprite : null;
    }

    // Lazy way of handling input > update / physics > fixedupdate
    void FixedUpdate() {
        if (throwWeapon) {
            ThrowWeapon();
        }
    }

    void SwitchWeapon() {
        if (sWeapon) {
            int index = sWeapons.FindIndex(s => s == sWeapon);
            if (Input.GetKeyDown("right")) {
                sWeapon = (index + 1) > sWeapons.Count - 1 ? sWeapons[0] : sWeapons[index + 1];
            }
            if (Input.GetKeyDown("left")) {
                sWeapon = (index - 1) < 0 ? sWeapons[sWeapons.Count - 1] : sWeapons[index - 1];
            }
        }
    }

    void ThrowWeapon() {
        if (throwTimer <= 0) {
            bool playerFlip = GetComponent<SpriteRenderer>().flipX;
            Vector2 playerPos = GetComponent<Transform>().position;
            Vector2 playerVel = GetComponent<Rigidbody2D>().velocity;
            GameObject clone = Instantiate(sWeapon, playerPos, GetComponent<Transform>().rotation);
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