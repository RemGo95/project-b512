using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine.UI;
//using Random = System.Random;
using System.Threading;

public class Robot_AI : Agent
{
    //Agent Settings
    public int point=0;
    public float movementSpeed = 20f;
    float moveX;
    float moveZ;

    float rawX;
    float rawY;
    float rawZ;
    float oldrand;
    [SerializeField] Transform target;
    [SerializeField] Transform Platfrom;
    [SerializeField] GameObject Enemy;
    [SerializeField] Text Tekst;
    //  [SerializeField] Transform target1;
    // [SerializeField] Transform target2;
    // [SerializeField] Transform target3;
    int c;
    // [SerializeField] 
    // Start is called before the first frame update
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        rawX = (Random.Range(0, 12)) / 1f;
        //FixAngles();
        rawZ = (Random.Range(-3, 3)) / 1f;
        transform.localPosition = new Vector3(rawX, 5.01f, rawZ);
        oldrand = rawZ;
        movementSpeed = 20f;
    }

    void FixAngles()
    {
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //FixAngles();
        moveX = 0f;
        moveZ = 0f;

        if (transform.localPosition.y < 3.8f)
        {
            AddReward(-1f);
            EndEpisode();
        }

        if (Enemy.transform.localPosition.y < 3.8f)
        {
            point++;
            Tekst.text = point.ToString();
            AddReward(+1f);
            EndEpisode();
           // Tekst.Text
        }

        c++;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(Enemy.transform.localPosition);
       
        //sensor.AddObservation(target.position);

    }

    public override void OnActionReceived(float[] actions)
    {
        //transform.localPosition += new Vector3(0, 0, 0) * Time.deltaTime * movementSpeed;
        

        moveX = actions[0];
        moveZ = actions[1];
        

       transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * movementSpeed;
            // Rigidbody.velocity
        
      
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }

        
    }
}
