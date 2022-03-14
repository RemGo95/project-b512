using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform target;
    public float smoothSpeed = 0.500f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
       // offset.x = 2 * target.transform.localScale.x;
        offset.y = 10 * target.transform.localScale.y;
        offset.z = -5 * target.transform.localScale.z;
    }

    void LateUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
