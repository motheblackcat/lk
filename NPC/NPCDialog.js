import UnityEngine.UI;
var textBox : GameObject;
var theText : Text;
var textFile : TextAsset;
var textLines : String[];
var currentLine : int;
var endAtLine : int;
var isActive : boolean;
var isTyping : boolean;
var cancelTyping : boolean;
var typeSpeed : float;

function Start () {
    if(textFile != null) {
        textLines = (textFile.text.Split("\n"[0]));
    }
    if (endAtLine == 0){
        endAtLine = textLines.Length - 1;
    }
    if(isActive) {
        EnableTextBox();
    }
    else {
        DisableTextBox();
    }
}

function Update () {
    if(!isActive) {
        return;
    }
    if (Input.GetButtonDown("Jump") && !GameObject.Find("UICanvas").GetComponent(PauseMenu).paused) {
        if (!isTyping) {
            currentLine += 1;

            if(currentLine > endAtLine) {
                DisableTextBox();
            }
            else {
                TextScroll(textLines[currentLine]);
            }
        }
        else if (isTyping && !cancelTyping) {
            cancelTyping = true;
        }
    }
}

function TextScroll(lineOfText : String) {
    var playerControl : Component;
    if (GameObject.Find("Player") == null) {
        playerControl = GameObject.FindWithTag("Player").GetComponent(PlayerControlIntro);
    }
    if (GameObject.Find("Player Intro") == null) {
        playerControl = GameObject.FindWithTag("Player").GetComponent(PlayerControl);
    }
    var NPC = playerControl.NPC;
    //var source = GameObject.FindWithTag("Player").GetComponent(AudioSource);
    //var clip = NPC.GetComponent(NPCTalkSound).talkSound;
    var letter : int = 0;
    theText.text = "";
    isTyping = true;
    cancelTyping = false;
    while (isTyping && !cancelTyping && letter < lineOfText.Length - 1) {
        theText.text += lineOfText[letter];
        letter += 1;
        //source.PlayOneShot(clip);
        yield new WaitForSeconds(typeSpeed);
    }
    theText.text = lineOfText;
    isTyping = false;
    cancelTyping = false;
}

function EnableTextBox() {
    var player = GameObject.FindWithTag("Player");
    textBox.SetActive(true);
    isActive = true;
    if (player.GetComponent(PlayerControl) == null) {
        player.GetComponent(PlayerControlIntro).playerMove = false;
    }
    else {
        player.GetComponent(PlayerControl).playerMove = false;
    }
    player.GetComponent(Animator).SetBool ("run", false);
    TextScroll(textLines[currentLine]);
}

function DisableTextBox() {
    textBox.SetActive(false);
    isActive = false;
    GameObject.FindWithTag("Player").GetComponent(PlayerControlIntro).playerMove = true;
}

function ReloadScript(theText : TextAsset) {
    if(theText != null) {
        textLines = new String[1];
        textLines = (theText.text.Split("\n"[0]));
    }
}