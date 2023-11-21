using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateableBuilding : BuildingData
{
    [SerializeField] private InputActionReference[] _rotationActions;
    [SerializeField] private int _rotationAmount = 90;
    private int _rotation = 0;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        for(int i = 0; i < _rotationActions.Length; i++)
        {
            _rotationActions[i].action.performed += _Rotate;
        }
    }


    protected void _Rotate(InputAction.CallbackContext context)
    {
        if(!context.canceled)
        {
            Vector2 value = context.ReadValue<Vector2>();
            if (value.y < 0) 
                _rotation = _rotationAmount;
            else 
                _rotation = _rotationAmount;

            transform.Rotate(new Vector3(0, _rotation, 0), Space.World);
        }
    }

    protected override void _OverrideModelData(GameObject gameObject)
    {
        base._OverrideModelData(gameObject);
        gameObject.transform.rotation = transform.rotation;
    }
}
