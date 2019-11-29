using UnityEngine;

public class NPCManager : MonoBehaviour {
    SpriteRenderer dialogArrow;
    Animator animator;
    SceneLoader sceneLoader;
    IntroSceneManager introSceneManager;

    void Start() {
        animator = GetComponent<Animator>();
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
        introSceneManager = GameObject.Find("SceneTransition").GetComponent<IntroSceneManager>();
    }

    void Update() {
        dialogArrow = GameObject.Find(this.name + "/ArrowUp") ? GameObject.Find(this.name + "/ArrowUp").GetComponent<SpriteRenderer>() : null;
    }

    void OnTriggerStay2D(Collider2D other) {
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool inDialog = GameObject.FindWithTag("DialogBox").GetComponent<DialogManager>().inDialog;
        if (other.gameObject.tag == "Player" && introDone) {
            if (dialogArrow)dialogArrow.enabled = !inDialog;
            if (animator) {
                animator.SetBool("watch", true);
                animator.SetBool("talk", inDialog);
            }
            if (tag == "NPC") {
                GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
            }
            // TOFIX: This is too specific to the door of the intro
            if (tag == "Door" && Input.GetAxis("Vertical") > 0) {
                sceneLoader.sceneIndex = 2;
                sceneLoader.loadScene = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (dialogArrow)dialogArrow.enabled = false;
            if (animator) {
                animator.SetBool("watch", false);
                animator.SetBool("talk", false);
            }
        }
    }
}