using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public bool columnPassed = false;
    public bool gameOver = false;
    public bool score = false;
    public float scrollSpeed = -1.5f;

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
	}

    void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdDie()
    {
        gameOver = true;
    }

    public void BirdScore()
    {
        if (gameOver) return;

        score = true;
        
    }
}
