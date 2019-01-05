using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdAgent : Agent {

    private GameObject[] columnsRef;
    private BirdController birdController;
    private Rigidbody2D birdRB;

    public override void InitializeAgent()
    {
        birdController = GetComponent<BirdController>();
        birdRB = GetComponent<Rigidbody2D>();
    }

    public override void AgentReset()
    {
        gameObject.transform.position = Vector3.zero;
        birdRB.velocity = Vector2.zero;
        
        ColumnPool poolScript = FindObjectOfType<ColumnPool>();
        poolScript.Reset();

        Scrolling[] scrollingObjects = (Scrolling[])FindObjectsOfType(typeof(Scrolling));
        foreach (Scrolling scrollingObject in scrollingObjects)
        {
            scrollingObject.InitScrolling();
        }
        birdController.isDead = false;
        GameController.instance.gameOver = false;
    }

    public override void CollectObservations()
    {
        Transform[] targetColumnsTransform = GetTargetColumnsTransform();
        Transform currentTarget = GetClosestTarget(targetColumnsTransform);

        if (currentTarget != null)
        {
            AddVectorObs(gameObject.transform.position.x - currentTarget.position.x);
            AddVectorObs(gameObject.transform.position.y - currentTarget.position.y);  
        }
        else
        {
            AddVectorObs(0f);
            AddVectorObs(0f);
        }

        AddVectorObs(gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float reward = 0.1f;
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            birdController.ApplyForce();
            
        }
        if(birdController.isDead)
        {
            reward = -10f;
            Done();
        }
        else
        {
            if (GameController.instance.score)
            {
                GameController.instance.score = false;
                reward = 1f;
            }
        }
        AddReward(reward);
    }

    public Transform[] GetTargetColumnsTransform()
    {
        GameObject[] targetColumnsObj = GameObject.FindGameObjectsWithTag("Goal");
        int columnsLength = GameController.instance.GetComponent<ColumnPool>().columns.Length;
        Transform[] targetColumnsTransform = new Transform[columnsLength];
        for (int i = 0; i < columnsLength; i++)
        {
            targetColumnsTransform[i] = targetColumnsObj[i].transform;
        }
        return targetColumnsTransform;
    }

    public Transform GetClosestTarget(Transform[] targets)
    {
        Vector3 currentPos = transform.position;
        Transform targetToReach = null;
        float minDistance = Mathf.Infinity;
        foreach (Transform posibleTarget in targets)
        {
            Vector3 dirToTarget = posibleTarget.position - currentPos;
            float distanceToTarget = dirToTarget.sqrMagnitude;
            if (distanceToTarget < minDistance && posibleTarget.position.x > currentPos.x)
            {
                minDistance = distanceToTarget;
                targetToReach = posibleTarget;
            }
        }

        return targetToReach;

    }
}
