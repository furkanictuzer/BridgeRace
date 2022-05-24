using System;
using System.Collections;
using System.Collections.Generic;
using ChemicalMatch.Controllers;
using UnityEngine;
using System.Linq;
using Characters;
using Final;

public class ContestantManager : MonoSingleton<ContestantManager>
{
    [SerializeField] private List<GameObject> contestants = new List<GameObject>();
    
    private void Awake()
    {
        FinishController.Instance.AddMethodFinishEvent(FinalStageSetter);
    }

    private void FinalStageSetter()
    {
        var order = GetTheOrder();

        if (order[0].GetComponent<PlayerAnimatorController>()!=null)
        {
            PlayerAnimatorController.Instance.isWin = true;
            PlayerAnimatorController.Instance.SetTriggerFinish();
        }
        else
        {
            order[0].GetComponent<AIAnimatorController>().SetTriggerFinish();
        }

        for (var i = 1; i < order.Count; i++)
        {
            if (order[i].GetComponent<PlayerAnimatorController>() != null)
            {
                PlayerAnimatorController.Instance.SetTriggerFinish();
            }
            else
            {
                order[i].GetComponent<AIAnimatorController>().SetTriggerFinish();
            }
        }
        FinalController.Instance.
            SetOrderInPlace(order[0].transform,order[1].transform,order[2].transform);
    }

    private List<GameObject> GetTheOrder()
    {
        contestants.Sort((p1, p2) => p1.gameObject.transform.position.x.
            CompareTo(p2.gameObject.transform.position.x));
        
        contestants.Reverse();
        
        return contestants;
    }
}
