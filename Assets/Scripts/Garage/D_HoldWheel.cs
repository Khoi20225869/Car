using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lưu list bánh xe 

[CreateAssetMenu]
public class D_HoldWheel : ScriptableObject
{
   private static D_HoldWheel instance;

   public static D_HoldWheel Instance
   {
      get
      {
         if (instance == null)
            instance = Resources.Load<D_HoldWheel>("D_HoldWheel") 
               as D_HoldWheel;

            return instance;
      }
   }

   [System.Serializable]

   public class ChangableWheels
   {
      public GameObject wheel;
   }

   public ChangableWheels[] wheels;
}
