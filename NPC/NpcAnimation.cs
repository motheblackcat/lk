using UnityEngine;

public class NpcAnimation : MonoBehaviour {
    Animator animator;
    PlayerState playerState;
    DialogManager dialogManager;
    public bool autoStart = false;
    bool introDone;

    void Start() {
        animator = GetComponent<Animator>();
        playerState = PlayerState.Instance;
        dialogManager = GameObject.Find("PlayerUI").GetComponent<DialogManager>();

        introDone = playerState.introDone;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && introDone) {
            if (animator) {
                animator.SetBool("watch", true);
                animator.SetBool("talk", dialogManager.inDialog);
            }
            if (tag == "NPC") {
                GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (animator) {
                animator.SetBool("watch", false);
                animator.SetBool("talk", false);
            }
        }
    }
}