using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject npc;
    GameObject player;
    Canvas dialogUI;
    PlayerControl playerControl;
    IntroSceneManager introSceneManager;
    public bool inDialog = false;

    void Start() {
        player = GameObject.Find("Player");
        dialogUI = GameObject.Find("DialogUI").GetComponent<Canvas>();
        playerControl = player.GetComponent<PlayerControl>();
        introSceneManager = GameObject.Find("GameManager").GetComponent<IntroSceneManager>();
    }

    void Update() {
        npc = playerControl.npc;
        inDialog = dialogUI.enabled;
        bool autoStartDialog = npc ? npc.GetComponent<NpcAnimation>().autoStart : false;
        bool introDone = introSceneManager ? introSceneManager.introDone : true;

        if (npc && introDone) {
            if (autoStartDialog) {
                getDialog(npc);
                autoStartDialog = false;
            }
            if (Input.GetButtonDown("Jump")) {
                getDialog(npc);
            }

            if (inDialog && Input.GetButtonDown("Jump")) {
                GameObject.Find("DialogText").GetComponent<Text>().text = "";
                dialogUI.enabled = false;
            }
        }
    }

    void getDialog(GameObject npc) {
        string path = "Text/" + npc.name + "Dialog";
        TextAsset text = Resources.Load<TextAsset>(path);
        GameObject.Find("DialogText").GetComponent<Text>().text = text.text;
        dialogUI.enabled = true;
    }
}