using UnityEngine;
using System.Collections;

public class Player1 : PlayerController {
	
	// Update is called once per frame
	void Update () {
        base.handleInput("HorizontalP1", "JumpP1");
	}
}
