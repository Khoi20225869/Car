using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ApplyCustomize : MonoBehaviour
{
   [SerializeField] private RCC_CarControllerV3 carController;
   

   public string saveFileName = "";
   public D_SaveCustomizeParameter loadout = new D_SaveCustomizeParameter();

   private void OnEnable()
   {
      
   }
}
