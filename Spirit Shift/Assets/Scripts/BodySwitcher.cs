/*
 * Anthony Wessel
 * Wolfgang Gross
 * Project 1 (Spirit Shift)
 * 
 * Swaps player control to different enemies and hides enemies
 * based on which team the player controlled enemy is on
 */

using UnityEngine;
using Cinemachine;

public class BodySwitcher : MonoBehaviour
{
    SwitchBehavior SBehavior;
    BasicMovement player;
    boundary playerHusk;
    public AudioSource playerAudio;
    public AudioClip switchSound;
    private CinemachineVirtualCamera followCamera;

    void Start()
    {
        
        player = FindObjectOfType<BasicMovement>();
        playerHusk = FindObjectOfType<boundary>();
        followCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Update()
    {
        //SBehavior.gameObject.GetComponent<>;

        //SBehavior.GetComponent<SwitchBehavior>().getBehavior();
        
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
                SBehavior = hitInfo.collider.GetComponent<SwitchBehavior>(); //Checks if is switchable
                //followPlayer isEnemy = hitInfo.collider.GetComponent<followPlayer>(); //Only works on enemies
                //boundary isPlayer = hitInfo.collider.GetComponent<boundary>(); //Only works on player

                // If the hit object is switchable 
                if (SBehavior != null) 
                {
                    //prep: set player object tag to player inactive
                    Debug.Log("Set player husk to inactive");

                    //set husk tag to inactive
                    playerHusk.gameObject.tag = "Player Inactive";
                    
                    //reference SBehavior to change the objects behavior to 3 (inactive)
                    playerHusk.GetComponent<SwitchBehavior>().setBehavior(3);
                    
                    //Check that you correctly set behavior
                    Debug.Log("Current Behavior: " + SBehavior.GetComponent<SwitchBehavior>().getBehavior());

                    //If current is player
                    if (SBehavior.GetComponent<SwitchBehavior>().getBehavior() == 3)
                    {
                        //currentBehavior = 3 //(player inactive)
                        Debug.Log("'this' -> means the player husk!");

                        Debug.Log("Running Switch_bodies");
                        switchBodies(hitInfo.collider.gameObject);

                        //currentBehavior = 2 //(player active)
                        playerHusk.GetComponent<SwitchBehavior>().setBehavior(2);
                    }

                    //If current is enemy
                    if (SBehavior.GetComponent<SwitchBehavior>().getBehavior() == 1)
                    {
                        //currentBehavior = 1 //(enemy)
                        Debug.Log("'this' -> means the enemy!");

                        SBehavior.GetComponent<SwitchBehavior>().setBehavior(2);
                        Debug.Log("New Behavior: " + SBehavior.GetComponent<SwitchBehavior>().getBehavior());

                        Debug.Log("Running Switch_bodies");
                        switchBodies(hitInfo.collider.gameObject);

                        //currentBehavior = 2 //(player active)
                        //playerHusk.GetComponent<SwitchBehavior>().setBehavior(2);
                        
                    }

                    // Switch to the other body
                    //Debug.Log("Running Switch_bodies");
                    //switchBodies(hitInfo.collider.gameObject);

                }
                //Elif the hit object is the inactive player body
                //else /*if (isPlayer != null)*/
                //{
                 //   //set player object tag to player active
                 //   Debug.Log("Set player to active");
                 //   playerHusk.gameObject.tag = "Player";

                    // Switch to the other body
  //                  Debug.Log("Running Switchbodies");
    //                switchBodies(hitInfo.collider.gameObject);
      //          }
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) showBlueTeam();
        if (Input.GetKeyDown(KeyCode.R)) showRedTeam();
        if (Input.GetKeyDown(KeyCode.G)) showBothTeams();
    }

    void switchBodies(GameObject newBody)
    {
        FindObjectOfType<BasicMovement>().GetComponent<SwitchBehavior>().setBehavior(1);

        playerAudio.PlayOneShot(switchSound);
        // Add BasicMovement script to clicked enemy //Note: can't reattach to player husk for some reason
        BasicMovement newPlayer = newBody.AddComponent<BasicMovement>();

        // Destroy old BasicMovement script
        Destroy(player);

        // Get reference to new BasicMovement script
        player = newPlayer;

        // Set camera to follow new body
        //NullReferenceException: Object Reference not set to an instance of an object
        followCamera.Follow = player.transform;
        
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