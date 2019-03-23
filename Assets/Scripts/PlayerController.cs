﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float jumpPwr;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Transform target;

    void FixedUpdate()
    {
        
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * speed);

        if (Input.GetKey(KeyCode.W)) // Forward
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
            //rb.AddForce(Vector3.forward * speed);
        }


        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            rb.AddForce(Vector3.up * jumpPwr);
        }
        
    }
}
