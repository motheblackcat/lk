using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    GameObject sceneTransition;
    GameObject player;
    PlayerControl playerControl;
    NPCManager npcManager;
    public GameObject npc;
    public bool inDialog = false;
    public bool autoStartDialog = false;

    void Start() {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        sceneTransition = GameObject.Find("SceneTransition");
        npcManager = GameObject.FindWithTag("NPC") ? GameObject.FindWithTag("NPC").GetComponent<NPCManager>() : null;
    }

    void Update() {
        CheckNpc();
    }

    // TODO: Refactor this whole part with NPC Manager merge and auto start dialog that applies to all npcs
    void CheckNpc() {
        npc = playerControl.npc;
        bool introDone = sceneTransition.GetComponent<IntroSceneManager>() ? sceneTransition.GetComponent<IntroSceneManager>().introDone : true;
        inDialog = GetComponent<Image>().enabled;
        if (npcManager) {
            Image inDialogButton = npcManager.checkGamepad() ? GameObject.Find("DialogBox/ButtonA").GetComponent<Image>() : GameObject.Find("DialogBox/ButtonA/SpaceBar").GetComponent<Image>();
            inDialogButton.enabled = inDialog;
            Image joyButton = GameObject.Find("DialogBox/ButtonA").GetComponent<Image>();
            Image keyButton = GameObject.Find("DialogBox/ButtonA/SpaceBar").GetComponent<Image>();

            if (npcManager.checkGamepad()) {
                keyButton.enabled = false;
            } else {
                joyButton.enabled = false;
            }
        }

        if (npc && introDone) {
            if (autoStartDialog) {
                getDialog(npc);
                autoStartDialog = false;
            }
            if (Input.GetButtonDown("Jump")) {
                getDialog(npc);
            }
        }

        if (inDialog && Input.GetButtonDown("Jump")) {
            GetComponent<Image>().enabled = false;
            GameObject.Find("Text").GetComponent<Text>().text = "";
        }
    }

    // Get the text asset according to the npc name and open the dialog box
    void getDialog(GameObject npc) {
        string path = "Text/" + npc.name + "Dialog";
        TextAsset text = Resources.Load<TextAsset>(path);
        GetComponent<Image>().enabled = true;
        GetComponentInChildren<Text>().text = text.text;
    }
}