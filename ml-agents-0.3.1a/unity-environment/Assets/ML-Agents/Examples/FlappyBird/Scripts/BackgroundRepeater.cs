using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour {

    private BoxCollider2D groundCollider;
    private float horizontalLength;

    void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start () {
        horizontalLength = groundCollider.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -horizontalLength)
        {
            RepositionBackground();
        }
	}

    void RepositionBackground()
    {
        transform.Translate(Vector2.right * horizontalLength * 2);
    }
}
