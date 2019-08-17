using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSWeapons : MonoBehaviour {
    public GameObject sWeapon;
    public float startPos = 1;
    public float throwForce = 20;
    public bool throwWeapon = false;

    /*
     * TODO:
     * - Add sound and timer according to weapon
     * - Make a manageable list of sweapons
     * - Adaptative throwing pattern
     * - Repsect player orientation
     * - UI for sweapon
     * - Selectable sweapon from keys / menu
     */
    void Update() {
        if (Input.GetButtonDown("SWeapon")) {
            throwWeapon = true;
        };
        Debug.Log(throwWeapon);
    }

    void FixedUpdate() {
        if (throwWeapon) {
            ThrowWeapon();
        }
    }

    void CheckWeaponType() {

    }

    void ThrowWeapon() {
        Vector2 playerPos = GetComponent<Transform>().position;
        GameObject clone = Instantiate(sWeapon, new Vector2(playerPos.x + startPos, playerPos.y), GetComponent<Transform>().rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwForce, throwForce), ForceMode2D.Impulse);
        throwWeapon = false;
    }
}