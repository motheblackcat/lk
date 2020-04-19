using UnityEngine;

public class SimpleWarp : MonoBehaviour {
    SceneTransition SceneTransition;
    PlayerInputActions playerInputs;
    bool warp = false;

    void Awake() {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Jump.performed += ctx => Warp();
    }

    void Start() {
        SceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    void Warp() {
        if (warp) StartCoroutine(SceneTransition.LoadScene(false));
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons) button.enabled = button.name == (PlayerState.Instance.isGamepad ? "ButtonA" : "SpaceBar");
            warp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            SpriteRenderer[] buttons = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons) button.enabled = false;
            warp = false;
        }
    }

    void OnEnable() {
        playerInputs.Enable();
    }

    void OnDisable() {
        playerInputs.Disable();
    }
}