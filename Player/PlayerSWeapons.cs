using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSWeapons : MonoBehaviour {
    public GameObject sWeapon;
    public float startPos = 1;
    public float throwForce = 20;

    /*
     * TODO:
     * - Add sound
     * - Make a manageable list of sweapons
     * - Adaptative throwing pattern
     * - Repsect player orientation
     * - UI for sweapon
     * - Selectable sweapon from keys / menu
     */
    void Update() {
        Vector2 playerPos = GetComponent<Transform>().position;
        if (Input.GetButtonDown("SWeapon")) {
            GameObject clone = Instantiate(sWeapon, new Vector2(playerPos.x + startPos, playerPos.y), GetComponent<Transform>().rotation);
            clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwForce, throwForce), ForceMode2D.Impulse);
        };
    }
}