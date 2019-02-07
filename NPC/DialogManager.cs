using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    GameObject player;
    float startTimer;
    public bool eventDone = false;
    public bool inDialog = false;
	
    // Refactor this script (make dialog box activation generic)
	void Start() {
        player = GameObject.Find("Player");
        if (GameObject.Find("MainCamera").GetComponent<IntroSceneManager>()) {
            startTimer = GameObject.Find("MainCamera").GetComponent<IntroSceneManager>().startTimer;
        }
	}
	
	void Update() {
        checkNpc(player.GetComponent<PlayerControl>().npc);
        inDialog = GetComponent<Image>().enabled;
	}

    void readDialog(GameObject npc) {
        string path = "Assets/Text/" + npc.name + "Dialog.txt";
        StreamReader sr = new StreamReader(path);
        GetComponent<Image>().enabled = true;
        GameObject.Find("Text").GetComponent<Text>().text = sr.ReadToEnd();
        GameObject.Find("ButtonA").GetComponent<Image>().enabled = true;
        sr.Close();
    }

    void checkNpc(GameObject npc) {
        if (startTimer < Time.time && npc) {
            if (npc.name != "Bob") {
                if (Input.GetAxis("Vertical") > 0) {
                    readDialog(npc);
                }
            } else if (!eventDone) {
                readDialog(npc);
            }
        }

        if (inDialog && Input.GetButtonDown("Jump")) {
            GetComponent<Image>().enabled = false;
            GameObject.Find("Text").GetComponent<Text>().text = "";
            GameObject.Find("ButtonA").GetComponent<Image>().enabled = false;
            eventDone = true;
        }
    }
}
