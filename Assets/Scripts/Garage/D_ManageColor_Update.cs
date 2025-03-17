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
            newColor = new Color(1f, 1f, 1f);  // ⚪ White
        }
        else if (index == 2)
        {
            newColor = new Color(0f, 0f, 1f);  // 🔵 Blue
        }
        else if (index == 3)
        {
            newColor = new Color(0f, 1f, 0f);  // 🟢 Green
        }
        else if (index == 4)
        {
            newColor = new Color(1f, 0.65f, 0f);  // 🟧 Orange
        }
        else if (index == 5)
        {
            newColor = new Color(1f, 0.75f, 0.8f);  // 🌸 Pink
        }
        else if (index == 6)
        {
            newColor = new Color(0.5f, 0f, 0.5f);  // 🟣 Purple
        }
        else if (index == 7)
        {
            newColor = new Color(1f, 0f, 0f);  // 🔴 Red
        }
        else if (index == 8)
        {
            newColor = new Color(0.56f, 0f, 1f);  // 🟪 Violet
        }
        else if (index == 9)
        {
            newColor = new Color(1f, 1f, 0f);  // 🟡 Yellow
        }

        for (int i = 0; i < Hold.Length; i++)
        {
            Hold[i].UpdateColor(newColor);
        }
    }
}
