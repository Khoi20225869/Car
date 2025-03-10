using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chứa method/function phục vụ cho RCC
public class D_Customize : MonoBehaviour
{
    public static void ChangeWheels(RCC_CarControllerV3 vehicle, GameObject wheel, bool applyRadius)
    {
        if (!CheckVehicle(vehicle))
            return;

        for (int i = 0; i < vehicle.AllWheelColliders.Length; i++)
        {
            if (vehicle.AllWheelColliders[i].wheelModel.GetComponent<MeshRenderer>())
                vehicle.AllWheelColliders[i].wheelModel.GetComponent<MeshRenderer>().enabled = false;
           
            foreach(Transform t in vehicle.AllWheelColliders[i].wheelModel.GetComponentInChildren<Transform>())
                t.gameObject.SetActive(false);

            GameObject newWheel = Instantiate(wheel, vehicle.AllWheelColliders[i].wheelModel.position,
                vehicle.AllWheelColliders[i].wheelModel.rotation, vehicle.AllWheelColliders[i].wheelModel);

            if (vehicle.AllWheelColliders[i].wheelModel.localPosition.x > 0f)
                newWheel.transform.localScale = new Vector3(newWheel.transform.localScale.x * -1f,
                    newWheel.transform.localScale.y, newWheel.transform.localScale.z);
            
            if(applyRadius)
                vehicle.AllWheelColliders[i].WheelCollider.radius = RCC_GetBounds.MaxBoundsExtent(wheel.transform);
        }
    }

    public static bool CheckVehicle(RCC_CarControllerV3 vehicle)
    {
        if (!vehicle)
            return false;

        return true;
    }
}
