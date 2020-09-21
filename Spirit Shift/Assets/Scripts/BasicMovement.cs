using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//created by: Wolfgang Gross
//CIS491 - Project 1

public class BasicMovement : MonoBehaviour
{
    public float speed;
    
    //Animation to initialize here

    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Basic Movement variable
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //animation stuff goes here

        //Movement using basic transform
        transform.position = transform.position + movement * speed * Time.deltaTime;
    }
}
