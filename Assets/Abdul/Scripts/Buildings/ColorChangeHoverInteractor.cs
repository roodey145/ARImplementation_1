using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeHoverInteractor : MonoBehaviour
{
    [SerializeField] private Color _hoveredColor = Color.red;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    private Dictionary<Material, Color> originalMatsColor = new Dictionary<Material, Color>();
    internal bool hovered = false;

    enum ProcessMethod
    {
        Hover,
        HoverExit,
        AssignOriginalColors,
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _ProcessMaterials(ProcessMethod.AssignOriginalColors);
    }

    public void Hover()
    {
        hovered = true;
        _ProcessMaterials(ProcessMethod.Hover);
    }

    public void HoverExit()
    {
        hovered = false;
        _ProcessMaterials(ProcessMethod.HoverExit);
    }

    private void _ProcessMaterials(ProcessMethod method)
    {
        Material[] materials;
        foreach (MeshRenderer renderer in _meshRenderers)
        {
            materials = renderer.materials;
            foreach (Material mat in materials)
            {
                switch(method)
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
