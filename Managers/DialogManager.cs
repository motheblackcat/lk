using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject npc;
    public bool inDialog = false;
    public bool autoStartDialog = false;
	
    // Check if player is in contact with an NPC or if the Dialog box is opened
    // Refactor for autostart dialog
	void Update() {
        npc = GameObject.Find("Player").GetComponent<PlayerControl>().npc;
        inDialog = GetComponent<Image>().enabled;

        readDialog(npc);
	}

    // Get the text asset according to the npc name and open / close the dialog box
    void readDialog(GameObject npc) {
        if (Input.GetAxis("Vertical") > 0 && Camera.main.GetComponent<IntroSceneManager>().introDone && npc) {
            string path = "Assets/Text/" + npc.name + "Dialog.txt";
            StreamReader sr = new StreamReader(path);
            GetComponent<Image>().enabled = true;
            GameObject.Find("Text").GetComponent<Text>().text = sr.ReadToEnd();
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = true;
            sr.Close();
        }

        if (inDialog && Input.GetButtonDown("Jump")) {
            GetComponent<Image>().enabled = false;
            GameObject.Find("Text").GetComponent<Text>().text = "";
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = false;
        }
    }
}
