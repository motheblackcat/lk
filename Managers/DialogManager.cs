using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject npc;
    Canvas dialogUI;
    PlayerControl playerControl;
    public bool inDialog = false;

    void Start() {
        dialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    void Update() {
        npc = playerControl.npc;
        inDialog = dialogUI.enabled;
        bool isGamepad = PlayerState.Instance.isGamepad;

        if (npc && playerControl.isGrounded) {
            OpenCloseDialog();
        }

        if (!npc || inDialog) {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("NPCButton");
            foreach (GameObject button in buttons) button.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (!npc) dialogUI.enabled = false;
    }

    void OpenCloseDialog() {
        bool autoStartDialog = npc.GetComponent<NpcAnimation>().autoStart;
        if (!autoStartDialog) {
            SpriteRenderer[] buttons = npc.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons)
                if (button.name != npc.name && button.tag == "NPCButton") button.enabled = button.name == (PlayerState.Instance.isGamepad ? "ButtonA" : "SpaceBar");
        }

        if ((autoStartDialog || Input.GetButtonDown("Jump")) && playerControl.isGrounded) {
            if (!inDialog) {
                GetDialog();
                npc.GetComponent<NpcAnimation>().autoStart = false;
            } else {
                dialogUI.enabled = false;
            }
        }
    }

    // TODO: Add multi pages dialogs
    void GetDialog() {
        string path = "Text/" + npc.name + "Dialog";
        TextAsset text = Resources.Load<TextAsset>(path);
        GameObject.Find("DialogText").GetComponent<Text>().text = text.text;
        dialogUI.enabled = true;
    }
}