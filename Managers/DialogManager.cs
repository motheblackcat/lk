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
        bool autoStartDialog = npc ? npc.GetComponent<NpcAnimation>().autoStart : false;
        bool introDone = introSceneManager ? introSceneManager.introDone : true;
        bool isGamepad = globalManager.isGamepad;

        if (npc && introDone) {
            if (autoStartDialog || Input.GetButtonDown("Jump")) {
                if (playerControl.isGrounded)getDialog();
                autoStartDialog = false;
            } else {
                // TODO: Can be simplified?
                GameObject.Find(npc.name + "/ButtonA").GetComponent<SpriteRenderer>().enabled = !inDialog && isGamepad;
                GameObject.Find(npc.name + "/SpaceBar").GetComponent<SpriteRenderer>().enabled = !inDialog && !isGamepad;
            }
            if (inDialog && Input.GetButtonDown("Jump")) {
                GameObject.Find("DialogText").GetComponent<Text>().text = "";
                dialogUI.enabled = false;
            }
        } else {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("NPCButton");
            foreach (GameObject button in buttons)button.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void getDialog() {
        string path = "Text/" + npc.name + "Dialog";
        TextAsset text = Resources.Load<TextAsset>(path);
        GameObject.Find("DialogText").GetComponent<Text>().text = text.text;
        dialogUI.enabled = true;
    }
}