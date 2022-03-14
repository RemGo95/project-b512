using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Random = System.Random;
using System;
using System.IO;
using UnityEngine.UI;

public class Pack_AI : Agent
{
    public Rigidbody act_Rig;
    System.Diagnostics.Stopwatch timer12 = new System.Diagnostics.Stopwatch();

    //Input Data
    public float tolerance=0.01f;
    public int lifeBit = 0;
    public int _packID = 0;
    public float _packSizeX = 0.0f;
    public float _packSizeY = 0.0f;
    public float _packSizeZ = 0.0f;

    //Output Data
    float xps;
    float yps;
    float zps;
    float xrs;
    float yrs;
    float zrs;

    //Palet
    public int PN = 0;
    [SerializeField] public int _maxLayerNumber=10;
    public float _paletXsize = 0.6f;
    public float _paletYsize = 0.2f;
    public float _paletZsize = 0.8f;


    //Packages ... one height
    [SerializeField] public int _maxPackNumber=10;
    public float _packsHeight = 0.2f;
    public float _maxPackSize = 0.4f;

    public float _actPacksizX = 0.0f;
    public float _actPacksizY = 0.0f;
    public float _actPacksizZ = 0.0f;
    public float _actPackposX = 0.0f;
    public float _actPackposY = 0.0f;
    public float _actPackposZ = 0.0f;
    public int _actLayerNumber = 0;

    //variebles for volume
    float oldRawXpos;
    float oldRawYpos;
    float oldRawZpos;
    float oldRawXsiz;
    float oldRawYsiz;
    float oldRawZsiz;
    float posYv;
    float posXv;
    float posZv;
    float sizeYv;
    float sizeXv;
    float sizeZv;
    float rawV;


    //Agent Settings and moves
    public int EpisodeNumber=0;
    public float movementSpeed = 1f;
    public float _rigAcc = 0f;
    float rawX;
    float rawY;
    float rawZ;
    public float Reward=0f;
    public float ThrustX;
    public float ThrustY;
    public float ThrustZ;
    float _oldXpos;
    float _oldYpos;
    float _oldZpos;

    [SerializeField] Text RewardText;
    [SerializeField] Text EpisodeText;
    [SerializeField] Text XposText;
    [SerializeField] Text YposText;
    [SerializeField] Text ZposText;
    [SerializeField] Text XsizeText;
    [SerializeField] Text YsizeText;
    [SerializeField] Text ZsizeText;

    [SerializeField] Transform target;
    [SerializeField] private GameObject PackagesV;
    [SerializeField] private GameObject ActualPackage;
    [SerializeField] private GameObject OldPackage;

    public GameObject Pack0;
    public List<GameObject> Packages;
    public List<float> RawObservations;

    //AI AGENT
    int _ResetTime=0;
    public int EpisodeTime=0;
    float moveX;
    float moveZ;
    float rotY;
    int c;
    // [SerializeField] 
    // Start is called before the first frame update
    public override void OnEpisodeBegin()
    {

        base.OnEpisodeBegin();
        EpisodeNumber++;
        GetSize();
        Spawn();

        EpisodeTime = 0;
        timer12.Stop();
        timer12.Reset();
        timer12.Start();

    }

    public void CheckAngle()
    {

    }

    public void CalcReward()
    {
        Reward = 0.1f + (PN * 0.1f);
    }

    public void Clear()
    {
        for (int i = 0; i < Packages.Count; i++)
        {
            Destroy(Packages[i]);
            //PN = 0;
            if (i == Packages.Count - 1)
            {
                Packages.Clear();
            }
        }
    }

    public void SaveDataActualPackage()
    {
        _actPackposX = transform.position.x;
        _actPackposY = transform.position.x;
        _actPackposZ = transform.position.x;

        _actPacksizX = transform.localScale.x;
        _actPacksizY = transform.localScale.y;
        _actPacksizZ = transform.localScale.z;
    }

    public void WatchTime()
    {
        EpisodeTime = Convert.ToInt32(timer12.Elapsed.TotalMilliseconds);
        if (EpisodeTime >= _ResetTime)
        {
            SetReward(-1);
            //Clear();
            EndEpisode();
        }
    } 

