using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeXAgent : Agent {

    public GameObject targetRef;
    public float speed = 3f;

    public override void AgentReset()
    {
        gameObject.transform.localPosition = Vector3.zero;
        targetRef.transform.localPosition = new Vector3(Random.Range(3f, 7f) * (Random.value <= 0.5 ? 1 : -1), 0, Random.Range(3f, 7f) * (Random.value <= 0.5 ? 1 : -1));
    }

    public override void CollectObservations()
    {
        AddVectorObs(new Vector2(transform.localPosition.x, targetRef.transform.localPosition.z));
        AddVectorObs(new Vector2(targetRef.transform.localPosition.x, targetRef.transform.localPosition.z));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if(brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            float initialDistanceToTarget = Vector3.Distance(targetRef.transform.position, transform.position);

            float horizontal = Mathf.RoundToInt(Mathf.Clamp(vectorAction[0], -1, 1));
            float vertical = Mathf.RoundToInt(Mathf.Clamp(vectorAction[1], -1, 1));

            float newX = transform.localPosition.x + (horizontal * speed * Time.deltaTime);
            float newZ = transform.localPosition.z + (vertical * speed * Time.deltaTime);
            newX = Mathf.Clamp(newX, -7.3f, 7.3f);
            newZ = Mathf.Clamp(newZ, -7.3f, 7.3f);
            transform.localPosition = new Vector3(newX, 0, newZ);

            float finalDistanceToTarget = Vector3.Distance(targetRef.transform.position, transform.position);

            if (finalDistanceToTarget < initialDistanceToTarget)
            {
                AddReward(0.1f);
            }
            else
            {
                AddReward(-0.3f);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("target"))
        {
            AddReward(30f);
            Done();
        }
    }
}
