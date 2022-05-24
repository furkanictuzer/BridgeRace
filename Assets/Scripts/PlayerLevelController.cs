using System;
using System.Collections;
using System.Collections.Generic;
using ChemicalMatch.Controllers;
using PlayerControl;
using UI;
using UnityEngine;

public enum StateType
{
    Dummy=0,
    Clever=30,
    Genius=75
}
public class PlayerLevelController : MonoSingleton<PlayerLevelController>
{
    public List<GameObject> stagePlayerObjects = new List<GameObject>();
    public List<Transform> spines = new List<Transform>();
    
    public ParticleSystem turnParticle;
    
    [HideInInspector] public GameObject currentPlayer;
    
    public StateType currentState = StateType.Dummy;

    public StateBarController stateBarController;
    
    private int totalPoint = 0;

    private float percent = 0;

    private void Awake()
    {
        turnParticle.Stop();
        
        currentPlayer = stagePlayerObjects[0];
    }

    public void AddPoint(int point)
    {
        totalPoint += point;
        totalPoint = Mathf.Clamp(totalPoint, 0, 100);
        
        SetStatePoint();
        stateBarController.SetStateText(currentState.ToString());
    }

    public void RemovePoint(int point)
    {
        totalPoint -= point;
        totalPoint = Mathf.Clamp(totalPoint, 0, 100);

        SetStatePoint();
        stateBarController.SetStateText(currentState.ToString());
    }

    private void SetStatePoint()
    {
        var prevState = currentState;
        
        if (totalPoint >= (int)StateType.Genius)
        {
            percent = totalPoint / 100f;

            currentState = StateType.Genius;
        }
        else if (totalPoint >= (float) StateType.Clever)
        {
            percent = totalPoint / (float) StateType.Genius;
            
            currentState = StateType.Clever;
        }
        else if (totalPoint >= (float) StateType.Dummy)
        {
            percent = totalPoint / (float) StateType.Clever;
            
            currentState = StateType.Dummy;
        }

        if (prevState != currentState)
        {
            ChangeStageObject(currentState);
        }
    }

    private void ChangeStageObject(StateType state)
    {
        switch (state)
        {
            case StateType.Dummy:
                ActivateStageObject(0);
                break;
            case StateType.Clever:
                ActivateStageObject(1);
                break;
            case StateType.Genius:
                ActivateStageObject(2);
                break;
            default:
                ActivateStageObject(0);
                break;
        }
    }

    private void ActivateStageObject(int num)
    {
        for (var i = 0; i < stagePlayerObjects.Count; i++)
        {
            if (i==num)
            {
                stagePlayerObjects[i].SetActive(true);
                currentPlayer = stagePlayerObjects[i];
                
                PlayerStackController.Instance.gatherParent.eulerAngles = Vector3.zero;
                PlayerStackController.Instance.gatherParent.parent = spines[i];
                PlayerStackController.Instance.gatherParent.localPosition = PlayerStackController.Instance.gatherPos;
                PlayerStackController.Instance.gatherParent.localEulerAngles = Vector3.zero;
                
                TurnAnim(stagePlayerObjects[i]);
                
                stateBarController.ScaleAnimation(1.5f);
            }
            else
            {
                stagePlayerObjects[i].SetActive(false);
            }
        }
        
    }

    private void TurnAnim(GameObject go)
    {
        turnParticle.Play();
        LeanTween.value(0, 360, .5f).setOnUpdate((val) =>
        {
            go.transform.localEulerAngles = new Vector3(0, val, 0);
        }).setOnComplete(() =>
        {
            go.transform.localEulerAngles = Vector3.zero;
            turnParticle.Stop();
        });
    }
}
