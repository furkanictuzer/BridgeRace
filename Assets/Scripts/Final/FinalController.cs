using System.Collections;
using System.Collections.Generic;
using ChemicalMatch.Controllers;
using UnityEngine;

public class FinalController : MonoSingleton<FinalController>
{
   [SerializeField] private List<Transform> placePos = new List<Transform>();

   public void SetOrderInPlace(Transform first, Transform second, Transform third)
   {
      first.position = placePos[0].position;
      second.position = placePos[1].position;
      third.position = placePos[2].position;
      
   }
}
