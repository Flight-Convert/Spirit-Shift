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
    //initialize reference to... 
    private Rigidbody2D rb2d;
    private GameObject player;
    private bool justAttacked;
    public static float distance = 0f;
    public float threshold;
    public float punchDuration;
    public float attackDelay;
    public float chargeForce;
    public GameObject fist;
    //private bool isControlled;
    private PlayerHealth playerHealthScript;
    private SpawnManager spawnManager;
    
    public GameObject bullet;
    public GameObject playerBullet;

    // This number specifies the enemy type: 1 for a charger, 2 for a shooter
    public int enemyType = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player Inactive");
        }
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();
        fist.SetActive(false);
        playerHealthScript = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PauseMenu>().paused) return;

        //NullReferenceException: Object reference not set to an instance of an object
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyType == 1)
        {
            if ((justAttacked) &&
                (other.CompareTag("Player") || other.CompareTag("Player Inactive")))
            {
                Debug.Log("Punched Player");
                playerHealthScript.TakeDamage();
            }
            else if (rb2d.GetComponent<BasicMovement>() && other.CompareTag("Enemy"))
            {
                Debug.Log("Punched Enemy");
                Destroy(other.gameObject);
                spawnManager.EnemyDestroyed();
            }
        }
    }

    IEnumerator Attack()
    {
        if(enemyType == 1)
        {
            if (spawnManager.OneEnemyRemaining())
            {
                //Give a second or two before killing the last (unkillable) enemy
                yield return new WaitForSeconds(2.0f);
                Destroy(gameObject);
            }
            if (!justAttacked)
            {
                justAttacked = true;

                //Prepare to charge
                justAttacked = true;
                fist.SetActive(true);
                yield return new WaitForSeconds(punchDuration);

                //Charge
                //If player tag true on controller then attack towards the cursor
                if (rb2d.GetComponent<BasicMovement>())
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                    Debug.Log(mousePos);
                    Vector3 mouseAngle = transform.rotation.eulerAngles - FindAngle(mousePos);
                    fist.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Abs(mouseAngle.x), Mathf.Abs(mouseAngle.y), Mathf.Abs(mouseAngle.z) + 90));
                    rb2d.AddForce(chargeForce * findDirectionFromPos(mousePos), ForceMode2D.Impulse);
                }
                //Else attack towards player
                else
                {
                    Vector3 fistAngle = transform.rotation.eulerAngles - FindAngle(player.transform.position);
                    fist.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Abs(fistAngle.x), Mathf.Abs(fistAngle.y), Mathf.Abs(fistAngle.z) + 90));
                    rb2d.AddForce(chargeForce * findDirectionFromPos(player.transform.position), ForceMode2D.Impulse);
                }

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
            if (spawnManager.OneEnemyRemaining())
            {
                //Give a second or two before killing the last (unkillable) enemy
                yield return new WaitForSeconds(2.0f);
                Destroy(gameObject);
            }
            if (!justAttacked)
            {
                justAttacked = true;
                //If player tag true on controller then shoot towards the cursor
                if (rb2d.GetComponent<BasicMovement>())
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                    Debug.Log(mousePos);
                    Vector3 bulletAngle = FindAngle(mousePos);
                    Debug.Log(bulletAngle.z);
                    Instantiate(playerBullet, transform.position, Quaternion.Euler(bulletAngle.x, bulletAngle.y, bulletAngle.z));
                }
                //Else shoot towards player
                else
                {
                    Vector3 bulletAngle = FindAngle(player.transform.position);
                    Debug.Log(bulletAngle.z);
                    Instantiate(bullet, transform.position, Quaternion.Euler(bulletAngle.x, bulletAngle.y, bulletAngle.z));
                }

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
    Vector3 FindAngle(Vector3 pos)
    {
        Vector3 rot;

        float xDiff = Mathf.Abs(pos.x - transform.position.x);
        float yDiff = Mathf.Abs(pos.y - transform.position.y);

        if (pos.x >= transform.position.x && pos.y >= transform.position.y)
        {
            rot = new Vector3(0.0f, 0.0f, Mathf.Atan(yDiff / xDiff) * Mathf.Rad2Deg);
        }
        else if (pos.x < transform.position.x && pos.y >= transform.position.y)
        {
            rot = new Vector3(0.0f, 0.0f, 90f + (Mathf.Atan(xDiff / yDiff) * Mathf.Rad2Deg));
        }
        else if (pos.x <= transform.position.x && pos.y < transform.position.y)
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
