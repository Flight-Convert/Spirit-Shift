using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//created by: Wolfgang Gross
//CIS491 - Project 1

public class BasicMovement : MonoBehaviour
{
    public float speed;
    private bool isCharger;
    
    //Animation to initialize here

    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        isCharger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Basic Movement variable
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //Determines if player is possessing a charger enemy
        try
        {
            isCharger = GetComponent<attackPlayer>().enemyType == 1 && !GetComponent<attackPlayer>().justAttacked;
        }
        catch (NullReferenceException e)
        {
            isCharger = false;
        }

        //animation stuff goes here

        //Movement using basic transform
        if (isCharger)
        {
            transform.position = transform.position + movement * speed * Time.deltaTime * 2;
        }
        else
        {
            transform.position = transform.position + movement * speed * Time.deltaTime;
        }
    }
}
