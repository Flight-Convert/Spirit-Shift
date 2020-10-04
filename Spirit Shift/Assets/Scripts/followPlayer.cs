/* Broc Edson
 * Liam Barrett
 * Wolfgang Gross
 * Spirit Shift
 * Makes the enemy follow the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class followPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private Vector3 playerPos;
    public float force;
    public float speed;

    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player Inactive");
        }
        rb2d = GetComponent<Rigidbody2D>();
        turnSpeed = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Determines where to rotate towards
        Vector3 targetDirection = player.transform.position - transform.position;

        //Single turn unit
        float singleUnit = turnSpeed * Time.deltaTime;

        //Rotate the forward vector towards the target direction by one unit
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleUnit, 0.0f);

        // Locks it in 2d
        newDirection = new Vector3(0f, 0f, newDirection.z);

        //Debug raycast to check pointing direction (Scene view)
        Debug.DrawRay(transform.position, newDirection, Color.red);

        //Calculate rotation a unit closer to the target and applies rotation to object
        transform.rotation = Quaternion.LookRotation(newDirection);

        // enemy in control
        if (GetComponent<BasicMovement>() == null)
        {
            playerPos = (player.transform.position);

            if (Mathf.Abs(rb2d.velocity.magnitude) <= speed)
            {
                //only add force to shooters when above a certain distance from player
                attackPlayer attack = GetComponent<attackPlayer>();
                if (GetComponent<attackPlayer>().enemyType == 2 && attackPlayer.distance >= GetComponent<attackPlayer>().threshold
                    || GetComponent<attackPlayer>().enemyType != 2)
                {
                    rb2d.AddForce(findDirectionFromPos(playerPos) * force * Time.deltaTime);
                }
            }
        }
    }

    Vector2 findDirectionFromPos(Vector2 pos)
    {
        Vector2 direction = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);

        direction = new Vector2(direction.normalized.x, direction.normalized.y);

        return direction;
    }
}
