/* Broc Edson
 * Liam Barrett
 * Spirit Shift
 * Moves an object forward and then destroys it
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float moveForce;
    public float speed;
    public float destroyDelay;
    private Rigidbody2D rb2d;
    private PlayerHealth playerHealthScript;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterDelay());
        playerHealthScript = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rb2d.velocity.magnitude) < speed)
        {
            rb2d.AddForce(transform.right * moveForce * Time.deltaTime);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
        yield return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!rb2d.CompareTag("PlayerBullet") && (other.CompareTag("Player") || other.CompareTag("Player Inactive")))
        {
            Debug.Log("Shot Player");
            playerHealthScript.TakeDamage();
        }
        else if (rb2d.CompareTag("PlayerBullet") && !other.GetComponent<BasicMovement>() && !other.CompareTag("Player Inactive"))
        {
            Debug.Log("Shot Enemy");
            Destroy(other.gameObject);
        }
    }
}
