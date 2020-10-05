using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWaveEnd : MonoBehaviour
{
    private float targetTime;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<TutorialManager>()) Destroy(this);
        targetTime = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().targetTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= targetTime)
        {
            Destroy(gameObject);
        }
    }
}
