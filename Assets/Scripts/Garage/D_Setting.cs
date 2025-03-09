using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class D_Setting : ScriptableObject
{
    private static D_Setting instance;

    public static D_Setting Instance
    {
        get
        {
            if(instance == null)
                instance = Resources.Load<D_Setting>("D_Setting")
                    as D_Setting;
            
            return instance;
        }
    }

    [Min(0)] public int mainMenuSceneIndex = 0;

    [Header("Default Settings")] [Min(0)] public int defaultMoney = 10000;

    [Min(0)] public int defaultSelectedVehicleIndex = 0;
    
    [Header("PlayerPrefs")] public string playerPrefsLastSelectedVehicleIndex = "LastSelectedVehicleIndex";
    public string playerPrefsPlayerMoney = "Money";
    public string playerPrefsPlayerVehicle = "Vehicle";
    
}
