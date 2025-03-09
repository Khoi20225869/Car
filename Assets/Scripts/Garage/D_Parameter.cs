using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Parameter
{
    public static int GetMoney()
    {
        return PlayerPrefs.GetInt(D_Setting.Instance.playerPrefsPlayerMoney, D_Setting.Instance.defaultMoney);
    }

    public static void ChangeMoney(int amount)
    {
        PlayerPrefs.SetInt(D_Setting.Instance.playerPrefsPlayerMoney, GetMoney() + amount);
    }

    public static int GetVehicle() 
    {
        UnlockVehicle(D_Setting.Instance.defaultSelectedVehicleIndex); // Mở khóa xe đầu tiên

        int index = PlayerPrefs.GetInt(D_Setting.Instance.playerPrefsPlayerVehicle,
            D_Setting.Instance.defaultSelectedVehicleIndex); // Lấy xe đã được chọn lần cuối , nếu không có lấy xe mặc định 

        if (HasVehicle(index)) // Kiểm tra xe có tồn tại không
        {
            return index;
        }
        else
        {
            PlayerPrefs.SetInt(D_Setting.Instance.playerPrefsPlayerVehicle, D_Setting.Instance.defaultSelectedVehicleIndex); // Reset dữ liệu , đặt về default
            return D_Setting.Instance.defaultSelectedVehicleIndex; 
        }
    }

    public static void SetVehicle(int vehicleIndex)
    {
        PlayerPrefs.SetInt(D_Setting.Instance.playerPrefsPlayerVehicle, vehicleIndex);
    }

    public static bool HasVehicle(int vehicleIndex)
    {
        if (D_Vehicles.Instance.playerVehicles.Length == 0)
        {
            Debug.Log("Empty");
            return false;
        }

        if (vehicleIndex >= D_Vehicles.Instance.playerVehicles.Length)
        {
            Debug.Log("Out of range");
            return false;
        }
        
        RCC_CarControllerV3 vehicle = D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle;
        // Kiểm tra RCC_Controller đã được gắn vào xe chưa 
        if (!vehicle)
        {
            Debug.Log("RCC_ControllerV3 Not Found");
            return false;
        }
        
        // Trong unity có thể gọi component thông qua mot component đã tồn tại trong gameobject đó hoặc gọi thông qua gameobject 
        D_Player player = D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<D_Player>(); 
        // Đảm bảo xe đã được gắn D_Player
        if (!player)
        {
            Debug.Log("D_player Not Found");
            D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle.gameObject.AddComponent<D_Player>();
        }

        return true;
    }

    public static void UnlockVehicle(int vehicleIndex)
    {
        if (HasVehicle(vehicleIndex)) // Nếu xe đã có trong danh sách , lưu trạng thái mở khóa của xe => sử dụng lúc mua xe
        {
            PlayerPrefs.SetInt(
                D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<D_Player>().vehicleSaveName, 1);
        }
    }

    public static void LockVehicle(int vehicleIndex)
    {
        if (HasVehicle(vehicleIndex))
        {
            PlayerPrefs.DeleteKey(D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<D_Player>().vehicleSaveName);
        }
    }
    
    public static void UnLockAllVehicles()
    {
        for (int i = 0; i < D_Vehicles.Instance.playerVehicles.Length; i++)
        {
            if(D_Vehicles.Instance.playerVehicles[i] != null)
                UnlockVehicle(i);
        }
    }

    public static void UnlockAllVehicles()
    {
        for (int i = 0; i < D_Vehicles.Instance.playerVehicles.Length; i++)
        {
            if(D_Vehicles.Instance.playerVehicles[i] != null)
                LockVehicle(i);
        }
    }

    public static bool IsOwnedVehicle(int vehicleIndex)
    {
        if (!HasVehicle(vehicleIndex))
            return false;

        if (PlayerPrefs.HasKey(D_Vehicles.Instance.playerVehicles[vehicleIndex].vehicle.GetComponent<D_Player>()
                .vehicleSaveName))
            return true;
        else
        {
            return false;
        }
    }
    
    
}
