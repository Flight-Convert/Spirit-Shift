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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = (player.transform.position);

        if (Mathf.Abs(rb2d.velocity.magnitude) <= speed)
        {
            rb2d.AddForce(findDirectionFromPos(playerPos) * force * Time.deltaTime);
        }
    }

    Vector2 findDirectionFromPos(Vector2 pos)
    {
        Vector2 direction = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);

        direction = new Vector2(direction.normalized.x, direction.normalized.y);

        return direction;
    }
}
