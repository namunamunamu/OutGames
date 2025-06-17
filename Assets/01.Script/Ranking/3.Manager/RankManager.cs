using System;
using UnityEngine;

public class RankManager : MonoBehaviour
{
   public static RankManager Instance;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
      }
   }
   
   
   
}
