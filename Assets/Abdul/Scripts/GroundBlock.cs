using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshRenderer))]
public class GroundBlock : MonoBehaviour
{
    public static int X = 0;
    public static int Z = 0;

    [SerializeField] private BuildingData _building = null; // Not Occupied / Empty Area
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;
    [SerializeField] private InputActionReference[] _clickActions;

    // Mesh Renderer 
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _occupiedAreaColor = Color.red;
    [SerializeField] private Color _unoccupiedAreaColor = Color.green;
    private MeshRenderer _meshRenderer;
    private static List<GroundBlock> _occupiedGroundBlocks = new List<GroundBlock>();


    public static GameObject player = null;
    public static bool selected = false;
    public static GameObject demo = null;

    public bool hovered = false;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.GetColor("_EmissionColor");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Select();
        }

        if(_clickActions != null)
        {
            for(int i = 0; i < _clickActions.Length; i++)
            {
                _clickActions[i].action.performed += _ClickAction;
            }
        }
    }

    // Setter
    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;
    }

    /// <summary>
    /// Set a building on top of this ground block.
    /// </summary>
    /// <param name="building">The Building Data component</param>
    public void SetBuilding(BuildingData building)
    {
        _building = building;
    }

    public void Hover()
    {
        // Make sure to reset the colors. 
        // This method must be called here to ensure that the calling chain is conistent.
        // Calling it in the HoverExit can cause problems, since the Hover can sometimes
            // be called before the HoverExit of the previous block, thus, causing the color
            // to reset to default immeditely.
        _ResetColors();

        X = GetX();
        Z = GetZ();
        hovered = true;


        //print($"({_x}, {_z}) Hovered");

        // Move the demo
        if(demo != null)
        {
            demo.GetComponent<BuildingData>().MoveDemo(X, Z);
        }

    }

    public void HoverExit()
    {
        hovered = false; // This might be 
        //print($"({_x}, {_z}) Un-Hovered");

        //_ResetColors();
    }

    private static void _ResetColors()
    {
        // Reset and clear the color of the ground blocks
        //if (demo != null)
        //{
            for (int i = 0; i < _occupiedGroundBlocks.Count; i++)
            {
                _occupiedGroundBlocks[i].ResetColor();
            }
            _occupiedGroundBlocks.Clear();
        //}
    }

    private void _ClickAction(InputAction.CallbackContext callbackContext)
    {
        if (hovered && callbackContext.performed)
        { // The player clicked on the left/right hand controller's activate button

            //print($"My: ({_x}, {_z}), Ground: ({X}, {Z})");

            if (demo != null)
            { // Place the model at the specified area if there is a demo to place
                demo.GetComponent<BuildingData>().PlaceModel(X, Z);
                //demo = null;
            }
        }
    }

    public void Select()
    {

        selected = true;

    }

    public void SelectExit()
    {
        selected = false;

        // Teleport the player to the center of this cell.
        player.transform.position = new Vector3(
                transform.position.x,
                player.transform.position.y,
                transform.position.z);
    }

    // Getter
    public int GetX()
    {
        return _x;
    }

    public int GetZ()
    {
        return _z;
    }

    internal bool IsOccupied()
    {
        return _building != null;
    }

    internal void IndicateOccupiedGround()
    {
        _meshRenderer.material.SetColor("_EmissionColor", _occupiedAreaColor);
        _occupiedGroundBlocks.Add(this);
    }

    internal void IndicateUnoccupiedGround()
    {
        _meshRenderer.material.SetColor("_EmissionColor", _unoccupiedAreaColor);
        _occupiedGroundBlocks.Add(this);
    }

    internal void ResetColor()
    {
        _meshRenderer.material.SetColor("_EmissionColor", _originalColor);
        //_occupiedGroundBlocks.Remove(this);
    }

    
}
