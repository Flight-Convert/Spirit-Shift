/* Broc Edson
 * Liam Barrett
 * Spirit Shift
 * Let's the enemy attack the player
 */
// Uses some code from followPlayer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private bool justCharged;
    private float distance;
    public float threshold;
    public float delay;
    public float chargeDelay;
    public float chargeForce;
    public GameObject fist;

    // This number specifies the enemy type: 0 for a normal enemy, 1 for a charger, 2 for a shooter
    public int enemyType = 0;

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
                StartCoroutine(Attack());
            }
        }
        //if player isn't in enemy body
        else
        {
            //if enemy is close enough to player body
            if (distance <= threshold)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        if(enemyType == 0)
        {
            fist.SetActive(true);
            yield return new WaitForSeconds(delay);
            fist.SetActive(false);
            yield return true;
        }
        else if(enemyType == 1)
        {
            if(!justCharged)
            {
                justCharged = true;
                fist.SetActive(true);
                rb2d.AddForce(chargeForce * findDirectionFromPos(player.transform.position), ForceMode2D.Impulse);
                yield return new WaitForSeconds(delay);
                fist.SetActive(false);
                yield return new WaitForSeconds(chargeDelay);
                justCharged = false;
            }
            else
            {
                yield return true;
            }
            
        }
        else
        {
            yield return true;
        }
    }

    Vector2 findDirectionFromPos(Vector2 pos)
    {
        Vector2 direction = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);

        direction = new Vector2(direction.normalized.x, direction.normalized.y);

        return direction;
    }
}
