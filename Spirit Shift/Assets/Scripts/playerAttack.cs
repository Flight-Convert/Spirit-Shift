using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject fist;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        fist.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(punch());
        }
    }

    IEnumerator punch()
    {
        fist.SetActive(true);
        yield return new WaitForSeconds(delay);
        fist.SetActive(false);
        yield return true;
    }
}
