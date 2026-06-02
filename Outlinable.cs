using System.Collections.Generic;
using UnityEngine;

// this also requires that the gameobject has a collider, since the collider
// is what the mouse events will be detected on
[RequireComponent(typeof(MeshRenderer))]
public class Outlinable : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;

    private MeshRenderer meshRenderer;

    private readonly List<Material> materials = new();

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.GetMaterials(materials);
    }

    private void OnMouseEnter()
    {
        if (!enabled) return;

        materials   .Add         (outlineMaterial);
        meshRenderer.SetMaterials(materials      );
    }

    public void OnMouseExit()
    {
        if (!enabled) return;
        
        materials   .Remove      (outlineMaterial);
        meshRenderer.SetMaterials(materials      );
    }
}
