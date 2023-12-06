using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarracksMenuInteractor : MonoBehaviour
{
    [SerializeField] private InputActionReference _toggleAction;
    [SerializeField] private InteractableBuilding _buildingData;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_toggleAction != null) _toggleAction.action.performed += _ToggleMenu;
        _buildingData = GetComponentInParent<InteractableBuilding>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Reverse the activation state of the barrack menu.
    /// </summary>
    /// <param name="context">Toggle action context</param>
    private void _ToggleMenu(InputAction.CallbackContext context)
    {
        if(!context.canceled && _buildingData.hovered)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }

}
