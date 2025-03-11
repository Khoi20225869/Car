using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lưu các thông số customize của xe => load dữ liệu 
[System.Serializable]
public class D_SaveCustomizeParameter
{
    public Color paint = new Color(1f, 1f, 1f, 0f);
    public int wheel = -1;

    public int engine = 0;
    public int handling = 0;
    public int speed = 0;
}
