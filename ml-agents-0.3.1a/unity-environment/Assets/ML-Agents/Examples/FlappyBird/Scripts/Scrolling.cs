using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {

    private Rigidbody2D envRB;

	// Use this for initialization
	void Start () {
        InitScrolling();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.instance.gameOver && GameController.instance != null)
        {
            envRB.velocity = Vector2.zero;
        }
	}

    public void InitScrolling()
    {
        envRB = GetComponent<Rigidbody2D>();
        envRB.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
    }
}
