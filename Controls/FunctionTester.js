function Update () {
	if(Input.GetButtonDown("Attack")) {
        GameObject.Find("Player").GetComponent(PlayerHealth).Death();
        testDeath = false;
    }

    //Debug.Log(Mathf.Round(Time.time));
}
