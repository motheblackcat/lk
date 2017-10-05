//This script manage the intro scene at the start of the game.
var skip : boolean;

function Start(){
    GameObject.Find("Player Intro").GetComponent(PlayerControlIntro).playerMove = false;

    yield WaitForSeconds(5);
    if(!skip){
        GetComponent(BoxCollider2D).enabled = true;
        GameObject.Find("Bartender").GetComponent(BoxCollider2D).enabled = true;
    }
}

function Update() {
    var player = GameObject.Find("Player Intro");
    var bartender = GameObject.Find("Bartender");
    var currentLine = GameObject.Find("TextBoxManager").GetComponent(NPCDialog).currentLine;

    if (Input.GetButtonDown("Select") && !GameObject.Find("UICanvas").GetComponent(PauseMenu).paused && !skip) {
        skip = true;
        currentLine = 7;
        bartender.GetComponent(BoxCollider2D).enabled = true;
        player.GetComponent(PlayerControlIntro).playerMove = true;
        if(GameObject.Find("DialogBox")){
            GameObject.Find("DialogBox").SetActive(false);
            GameObject.Find("TextBoxManager").GetComponent(NPCDialog).isActive = false;
            GameObject.Find("TextBoxManager").GetComponent(NPCDialog).isTyping = false;
        }
    }
    if (currentLine >= 5) { 
        GetComponent(BoxCollider2D).enabled = false;
        player.GetComponent(Animator).enabled = true;
        player.GetComponent(Rigidbody2D).constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    if (currentLine >= 7) {
        skip = true;
        bartender.GetComponent(ActivateTextAtLine).enabled = true;
    }
}