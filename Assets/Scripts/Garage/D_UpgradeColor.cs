using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpgradeColor : MonoBehaviour
{
    [SerializeField] private D_ApplyCustomize Apply;

    public MeshRenderer[] bodyRenderer;
    public int index = 0;

    public void UpdateColor(Color newColor)
    {
        if (bodyRenderer.Length == 0)
            return;
        foreach (MeshRenderer meshRenderer in bodyRenderer)
            meshRenderer.materials[index].color = newColor;
        Apply.loadout.paint = newColor;
        Apply.Save();
    }
}
