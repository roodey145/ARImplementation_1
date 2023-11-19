using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundBlock : MonoBehaviour
{
    public static int X = 0;
    public static int Z = 0;


    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;
    [SerializeField] private InputActionReference[] _clickActions;

    public static GameObject player = null;
    public static bool selected = false;
    public static GameObject demo = null;

    public bool hovered = false;

    private void Start()
    {
        if(player == null)
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

    public void Hover()
    {
        X = GetX();
        Z = GetZ();
        hovered = true;

        //player.transform.position = new Vector3(
        //    transform.position.x,
        //    player.transform.position.y,
        //    transform.position.z);
    }

    public void HoverExit()
    {
        hovered = false;
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

    private void _ClickAction(InputAction.CallbackContext callbackContext)
    {
        if (hovered && callbackContext.performed)
        { // The player clicked on the left/right hand controller's activate button
            if(demo != null)
            { // Place the model at the specified area if there is a demo to place
                demo.GetComponent<BuildingData>().PlaceModel(_x, _z);
                demo = null;
            }
        }
    }

    public TextMeshProUGUI text;
    public void Activate()
    {
        text.text = "Activated";
    }

    public void Deactivate()
    {
        text.text = "Deactivated";
    }

    public void Focus()
    {
        text.text = "Focused";
    }

    public void Blur()
    {
        text.text = "Blur";
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
}
