using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    GameObject player;
    PlayerControl playerControl;
    PlayerSound playerSound;
    public GameObject npc;
    public bool inDialog = false;
    public bool autoStartDialog = false;

    void Start() {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        playerSound = player.GetComponent<PlayerSound>();
    }

    // Check if player is in contact with an NPC or if the Dialog box is opened
    void Update() {
        npc = playerControl.npc;
        inDialog = GetComponent<Image>().enabled;
        CheckNpc(npc);
        playerSound.enabled = !inDialog;
    }

    // Check for an npc to get the dialogs and close the dialog box
    // Auto start dialog apply TO ALL NPC
    void CheckNpc(GameObject npc) {
        bool introDone = Camera.main.GetComponent<IntroSceneManager>() ? Camera.main.GetComponent<IntroSceneManager>().introDone : true;

        if (npc && introDone) {
            if (autoStartDialog) {
                getDialog(npc);
                autoStartDialog = false;
            } else if (Input.GetAxis("Vertical") > 0) {
                getDialog(npc);
            }
        }

        if (inDialog && Input.GetButtonDown("Jump")) {
            GetComponent<Image>().enabled = false;
            GameObject.Find("Text").GetComponent<Text>().text = "";
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = false;
        }
    }

    // Get the text asset according to the npc name and open the dialog box
    void getDialog(GameObject npc) {
        string path = "Assets/Text/" + npc.name + "Dialog.txt";
        StreamReader sr = new StreamReader(path);
        GetComponent<Image>().enabled = true;
        GameObject.Find("Text").GetComponent<Text>().text = sr.ReadToEnd();
        GameObject.Find("ButtonA").GetComponent<Image>().enabled = true;
        sr.Close();
    }
}