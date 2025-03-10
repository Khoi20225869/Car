using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ManageCustomize : MonoBehaviour
{
    public D_ApplyCustomize Apply;

    private void OnEnable()
    {
        RCC_SceneManager.OnVehicleChanged += D_SceneManager_OnVehicleChanged;
    }
    
    private void D_SceneManager_OnVehicleChanged() {

        Apply = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<D_ApplyCustomize>();

    }

    public void ChangeWheels(int wheelIndex)
    {
        if (!Apply)
            return;

        Apply.upgradeWheel.UpdateWheel(wheelIndex);
    }

    public void Paint(Color color)
    {
        if (!Apply)
            return;
        
        Apply.colorManage.Paint(color);
    }
    private void OnDisable()
    {
        RCC_SceneManager.OnVehicleChanged -= D_SceneManager_OnVehicleChanged;
    }
}
