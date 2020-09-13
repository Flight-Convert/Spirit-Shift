/*
 * Anthony Wessel
 * Project 1 (Spririt Shift)
 * 
 * Swaps player control to different enemies and hides enemies
 * based on which team player controlled enemy is on
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySwitcher : MonoBehaviour
{
    BasicMovement player;

    void Start()
    {
        player = FindObjectOfType<BasicMovement>();
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
                // If the hit object has an enemy script or is the player body
                followPlayer fp = hitInfo.collider.GetComponent<followPlayer>();
                if (fp != null || hitInfo.collider.CompareTag("Player"))
                {
                    // Switch to that body
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
        // Add BasicMovement script to clicked enemy
        BasicMovement newPlayer = newBody.AddComponent<BasicMovement>();

        // Destroy old BasicMovement script
        Destroy(player);

        // Get reference to new BasicMovement script
        player = newPlayer;
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