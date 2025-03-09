using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Player : MonoBehaviour
{
    public string vehicleSaveName = "";
    public RCC_CarControllerV3 carController;

    public RCC_CarControllerV3 CarController
    {
        get
        {
            if (carController == null)
            {
                carController = GetComponent<RCC_CarControllerV3>();
            }
            
            return carController;
        }
    }
}
