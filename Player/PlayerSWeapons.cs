using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSWeapons : MonoBehaviour {
    public List<GameObject> sWeapons;
    public GameObject sWeapon;
    GlobalManager globalManager;
    public bool throwWeapon = false;
    public float throwTimer = 0;
    int sWeaponsCount = 0;

    void Start() {
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
        sWeapons = PlayerState.Instance.sWeapons;
        // TODO: SWeapon choice is reset between scenes
        if (sWeapons.Count > 0)sWeapon = sWeapons[0];
        sWeaponsCount = sWeapons.Count;
    }

    void Update() {
        // TODO: Make a stable way to detect changes to the sWeapons list at runtime
        if (sWeapons.Count > sWeaponsCount && sWeapons.Count < 2)sWeapon = sWeapons[0];

        GameObject.Find("SWeaponUI").GetComponent<Canvas>().enabled = sWeapon;

        if (sWeapon) {
            if (Input.GetButtonDown("SWeapon") && GetComponent<PlayerControl>().canMove && throwTimer <= 0) {
                throwWeapon = true;
            }
            // TODO: Provide graphics for SWeapon UI buttons
            Image[] buttons = GameObject.Find("SWeaponUI").GetComponentsInChildren<Image>();
            foreach (Image button in buttons)
                if (button.name != "SWeaponIcon")button.enabled = button.name == (globalManager.isGamepad ? "Buttons" : "Keys");

            GameObject.Find("SWeaponIcon").GetComponent<Image>().sprite = sWeapon.GetComponent<SpriteRenderer>().sprite;
            SwitchWeapon();
        }
    }

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