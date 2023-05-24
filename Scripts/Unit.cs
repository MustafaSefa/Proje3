using System;
using System.Linq;
using UnityEngine;
public class Unit : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material defaultMaterial;
    public Material material;
    void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject);
    }
    private void Update()
    {
        ChangeMaterialAlpha();
    }
    public void ChangeMaterialAlpha()
    {
        if (UnitSelection.unitSelected.Count == 1 && UnitSelection.unitSelected[0] == gameObject)
        {
            skinnedMeshRenderer.material = new Material(material);
            Color materialColor = skinnedMeshRenderer.material.color;
            materialColor.a = 0.35f;
            skinnedMeshRenderer.material.color = materialColor;
        }
        else if (UnitSelection.unitSelected.Count > 1 && UnitSelection.unitSelected.Contains(gameObject))
        {
            skinnedMeshRenderer.material = new Material(material);
            Color materialColor = skinnedMeshRenderer.material.color;
            materialColor.a = 0.35f;
            skinnedMeshRenderer.material.color = materialColor;
        }
        else
        {
            skinnedMeshRenderer.material = new Material(defaultMaterial);
            Color materialColor = skinnedMeshRenderer.material.color;
            materialColor.a = 1f;
            skinnedMeshRenderer.material.color = materialColor;
        }
    }
    private void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }
}

