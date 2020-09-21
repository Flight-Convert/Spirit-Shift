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
    private bool justAttacked;
    private float distance;
    public float threshold;
    public float punchDuration;
    public float attackDelay;
    public float chargeForce;
    public GameObject fist;
    
    public GameObject bullet;

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
            if(!justAttacked)
            {
                justAttacked = true;
                fist.SetActive(true);
                yield return new WaitForSeconds(punchDuration);
                fist.SetActive(false);
                yield return new WaitForSeconds(attackDelay);
                justAttacked = false;
                yield return true;
            }
            else
            {
                yield return true;
            }
        }
        else if(enemyType == 1)
        {
            if(!justAttacked)
            {
                justAttacked = true;

                //Prepare to charge
                justAttacked = true;
                fist.SetActive(true);
                yield return new WaitForSeconds(punchDuration);
                
                //Charge
                rb2d.AddForce(chargeForce * findDirectionFromPos(player.transform.position), ForceMode2D.Impulse);
                yield return new WaitForSeconds(attackDelay);
                
                //Done Charging
                yield return new WaitForSeconds(punchDuration);
                fist.SetActive(false);
                justAttacked = false;
                yield return true;
            }
            else
            {
                yield return true;
            }
        }
        else
        {
            if (!justAttacked)
            {
                justAttacked = true;
                Vector3 bulletAngle = FindAngle();
                Debug.Log(bulletAngle.z);
                Instantiate(bullet, transform.position, Quaternion.Euler(bulletAngle.x, bulletAngle.y, bulletAngle.z));
                yield return new WaitForSeconds(attackDelay);
                justAttacked = false;
                yield return true;
            } 
            else
            {
                yield return true;
            }
        }
    }

    Vector2 findDirectionFromPos(Vector2 pos)
    {
        Vector2 direction = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);

        direction = new Vector2(direction.normalized.x, direction.normalized.y);

        return direction;
    }

    // This method finds an angle using the position of the player using trigonometry
    Vector3 FindAngle()
    {
        Vector3 rot;

        float xDiff = Mathf.Abs(player.transform.position.x - transform.position.x);
        float yDiff = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (player.transform.position.x >= transform.position.x && player.transform.position.y >= transform.position.y)
        {
            rot = new Vector3(0.0f, 0.0f, Mathf.Atan(yDiff / xDiff) * Mathf.Rad2Deg);
        }
        else if (player.transform.position.x < transform.position.x && player.transform.position.y >= transform.position.y)
        {
            rot = new Vector3(0.0f, 0.0f, 90f + (Mathf.Atan(xDiff / yDiff) * Mathf.Rad2Deg));
        }
        else if (player.transform.position.x <= transform.position.x && player.transform.position.y < transform.position.y)
        {
            rot = new Vector3(0.0f, 0.0f, 180f + (Mathf.Atan(yDiff / xDiff) * Mathf.Rad2Deg));
        }
        else
        {
            rot = new Vector3(0.0f, 0.0f, 270f + (Mathf.Atan(xDiff / yDiff) * Mathf.Rad2Deg));
        }

        return rot;
    }
}
