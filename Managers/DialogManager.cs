using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    GameObject sceneTransition;
    GameObject player;
    PlayerControl playerControl;
    public GameObject npc;
    public bool inDialog = false;
    public bool autoStartDialog = false;

    void Start() {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        sceneTransition = GameObject.Find("SceneTransition");
    }

    void Update() {
        npc = playerControl.npc;
        inDialog = GetComponent<Image>().enabled;
        CheckNpc(npc);
    }

    // TODO: Auto start dialog apply TO ALL NPC IN THE SCENE
    void CheckNpc(GameObject npc) {
        bool introDone = sceneTransition.GetComponent<IntroSceneManager>() ? sceneTransition.GetComponent<IntroSceneManager>().introDone : true;

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
            GameObject.Find("DialogBox/ButtonA").GetComponent<Image>().enabled = false;
        }
    }

    // Get the text asset according to the npc name and open the dialog box
    void getDialog(GameObject npc) {
        string path = "Assets/Text/" + npc.name + "Dialog.txt";
        StreamReader sr = new StreamReader(path);
        GetComponent<Image>().enabled = true;
        GameObject.Find("Text").GetComponent<Text>().text = sr.ReadToEnd();
        GameObject.Find("DialogBox/ButtonA").GetComponent<Image>().enabled = true;
        sr.Close();
    }
}