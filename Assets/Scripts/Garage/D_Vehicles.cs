using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class D_Vehicles : ScriptableObject
{
    private static D_Vehicles instance;

    
    public static D_Vehicles Instance
    {
        get
        {
            if(instance == null)
                instance= Resources.Load<D_Vehicles>("D_Vehicles")
                    as D_Vehicles;
            
            return instance;
        }
    }

    [System.Serializable]
    public class PlayerVehicle
    {
        public bool initialized = false; // Check xe được cấu hình chưa 
        public RCC_CarControllerV3 vehicle; // Prefab được liên kết với đối tượng này
        [Min(0)] public int price = 0;      // Giá tiền của xe , cần đủ tiền để mo khóa 
        // Thông số ban đầu xe
        [Space()] [Range(0f, 20f)] public float acceleration = .1f;
        [Range(0f, 20f)] public float brake = .1f;
        [Range(100f, 1000f)] public float speed = 100f;
        [Range(0f, 20f)] public float handling = .1f;
        // Hệ số update
        [Space()] [Range(1f, 2f)] public float upgradedAccelerationEfficiency = 1.2f;
        [Range(1f,2f)] public float upgradedBrakeEfficiency = 1.2f;
        [Range(1f,2f)] public float upgradedHangdlingEfficiency = 1.2f;
    }
    
    public PlayerVehicle[] playerVehicles;
    

    public void AddNewVehicle(PlayerVehicle newVehicle)
    {
        List<PlayerVehicle> currentVehicles = new List<PlayerVehicle>(); // Tạo một List động 
        currentVehicles = playerVehicles.ToList(); // Chuyển mảng xe thành list động => gán vào list vừa tạo
        currentVehicles.Add(newVehicle); // Thêm phần tử vào list động
        playerVehicles = currentVehicles.ToArray();  // Gán lại giá trị của cho mảng 
    }
}