    public void RepairAngles()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PN > 0)
        {
            Packages[PN-1].transform.rotation = Quaternion.Euler(0, 0, 0);
           // Packages[PN].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Forces()
    {
        int f=0;
        
        act_Rig = GetComponent<Rigidbody>();

        if(Math.Abs(_oldXpos - 0.05f)< 0.05)
        {

           //transform.position.x; 
        }

        ThrustX = 0f;
        ThrustY = 0f;
        ThrustZ = 0f;
        
       // if(transform.)

        switch(f)
        {
            case 1:
                ThrustX = 0f;
                ThrustZ = _rigAcc;
                break;
            case 2:
                    ThrustX = 0f;
                    ThrustZ = -1f * _rigAcc;
                    break;
            case 3:
                    ThrustX = _rigAcc;
                    ThrustZ = 0f;
                    break;
            case 4:
                    ThrustX = -1f * _rigAcc;
                    ThrustZ = 0f;
                    break;


        }

        act_Rig.AddForce(ThrustX, ThrustY, ThrustZ, ForceMode.Acceleration);

        //save pos
    }

    // Update is called once per frame
    void Update()
    {

        RepairAngles();

        if (PN >= _maxPackNumber && _actPackposX == 1000f)
        {
            CalcReward();
            SetReward(Reward);
            Clear();
            EndEpisode();
        }

        //WatchTime();

        if(transform.position.y <= ((_packsHeight/2f)+tolerance))//+ (PN * _packsHeight)))
        {
            SaveDataActualPackage();
            GenerateOldPackage();
             
            GetSize();
            timer12.Stop();
            timer12.Reset();
            timer12.Start();

           // CalcReward();
           // SetReward(Reward);

          //  EndEpisode();
        }

        if(transform.position.y > 100f+ ((_maxLayerNumber * _packsHeight) - (_packsHeight / 2)) && Reward == 1000f)
        {
            CalcReward();
            SetReward(Reward);
            EndEpisode();
        }

        Forces();

        moveX = 0f;
        moveZ = 0f;
        // c++;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(target.position);
        sensor.AddObservation(transform.localScale);
      

    }

    public override void OnActionReceived(float[] actions)
    {
        float ActionCorr = 0.5f;

        moveX = actions[0];
        moveZ = actions[1];
        rotY = actions[2];
        //rotY = actions[2];


        if (moveZ < 0)
        {
            //  moveZ = moveZ * (-1f);
        }

        if (Convert.ToInt32(timer12.Elapsed.TotalMilliseconds) > 3000)
        {
            //_actPacksizX = _actPacksizZ
        }

       // transform.position += new Vector3(moveX*ActionCorr, 0, moveZ*ActionCorr) * Time.deltaTime * movementSpeed;
        

        RepairAngles();
        // Rigidbody.velocity


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            /// float droga;
            // droga = 
            SetReward(-1f);
            EndEpisode();
        }
    }


    //Get size from vision system or get random for simulation
    public void GetSize()
    {
        int p;
        float correction = 1f;
        Random pos3 = new Random();
        p = pos3.Next(1, 3);

        switch (p)
        {
            case 1:
                _packSizeX = 0.20f*correction;
                _packSizeY = 0.20f*correction;
                _packSizeZ = 0.20f*correction;
                break;
            case 2:
                _packSizeX = 0.40f*correction;
                _packSizeY = 0.20f*correction;
                _packSizeZ = 0.20f*correction;
                break;
            case 3:
                _packSizeX = 0.20f*correction;
                _packSizeY = 0.20f*correction;
                _packSizeZ = 0.40f*correction;
                break;

        }

        transform.localScale = new Vector3(_packSizeX,_packSizeY,_packSizeZ);

    }
    
    public void GetRandomColor()
    {
        int color;
        Random pos5 = new Random();
        color = pos5.Next(1, 4);

        switch (color)
        {
            case 1:
                ActualPackage.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                break;
            case 2:
                ActualPackage.GetComponent<Renderer>().material.color = new Color(200, 5, 1);
                break;
            case 3:
                ActualPackage.GetComponent<Renderer>().material.color = new Color(20, 5, 255);
                break;

        }
    }

    public void CalcVolume()
    {
        if (PN > 1)
        {
            //Packages[PN - 3].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            sizeXv = (Packages[PN - 2].transform.localPosition.x) + (Packages[PN - 1].transform.localScale.x / 2f);
            sizeYv = (Packages[PN - 2].transform.localPosition.y) + (Packages[PN - 1].transform.localScale.y / 2f);
            sizeZv = (Packages[PN - 2].transform.localPosition.z) + (Packages[PN - 1].transform.localScale.z / 2f);

            if ((PackagesV.transform.localScale.x / 2) + PackagesV.transform.localPosition.x > (Packages[PN - 2].transform.localPosition.x) + (Packages[PN - 2].transform.localScale.x / 2f))
            {
                sizeXv = PackagesV.transform.localScale.x;
            }

            if ((PackagesV.transform.localScale.y / 2) + PackagesV.transform.localPosition.y > (Packages[PN - 2].transform.localPosition.y) + (Packages[PN - 2].transform.localScale.y / 2f))
            {
                sizeYv = PackagesV.transform.localScale.y;
            }

            if ((PackagesV.transform.localScale.z / 2) + PackagesV.transform.localPosition.z > (Packages[PN - 2].transform.localPosition.z) + (Packages[PN - 2].transform.localScale.z / 2f))
            {
                sizeZv = PackagesV.transform.localScale.z;
            }

            PackagesV.transform.localScale = new Vector3(sizeXv, sizeYv, sizeZv);
            posYv = 0.05f + (PackagesV.transform.localScale.y / 2f);

            float vxpos;
            float vzpos;
            vxpos = sizeXv / 2f;
            vzpos = sizeZv / 2f;
            PackagesV.transform.localPosition = new Vector3(vxpos, posYv, vzpos);
        }
    }

    public void Spawn()
    {
        float H = 0f;
        H = (_packsHeight / 2f) + (_packsHeight * _actLayerNumber) + _packsHeight*2f;

        transform.position = new Vector3((transform.localScale.x/2f), H, (transform.localScale.z/ 2f));
        //transform.localScale
    }

    public void GenerateOldPackage()
    {
        PN++;
        //Problem with rigbofy !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        float corr = 1f; 
        oldRawXsiz = _actPacksizX;
        oldRawZsiz = _actPacksizZ;
        oldRawYsiz = _actPacksizY;
        oldRawXpos = _actPackposX;
        oldRawZpos = _actPackposZ;
        oldRawYpos = _actPackposY;

        GameObject newPackage = Instantiate(OldPackage, Vector3.zero, Quaternion.identity) as GameObject;
        //newPackage.AddComponent<BoxCollider>();
        //newPackage.AddComponent<Rigidbody>();
       // newPackage.transform.parent = Pack0.transform;
        //newPackage.GetComponent<Renderer>().material.color = new Color(PN % 4, PN % 11, PN % 3);
        //newPackage.AddComponent<NewBehaviourScript>().
        //newPackage.transform.localPosition = new Vector3 (0.45f, 4.58f, 0.39f);
        //newPackage.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
        //
        // Random rnd = new Random();
        //PackN = rnd.Next(1, 5);
        //
        newPackage.transform.localScale = new Vector3((oldRawXsiz*corr), oldRawYsiz*corr, oldRawZsiz*corr);
        newPackage.transform.localPosition = new Vector3(oldRawXpos * corr, oldRawYpos * corr - 0.01f, oldRawZpos * corr);
        transform.position = new Vector3(oldRawXpos, (oldRawYpos + 1f), oldRawZpos);
        newPackage.AddComponent<BoxCollider>();
        newPackage.AddComponent<Renderer>();
        newPackage.AddComponent<Rigidbody>();
        
        
        newPackage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //newPackage.transform.localRotation = Quaternion.Euler(xrs, yrs, zrs);
        // newPackage.transform.parent = RawP.transform;
        //newPackage.transform.parent = RawPackage.transform;
        Packages.Add(newPackage);
        
    }

    public void Display()
    {
        //float r;
       // r = 0.0f;
        // r = ((Fillreward) + (PN * 0.1f));
        //r = SumRewards;
        EpisodeText.text = EpisodeNumber.ToString();
        RewardText.text = Reward.ToString();
        XsizeText.text = _packSizeX.ToString();
        YsizeText.text = _packSizeY.ToString();
        ZsizeText.text = _packSizeZ.ToString();
        
    }

}