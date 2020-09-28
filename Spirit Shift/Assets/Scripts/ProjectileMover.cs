/* Broc Edson
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

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterDelay());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player Inactive"))
        {
            Debug.Log("Shot Player");
        }
    }
}
