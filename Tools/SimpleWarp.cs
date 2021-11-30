using UnityEngine;

public class SimpleWarp : MonoBehaviour {
    [SerializeField] int nextSceneIndex = -1;
    SceneTransition SceneTransition;
    PlayerInputActions playerInputs;
    bool warp = false;

    /* TODO: Make playerInputs global? */
    void Awake() {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Jump.performed += ctx => Warp();
    }

    void Start() {
        SceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    void Warp() {
        if (warp && GameObject.Find("Player").GetComponent<PlayerControl>().isGrounded) StartCoroutine(SceneTransition.LoadScene(nextSceneIndex));
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