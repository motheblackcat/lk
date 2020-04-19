using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSWeapons : MonoBehaviour {
    public List<GameObject> sWeapons;
    public GameObject sWeapon;
    Animator animator;
    PlayerState playerState;
    PlayerInputActions playerInputs;
    public bool throwWeapon = false;
    public float throwTimer = 0;

    void Awake() {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Throw.performed += ctx => TriggerThrow();
        playerInputs.Player.Switch.performed += ctx => SwitchWeapon(ctx.ReadValue<float>());
    }

    void Start() {
        animator = GetComponent<Animator>();
        playerState = GameObject.Find("PlayerState") ? GameObject.Find("PlayerState").GetComponent<PlayerState>() : null;
        sWeapons = PlayerState.Instance.sWeapons;
        sWeapon = PlayerState.Instance.sWeapon;
    }

    void Update() {
        if (sWeapons.Count == 1) sWeapon = sWeapons[0];

        if (sWeapon) {
            animator.SetBool("throw", throwWeapon);
            GameObject.Find("SWeaponUI").GetComponent<Canvas>().enabled = true;

            Canvas[] buttons = GameObject.Find("SWeaponUI").GetComponentsInChildren<Canvas>();
            foreach (Canvas button in buttons)
                if (button.name != "SWeaponUI") button.GetComponent<Canvas>().enabled = button.name == (playerState.isGamepad ? "Buttons" : "Keys");

            GameObject.Find("SWeaponIcon").GetComponent<Image>().sprite = sWeapon.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void FixedUpdate() {
        if (throwWeapon) ThrowWeapon();
        else throwTimer -= Time.fixedDeltaTime;
    }

    void TriggerThrow() {
        if (GetComponent<PlayerControl>().canMove && throwTimer <= 0 && sWeapon) throwWeapon = true;
    }

    void ThrowWeapon() {
        if (throwTimer <= 0) {
            bool playerFlip = GetComponent<SpriteRenderer>().flipX;
            Vector2 playerVel = GetComponent<Rigidbody2D>().velocity;
            GameObject clone = Instantiate(sWeapon, transform) as GameObject;
            SWeaponsControl sWeaponControl = clone.GetComponent<SWeaponsControl>();
            clone.GetComponent<Rigidbody2D>().AddForce(
                new Vector2((playerFlip ? -sWeaponControl.throwForceX : sWeaponControl.throwForceX) + playerVel.x, sWeaponControl.throwForceY),
                ForceMode2D.Impulse
            );
            throwTimer = sWeaponControl.throwTimerCd;
        } else throwWeapon = false;
    }

    void SwitchWeapon(float direction) {
        if (sWeapons.Count > 1) {
            int index = sWeapons.FindIndex(s => s == sWeapon);
            int lastIndex = sWeapons.Count - 1;
            if (direction == 1) sWeapon = (index + 1) > lastIndex ? sWeapons[0] : sWeapons[index + 1];
            if (direction == -1) sWeapon = (index - 1) < 0 ? sWeapons[lastIndex] : sWeapons[index - 1];
        }
    }

    void OnEnable() {
        playerInputs.Enable();
    }

    void OnDisable() {
        playerInputs.Disable();
    }
}