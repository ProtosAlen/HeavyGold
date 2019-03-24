using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bots : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float speed;
    private float range1; private bool movingTo1;
    private float range2; private bool movingTo2;

    private void Start()
    {
        movingTo1 = true;
    }

    public void Update()
    {
        range1 = Vector3.Distance(transform.position, target1.position);
        if (range1 < 1) {  movingTo1 = false; movingTo2 = true; transform.localRotation *= Quaternion.Euler(0, 180, 0); }
        range2 = Vector3.Distance(transform.position, target2.position);
        if (range2 < 1) {  movingTo2 = false; movingTo1 = true; transform.localRotation *= Quaternion.Euler(0, -180, 0); }

        if(movingTo1 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.position, speed * Time.deltaTime);
        }
        if (movingTo2 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
        }
    }
}
