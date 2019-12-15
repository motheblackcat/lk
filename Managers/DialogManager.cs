using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject npc;
    Canvas dialogUI;
    PlayerControl playerControl;
    IntroSceneManager introSceneManager;
    GlobalManager globalManager;
    public bool inDialog = false;

    void Start() {
        dialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        introSceneManager = GameObject.Find("GameManager").GetComponent<IntroSceneManager>();
        globalManager = GameObject.Find("GameManager").GetComponent<GlobalManager>();
    }

    void Update() {
        npc = playerControl.npc;
        inDialog = dialogUI.enabled;
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool isGamepad = globalManager.isGamepad;

        if (npc && introDone) {
            OpenCloseDialog();
            SpriteRenderer[] buttons = npc.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer button in buttons)
                if (button.name != npc.name)button.enabled = button.name == (globalManager.isGamepad ? "ButtonA" : "SpaceBar");
        }

        if (!npc || inDialog) {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("NPCButton");
            foreach (GameObject button in buttons)button.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OpenCloseDialog() {
        bool autoStartDialog = npc ? npc.GetComponent<NpcAnimation>().autoStart : false;
        if ((autoStartDialog || Input.GetButtonDown("Jump")) && playerControl.isGrounded) {
            if (!inDialog) {
                getDialog();
                autoStartDialog = false;
            } else {
                dialogUI.enabled = false;
            }
        }
    }

    void getDialog() {
        string path = "Text/" + npc.name + "Dialog";
        TextAsset text = Resources.Load<TextAsset>(path);
        GameObject.Find("DialogText").GetComponent<Text>().text = text.text;
        dialogUI.enabled = true;
    }
}