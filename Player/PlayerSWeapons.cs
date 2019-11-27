using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSWeapons : MonoBehaviour {
    public List<GameObject> sWeapons;
    int sWeaponsCount = 0;
    GameObject sWeapon;
    public bool throwWeapon = false;
    public float throwTimer = 0;

    void Start() {
        // TODO: Move the savestate logic in it's own script
        if (GameObject.Find("PlayerState")) {
            if (sWeapons.Count < GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().sWeapons.Count) {
                sWeapons = GameObject.Find("PlayerState").GetComponent<PlayerStateSave>().sWeapons;
            }
        }
        if (sWeapons.Count > 0)sWeapon = sWeapons[0];
    }

    void Update() {
        // TODO: Make a stable way to detect changes to the sWeapons list ingame
        if (sWeapons.Count != sWeaponsCount)sWeapon = sWeapons[0];

        GameObject.Find("SWeaponUI").GetComponent<Canvas>().enabled = sWeapon;

        if (sWeapon) {
            if (Input.GetButtonDown("SWeapon") && GetComponent<PlayerControl>().canMove && throwTimer <= 0) {
                throwWeapon = true;
            }

            GameObject.Find("SWeaponIcon").GetComponent<Image>().sprite = sWeapon.GetComponent<SpriteRenderer>().sprite;

            SwitchWeapon();
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

    void SwitchWeapon() {
        int index = sWeapons.FindIndex(s => s == sWeapon);
        if (Input.GetButtonDown("RB")) {
            sWeapon = (index + 1) > sWeapons.Count - 1 ? sWeapons[0] : sWeapons[index + 1];
        }
        if (Input.GetButtonDown("LB")) {
            sWeapon = (index - 1) < 0 ? sWeapons[sWeapons.Count - 1] : sWeapons[index - 1];
        }
    }

    void ThrowWeapon() {
        if (throwTimer <= 0) {
            bool playerFlip = GetComponent<SpriteRenderer>().flipX;
            Vector2 playerVel = GetComponent<Rigidbody2D>().velocity;
            GameObject clone = Instantiate(sWeapon, transform)as GameObject;
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