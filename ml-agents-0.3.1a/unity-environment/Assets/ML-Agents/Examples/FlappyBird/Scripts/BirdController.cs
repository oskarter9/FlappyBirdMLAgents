using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    public float upForce = 200f;
    public bool isDead = false;

    private Rigidbody2D birdRB;

    void Awake()
    {
        birdRB = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        if (!isDead) {
            if (Input.GetMouseButtonDown(0))
            {
                ApplyForce();
            }
        } 
	}

    public void ApplyForce()
    {
        birdRB.velocity = Vector2.zero;
        birdRB.AddForce(new Vector2(0, upForce));
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        birdRB.velocity = Vector2.zero;
        isDead = true;
        GameController.instance.BirdDie(); 
    }
}
