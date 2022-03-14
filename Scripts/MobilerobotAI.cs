// R.Golinski 4.3.22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MobilerobotAI : Agent
{
    public float movementSpeed = 20f;
    float rawX;
    float rawY;
    float rawZ;
    [SerializeField] Transform MobileRobot;
    [SerializeField] Transform target;
    Rigidbody _RobotRig;
    float moveX;
    float moveZ;
    public float moveAngle;
    public float _mobileRobotAceleration;
    public float _accP;
    int c;

    #region
    //5DOF ARM
    [SerializeField] GameObject Axis0;
    [SerializeField] GameObject Axis1;
    [SerializeField] GameObject Axis2;
    [SerializeField] GameObject Axis3;
    [SerializeField] GameObject Axis4;
    [SerializeField] GameObject Axis5;
    [SerializeField] GameObject Axis6;

    public float targetAngle0 = 0;
    public float targetAngle1 = 0;
    public float targetAngle2 = 0;
    public float targetAngle3 = 0;
    public float targetAngle4 = 0;
    public float targetAngle5 = 0;
    public float targetAngle6 = 0;

    public float acttargetAngle0 = 0;
    public float acttargetAngle1 = 0;
    public float acttargetAngle2 = 0;
    public float acttargetAngle3 = 0;
    public float acttargetAngle4 = 0;
    public float acttargetAngle5 = 0;
    public float acttargetAngle6 = 0;

    public float rotationDegreesPerSecond = 45f;
    public float rotationDegreesAmount = 90f;
    private float totalRotation = 0;
    public float degreesPerSecond = 1f;

    public float turnSpeed = 0.01f;

    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        _RobotRig = MobileRobot.GetComponent<Rigidbody>();
        base.OnEpisodeBegin();

        rawX = (Random.Range(1, 100)) / 100f;
        rawY = (Random.Range(1, 100)) / 100f;
        transform.position = new Vector3(0f, 0.5f, -0.77f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);

    }
    
    private void Turn()
    {
        float direction = moveAngle;
        int rotationMultiplier = 1;
        transform.Rotate(new Vector3(0, direction * rotationMultiplier, 0));
    }



    public override void OnActionReceived(float[] actions)
    {
        moveAngle = actions[0];
        _mobileRobotAceleration = actions[1];
        float[] moveAngle = actions[2];

        //Turn();
        transform.position += new Vector3(0.1f+_mobileRobotAceleration, 0, 0) * Time.deltaTime * _accP;
        // Rigidbody.velocity
        //_RobotRig.AddForce(new Vector3(-_mobileRobotAceleration*_accP, 0,0));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            /// float droga;
            // droga = 
            SetReward(-1f);
            EndEpisode();
        }
    }

    public void SendDataArm()
    {

    }

    public void ArmMove()
    {
        rot0();
        rot1();
        rot2();
        rot3();
        rot4();
       
    }

    public void rot0()
    {
        if (targetAngle0 < 0)
        {
            if (acttargetAngle0 > targetAngle0)
            {
                acttargetAngle0 = acttargetAngle0 - turnSpeed;
            }
            Axis0.transform.localRotation = Quaternion.Euler(0, acttargetAngle0, 0);
        }
        else if (targetAngle0 > 0)
        {
            if (acttargetAngle0 < targetAngle0)
            {
                acttargetAngle0 = acttargetAngle0 + turnSpeed;
            }
            Axis0.transform.localRotation = Quaternion.Euler(0, acttargetAngle0, 0);
        }
    }

    public void rot1()
    {
        if (targetAngle1 < 0)
        {
            if (acttargetAngle1 > targetAngle1)
            {
                acttargetAngle1 = acttargetAngle1 - turnSpeed;
            }
            Axis1.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle1);
        }
        else if (targetAngle1 > 0)
        {
            if (acttargetAngle1 < targetAngle1)
            {
                acttargetAngle1 = acttargetAngle1 + turnSpeed;
            }
            Axis1.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle1);
        }
    }

    public void rot2()
    {
        if (targetAngle2 < 0)
        {
            if (acttargetAngle2 > targetAngle2)
            {
                acttargetAngle2 = acttargetAngle2 - turnSpeed;
            }
            Axis2.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle2);
        }
        else if (targetAngle2 > 0)
        {
            if (acttargetAngle2 < targetAngle2)
            {
                acttargetAngle2 = acttargetAngle2 + turnSpeed;
            }
            Axis2.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle2);
        }
    }

    public void rot3()
    {
        if (targetAngle3 < 0)
        {
            if (acttargetAngle3 > targetAngle3)
            {
                acttargetAngle3 = acttargetAngle3 - turnSpeed;
            }
            Axis3.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle3);
        }
        else if (targetAngle3 > 0)
        {
            if (acttargetAngle3 < targetAngle3)
            {
                acttargetAngle3 = acttargetAngle3 + turnSpeed;
            }
            Axis3.transform.localRotation = Quaternion.Euler(0, 0, acttargetAngle3);
        }
    }

    public void rot4()
    {
        if (targetAngle4 < 0)
        {
            if (acttargetAngle4 > targetAngle4)
            {
                acttargetAngle4 = acttargetAngle4 - turnSpeed;
            }
            Axis4.transform.localRotation = Quaternion.Euler(acttargetAngle4, 0, 0);
        }
        else if (targetAngle4 > 0)
        {
            if (acttargetAngle4 < targetAngle4)
            {
                acttargetAngle4 = acttargetAngle4 + turnSpeed;
            }
            Axis4.transform.localRotation = Quaternion.Euler(acttargetAngle4, 0, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //MobileRobot.transform.localPosition.x =+ +0.01f;
    }


}
