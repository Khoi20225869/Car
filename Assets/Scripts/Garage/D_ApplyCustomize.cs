using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ApplyCustomize : MonoBehaviour
{
   public RCC_CarControllerV3 carController;
   

   public string saveFileName = "";
   public D_SaveCustomizeParameter loadout = new D_SaveCustomizeParameter();

   private void OnEnable()
   {
      
   }

   public void Save()
   {
      PlayerPrefs.SetString(saveFileName, JsonUtility.ToJson(loadout));
   }

   public void Load()
   {
      loadout = new D_SaveCustomizeParameter();
      
      if(PlayerPrefs.HasKey(saveFileName))
         loadout = (D_SaveCustomizeParameter)
      JsonUtility.FromJson(PlayerPrefs.GetString(saveFileName), typeof(D_SaveCustomizeParameter));
   }

   private void Reset()
   {
      saveFileName = transform.name;
   }
}
