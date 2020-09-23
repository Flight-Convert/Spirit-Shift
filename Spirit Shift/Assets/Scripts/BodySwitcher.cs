/*
 * Anthony Wessel
 * Project 1 (Spirit Shift)
 * 
 * Swaps player control to different enemies and hides enemies
 * based on which team the player controlled enemy is on
 */

using UnityEngine;

public class BodySwitcher : MonoBehaviour
{
    BasicMovement player;
    boundary playerObject;
    public AudioSource playerAudio;
    public AudioClip switchSound;

    void Start()
    {
        player = FindObjectOfType<BasicMovement>();
        playerObject = FindObjectOfType<boundary>();
    }

    void Update()
    {
        // If right mouse button is clicked
        if (Input.GetMouseButtonDown(1))
        {
            // Perform a raycast to see what we clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo;
            hitInfo = Physics2D.GetRayIntersection(ray);

            // If we actually clicked on something
            if (hitInfo.collider != null)
            {
                //reference for hit object
                followPlayer fp = hitInfo.collider.GetComponent<followPlayer>();

                // If the hit object has an enemy script or is the player body
                if (fp != null || hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Player Inactive"))
                {
                    //If hit object has an enemy script
                    if (fp != null)
                    {
                        //set player object tag to player inactive
                        playerObject.gameObject.tag = "Player Inactive";
                    }
                    
                    //else If the hit object is the inactive player 
                    else if (hitInfo.collider.CompareTag("Player Inactive"))
                    {
                        //Set player object tag to default player
                        playerObject.gameObject.tag = "Player";
                    }

                    // Switch to the other body
                    switchBodies(hitInfo.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) showBlueTeam();
        if (Input.GetKeyDown(KeyCode.R)) showRedTeam();
        if (Input.GetKeyDown(KeyCode.G)) showBothTeams();
    }

    void switchBodies(GameObject newBody)
    {
        playerAudio.PlayOneShot(switchSound);
        // Add BasicMovement script to clicked enemy
        BasicMovement newPlayer = newBody.AddComponent<BasicMovement>();

        // Destroy old BasicMovement script
        Destroy(player);

        // Get reference to new BasicMovement script
        player = newPlayer;
        
        // Stop the new bodies current movement
        Rigidbody2D rb2d = newBody.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = new Vector2(0, 0);
        }

        if (newBody.layer == LayerMask.NameToLayer("RedTeam"))
            showRedTeam();
        else if (newBody.layer == LayerMask.NameToLayer("BlueTeam"))
            showBlueTeam();
        else
            showBothTeams();
    }

    #region Show / Hide teams

    void showBlueTeam()
    {
        showLayer("BlueTeam");
        hideLayer("RedTeam");
    }

    void showRedTeam()
    {
        hideLayer("BlueTeam");
        showLayer("RedTeam");
    }

    void showBothTeams()
    {
        showLayer("BlueTeam");
        showLayer("RedTeam");
    }

    void hideBothTeams()
    {
        hideLayer("BlueTeam");
        hideLayer("RedTeam");
    }

    #endregion

    #region helper methods

    /// Methods to show/hide specific layers
    void showLayer(int layer)
    {
        Camera.main.cullingMask |= 1 << layer;
    }

    void showLayer(string layer)
    {
        showLayer(LayerMask.NameToLayer(layer));
    }

    void hideLayer(int layer)
    {
        Camera.main.cullingMask &= ~(1 << layer);
    }

    void hideLayer(string layer)
    {
        hideLayer(LayerMask.NameToLayer(layer));
    }

    #endregion
}