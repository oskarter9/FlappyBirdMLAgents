using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {

    public int columnPoolSize = 5;
    public GameObject columnPref;
    public float spawnRate;
    public float colMin = 2f;
    public float colMax = 0.5f;
    public GameObject[] columns;

    private Vector2 objectPoolPos = new Vector2(-12, 0);
    private float timeSinceLastSpawn;
    private float spawnXPos = 12f;
    private int currentCol;

	// Use this for initialization
	void Start() {
        timeSinceLastSpawn = 0f;
        columns = new GameObject[columnPoolSize];
        for (int i=0; i < columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPref, objectPoolPos, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawn += Time.deltaTime;
        if(!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0f;
            float spawnYPos = Random.Range(colMin, colMax);
            columns[currentCol].transform.position = new Vector2(spawnXPos, spawnYPos);
            currentCol++;
            if(currentCol >= columnPoolSize)
            {
                currentCol = 0;
            }
        }
	}
    public void Reset()
    {
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i].transform.position = objectPoolPos;
        }
    }

}
