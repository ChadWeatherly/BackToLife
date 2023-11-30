using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpell : MonoBehaviour
{
    public bool isCastingBlackHole = false;

   void Start()
   {
       //isCastingBlackHole = true;

       //Invoke("StopCastingBlackHole", 5f);
   }

   void StopCastingBlackHole()
   {
       isCastingBlackHole = false;
   }

   private void Update()
   {
       if (isCastingBlackHole)
       {
           
           Invoke("StopCastingBlackHole", 2f);
       }
   }

    

    
}
