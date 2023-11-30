using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

enum ProcessMethod
{
    Hover,
    HoverExit,
    RetrieveInitialColor,
}

[RequireComponent(typeof(XRSimpleInteractable))]
public class InteractableBuilding : BuildingData
{
    #region Fields
    [Header("Actions Setting")]
    [SerializeField] private InputActionReference _clickAction;
    [SerializeField] private InputActionReference _selectAction;

    [Header("Hover Setting")]
    [SerializeField] protected Color _hoveredColor = Color.red;
    internal bool hovered = false;
    private MeshRenderer[] _meshRenderers;
    private Dictionary<Material, Color> originalMatsColor = new Dictionary<Material, Color>();
    private XRSimpleInteractable _xrInteractable;

    [Header("External-Callbacks Setting")]
    [SerializeField] private List<Action> _hoverCallbacks = new List<Action>();
    [SerializeField] private List<Action> _hoverExitCallbacks = new List<Action>();
    #endregion

    #region Hover callback getters and setters
    internal void RegisterHoverCallback(Action callback)
    {
        _hoverCallbacks.Add(callback);
    }

    internal void UnregisterHoverCallback(Action callback)
    {
        _hoverCallbacks.Remove(callback);
    }

    internal void RegisterHoverExitCallback(Action callback)
    {
        _hoverExitCallbacks.Add(callback);
    }

    internal void UnregisterHoverExitCallback(Action callback)
    {
        _hoverExitCallbacks.Remove(callback);
    }
    #endregion

    #region MonoBehaviour Related Methods
    protected new void Awake()
    {
        base.Awake();


        // Make sure to register a call back when hover enter and exit in the xr interactable
        _xrInteractable = GetComponent<XRSimpleInteractable>();
        _xrInteractable.hoverEntered.AddListener( (HoverEnterEventArgs call) => HoverEnter() );
        _xrInteractable.hoverExited.AddListener( (HoverExitEventArgs call) => HoverExit() );


        // Get the mesh renderer on awake incase they are disabled later on.
        _meshRenderers = _GetMeshRenderers();
        // Assign the initial colors of this GameObject materials.
        _ProcessMaterials(ProcessMethod.RetrieveInitialColor);
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // Register the click event
        if(_clickAction != null) _clickAction.action.performed += _Click;

        // Register the activate event
        if (_selectAction != null) _selectAction.action.performed += _Select;
    }
    #endregion

    #region Events: Click, Select, Hover, HoverExit
    private void _Click(InputAction.CallbackContext context)
    {
        if(!context.canceled && hovered)
        {
            Clicked();
        }
    }

    /// <summary>
    /// Is called when the player clicks the button while hovering over this GameObject.
    /// </summary>
    protected virtual void Clicked() { }

    private void _Select(InputAction.CallbackContext context)
    {
        if(!context.canceled && hovered)
        {
            if(isDemo()) SelectedDemo();
            else         Selected();
        }
    }

    /// <summary>
    /// If the building is NOT demo and the player hovered over the building on selected it, this method will be called
    /// </summary>
    protected virtual void Selected() { }

    /// <summary>
    /// If the building is a demo and the player hovered over the building on selected it, this method will be called
    /// </summary>
    protected virtual void SelectedDemo() { }

    public void HoverEnter()
    {
        hovered = true;
        
        // Change the GameObject color to indicate it has been hovered on.
        _ProcessMaterials(ProcessMethod.Hover);

        // Notify other registed components
        for (int i = 0; i < _hoverCallbacks.Count; i++)
        {
            _hoverCallbacks[i]();
        }
    }

    public void HoverExit()
    {
        hovered = false;

        // Return the GameObject color to its origianl color to indicate the hover has exited.
        _ProcessMaterials(ProcessMethod.HoverExit);

        // Notify other registed components
        for (int i = 0; i < _hoverExitCallbacks.Count; i++)
        {
            _hoverExitCallbacks[i]();
        }
    }
    #endregion

    /// <summary>
    /// Gets the Mesh renderers of this gameobejct children.
    /// It ignores the materials that do not have the _EmissionColor property.
    /// </summary>
    /// <returns>The MeshRenderers of this gameobject children.</returns>
    private MeshRenderer[] _GetMeshRenderers()
    {
        MeshRenderer[] unfilteredRenderers = GetComponentsInChildren<MeshRenderer>();
        List<MeshRenderer> filtteredRenderers = new List<MeshRenderer>();

        for (int i = 0; i < unfilteredRenderers.Length; i++)
        {
            if (unfilteredRenderers[i].material.HasProperty("_EmissionColor"))
            {
                filtteredRenderers.Add(unfilteredRenderers[i]);
            }
        }

        return filtteredRenderers.ToArray();
    }

    /// <summary>
    /// Process the materials of this GameObject by either changing their color when hovered, 
    /// returning their color to the origian color in case the hover exited, or retrieve the
    /// initial color of the materials on awake.
    /// </summary>
    /// <param name="method">The processing method: Hover, HoverExit, RetrieveInitialColor.</param>
    private void _ProcessMaterials(ProcessMethod method)
    {
        Material[] materials;
        foreach (MeshRenderer renderer in _meshRenderers)
        {
            materials = renderer.materials;
            foreach (Material mat in materials)
            {
                switch (method)
                {
                    case ProcessMethod.Hover:
                        mat.SetColor("_EmissionColor", _hoveredColor);
                        break;
                    case ProcessMethod.HoverExit:
                        Color color;
                        originalMatsColor.TryGetValue(mat, out color);
                        mat.SetColor("_EmissionColor", color);
                        break;
                    case ProcessMethod.RetrieveInitialColor:
                        mat.EnableKeyword("_EMISSION");
                        originalMatsColor.Add(mat, mat.GetColor("_EmissionColor"));
                        break;
                }
            }
        }
    }
}
