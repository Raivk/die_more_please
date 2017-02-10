using UnityEngine;
using System.Collections;

public class Player2 : PlayerController {
	
	// Update is called once per frame
	void Update () {
        base.handleInput("HorizontalP2", "JumpP2");
    }
}
