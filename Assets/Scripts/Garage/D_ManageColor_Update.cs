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
        if (Apply.loadout.paint != new Color(1f, 1f, 1f, 0f))
        {
            for (int i = 0; i < Hold.Length; i++)
                Hold[i].UpdateColor(Apply.loadout.paint);
        }
            
    }
    
    public void Paint(int index)
    {
        Color newColor = new Color();
        if (index == 1)
        {
            Debug.Log("kk");
            newColor = new Color(1f, 0.65f, 0f);
        }
        else if (index == 2)
        {
            newColor = Color.white;
        }
        else if (index == 3)
        {
            newColor = Color.black; 
        }
        else if (index == 4)
        {
            newColor = Color.red; 
        }
        else if (index == 5)
        {
            newColor = Color.blue;
        }
        else if (index == 6)
        {
            newColor = Color.cyan;
        }
        else if (index == 7)
        {
            newColor = Color.magenta;
        }
        else if (index == 8)
        {
            newColor = new Color(1f, 0.75f, 0.8f);
        }
        else if (index == 9)
        {
            newColor = Color.green;
        }
        for (int i = 0; i < Hold.Length; i++)
        {
            Hold[i].UpdateColor(newColor);
        }
    }
}
