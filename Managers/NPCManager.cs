using UnityEngine;

public class NPCManager : MonoBehaviour {
    SpriteRenderer dialogButton;
    Animator animator;
    SceneLoader sceneLoader;
    IntroSceneManager introSceneManager;
    GameObject dialogBox;
    bool introDone;
    void Start() {
        animator = GetComponent<Animator>();
        sceneLoader = GameObject.Find("SceneTransition").GetComponent<SceneLoader>();
        introSceneManager = GameObject.Find("SceneTransition").GetComponent<IntroSceneManager>();
        dialogBox = GameObject.Find("DialogBox");
    }

    void Update() {
        introDone = introSceneManager ? introSceneManager.introDone : true;
        // setDialogButton();
    }

    // void setDialogButton() {
    //     SpriteRenderer joyButton = GameObject.Find(this.name + "/ButtonA").GetComponent<SpriteRenderer>();
    //     SpriteRenderer keyButton = GameObject.Find(this.name + "/ButtonA/SpaceBar").GetComponent<SpriteRenderer>();
    //     dialogButton = checkGamepad() ? joyButton : keyButton;
    //     if (!introDone) {
    //         joyButton.enabled = false;
    //         keyButton.enabled = false;
    //     } else if (checkGamepad()) {
    //         keyButton.enabled = false;
    //     } else {
    //         joyButton.enabled = false;
    //     }
    // }

    void OnTriggerStay2D(Collider2D other) {
        bool inDialog = dialogBox.GetComponent<DialogManager>().inDialog;
        if (other.gameObject.tag == "Player" && introDone) {
            if (dialogButton)dialogButton.enabled = !inDialog;
            if (animator) {
                animator.SetBool("watch", true);
                animator.SetBool("talk", inDialog);
            }
            if (tag == "NPC") {
                GetComponent<SpriteRenderer>().flipX = other.gameObject.transform.position.x > transform.position.x;
            }
            // TOFIX: This is too specific to the door of the intro
            if (tag == "Door" && Input.GetButtonDown("Jump")) {
                sceneLoader.sceneIndex = 2;
                sceneLoader.loadScene = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (dialogButton)dialogButton.enabled = false;
            if (animator) {
                animator.SetBool("watch", false);
                animator.SetBool("talk", false);
            }
        }
    }
}