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
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) showBlueTeam();
        if (Input.GetKeyDown(KeyCode.R)) showRedTeam();
        if (Input.GetKeyDown(KeyCode.G)) showBothTeams();
    }

    #region helper methods

    /// Methods to choose which team(s) to show
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

    /// Methods to show/hide specific layers
    void showLayer(int layer)
    {
        cam.cullingMask |= 1 << layer;
    }

    void showLayer(string layer)
    {
        showLayer(LayerMask.NameToLayer(layer));
    }

    void hideLayer(int layer)
    {
        cam.cullingMask &= ~(1 << layer);
    }

    void hideLayer(string layer)
    {
        hideLayer(LayerMask.NameToLayer(layer));
    }
    #endregion
}