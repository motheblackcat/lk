using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSWeapons : MonoBehaviour {
    public GameObject sWeapon;
    public bool throwWeapon = false;
    public float throwTimer = 0;

    void Update() {
        if (sWeapon) {
            if (Input.GetButtonDown("SWeapon")) {
                if (throwTimer <= 0) {
                    throwWeapon = true;
                }
            }

            if (!throwWeapon) {
                throwTimer -= Time.fixedDeltaTime;
            }
        }
    }

    // Lazy way of handling input > update / physics > fixedupdate
    void FixedUpdate() {
        if (throwWeapon) {
            ThrowWeapon();
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
            GetComponent<AudioSource>().PlayOneShot(sWeaponControl.sWeaponSound);
            throwTimer = sWeaponControl.throwTimerCd;
        } else {
            throwWeapon = false;
        }
    }
}