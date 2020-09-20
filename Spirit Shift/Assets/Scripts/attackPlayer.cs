/* Broc Edson
 * Spirit Shift
 * Let's the enemy attack the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private float distance;
    public float threshold;
    public float delay;
    public GameObject fist;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        fist.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(rb2d.transform.position, player.transform.position);

        //if player is in enemy body
        if (rb2d.GetComponent<BasicMovement>())
        {
            //on left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(punch());
            }
        }
        //if player isn't in enemy body
        else
        {
            //if enemy is close enough to player body
            if (distance <= threshold)
            {
                StartCoroutine(punch());
            }
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
