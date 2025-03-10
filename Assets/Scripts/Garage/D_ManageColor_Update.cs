using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class D_ManageColor_Update : MonoBehaviour
{
    public D_ApplyCustomize Apply;

    public D_UpgradeColor[] Hold;

    public void Initialize()
    {
        if (Hold == null)
            return;
        
        if(Apply.loadout.paint != new Color(1f,1f, 1f,0f))
            Paint(Apply.loadout.paint);
    }

    public void Paint(Color newColor)
    {
        for (int i = 0; i < Hold.Length; i++)
            Hold[i].UpdateColor(newColor);
    }
}
