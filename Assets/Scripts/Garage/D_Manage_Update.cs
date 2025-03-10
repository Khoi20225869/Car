using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Manage_Update : MonoBehaviour
{
    public D_ApplyCustomize vehicle;

    private void OnEnable()
    {
        RCC_SceneManager.OnVehicleChanged += D_SceneManager_OnVehicleChanged;
    }
    
    private void D_SceneManager_OnVehicleChanged() {

        vehicle = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<D_ApplyCustomize>();

    }

    public void ChangeWheels(int wheelIndex)
    {
        if (!vehicle)
            return;

        vehicle.wheelManage.UpdateWheel(wheelIndex);
    }

    private void OnDisable()
    {
        RCC_SceneManager.OnVehicleChanged -= D_SceneManager_OnVehicleChanged;
    }
}
