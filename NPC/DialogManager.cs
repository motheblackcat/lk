using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	Text dialogText;
    public TextAsset aDialogs;
    public TextAsset dDialogs;
    public GameObject npc;
	
	void Start () {
		dialogText = GameObject.Find("Text").GetComponent<Text>();
	}
	
	void Update () {
        checkDialogBox(npc);
	}

    void textLineReader() {
        var lines = dialogText.text.Split("\n"[0]);
        Debug.Log(lines);
        foreach(var line in lines) {
            Debug.Log(line);
        }
    }

    // Make it so any NPC can activate the box with the correct dialog
    // Need to get NPC
    // Need to get Dialog
    void checkDialogBox(GameObject npc) {
        if (npc) {
            if (npc.GetComponent<Animator>().GetBool("talk")) {
                GameObject.Find("DialogBox").GetComponent<Image>().enabled = true;
                GameObject.Find("Text").GetComponent<Text>().enabled = true;
                // GameObject.Find("ButtonA").GetComponent<Image>().enabled = true;
                npc.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;

            } else {
                GameObject.Find("DialogBox").GetComponent<Image>().enabled = false;            
                GameObject.Find("Text").GetComponent<Text>().enabled = false;
                // qGameObject.Find("ButtonA").GetComponent<Image>().enabled = false;
            }

            if (npc.name == "Bartender") {
                dialogText.text = aDialogs.text;
            }

            if (npc.name == "Drinker1") {
                dialogText.text = dDialogs.text;
            }
        }
    }
}
