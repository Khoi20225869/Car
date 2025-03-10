using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpgradeWheel : MonoBehaviour
{
    [SerializeField] private D_ApplyCustomize Apply;

    public void Initialize()
    {
        int wheelIndex = Apply.loadout.wheel;

        if (wheelIndex != -1)
            D_Customize.ChangeWheels(Apply.carController, D_HoldWheel.Instance.wheels[wheelIndex].wheel, true);
    }

    public void UpdateWheel(int wheelIndex)
    {
        Apply.loadout.wheel = wheelIndex;
        Apply.Save();
        D_Customize.ChangeWheels(Apply.carController, D_HoldWheel.Instance.wheels[wheelIndex].wheel, true);
    }
}
