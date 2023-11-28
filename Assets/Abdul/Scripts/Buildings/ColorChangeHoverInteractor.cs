using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeHoverInteractor : MonoBehaviour
{
    [SerializeField] private Color _hoveredColor = Color.red;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private List<Action> _hoverCallbacks = new List<Action>();
    [SerializeField] private List<Action> _hoverExitCallbacks = new List<Action>();
    private Dictionary<Material, Color> originalMatsColor = new Dictionary<Material, Color>();
    internal bool hovered = false;

    enum ProcessMethod
    {
        Hover,
        HoverExit,
        AssignOriginalColors,
    }

    // Start is called before the first frame update
    void Awake()
    {
        // Get the mesh renderer on awake incase they are disabled later on.
        _meshRenderers = _GetMeshRenderers(); 
        _ProcessMaterials(ProcessMethod.AssignOriginalColors);
    }

    private MeshRenderer[] _GetMeshRenderers()
    {
        MeshRenderer[] unfilteredRenderers = GetComponentsInChildren<MeshRenderer>(); 
        List<MeshRenderer> filtteredRenderers = new List<MeshRenderer>();

        for(int i = 0; i < unfilteredRenderers.Length; i++)
        {
            if (unfilteredRenderers[i].material.HasProperty("_EmissionColor"))
            {
                filtteredRenderers.Add(unfilteredRenderers[i]);
            }
        }

        return filtteredRenderers.ToArray();
    }

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

    public void Hover()
    {
        hovered = true;
        _ProcessMaterials(ProcessMethod.Hover);

        // Notify other registed components
        for(int i = 0; i < _hoverCallbacks.Count; i++)
        {
            _hoverCallbacks[i]();
        }
    }

    public void HoverExit()
    {
        hovered = false;
        _ProcessMaterials(ProcessMethod.HoverExit);

        // Notify other registed components
        for (int i = 0; i < _hoverExitCallbacks.Count; i++)
        {
            _hoverExitCallbacks[i]();
        }
    }

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
                    case ProcessMethod.AssignOriginalColors:
                        mat.EnableKeyword("_EMISSION");
                        originalMatsColor.Add(mat, mat.GetColor("_EmissionColor"));
                        break;
                }
            }
        }
    }

}
