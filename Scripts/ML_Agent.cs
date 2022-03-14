using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class ML_Agent : Agent
{
    //Agent Settings
    public float movementSpeed=20f;
    float rawX;
    float rawY;
    float rawZ;
    [SerializeField] Transform target;

    //AI AGENT 
    float moveX;
    float moveZ;
    int c;
   // [SerializeField] 
    // Start is called before the first frame update
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        
        rawX = (Random.Range(1, 100))/100f;
        rawY = (Random.Range(1, 100)) / 100f;
        transform.position = new Vector3(-2f, 0.1f, -3.12f);
    }

    // Update is called once per frame
    void Update()
    {
        moveX=0f;
        moveZ=0f;
        // c++;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);

    }

    public override void OnActionReceived(float[] actions)
    {
        moveX = actions[0];
        moveZ = actions[1];

        if(moveZ < 0)
        {
          //  moveZ = moveZ * (-1f);
        }

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * movementSpeed;
       // Rigidbody.velocity


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }

        if(other.TryGetComponent<Wall>(out Wall wall))
        {
           /// float droga;
           // droga = 
            SetReward(-1f);
            EndEpisode();
        }
    }
}
