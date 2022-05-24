using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UI;
using UnityEngine;

public class AILevelController : MonoBehaviour
{
    public List<GameObject> stageAIObjects = new List<GameObject>();
    public List<Transform> spines = new List<Transform>();

    public ParticleSystem turnParticle;

    public StateType currentState = StateType.Dummy;

    [HideInInspector] public GameObject currentAIObject;

    public StateBarController stateBarController;

    private AIController _aıController;
    
    private int _totalPoint = 0;

    private float _percent = 0;

    private void Awake()
    {
        turnParticle.Stop();
        
        _aıController = GetComponent<AIController>();
        
        currentAIObject = stageAIObjects[0];
    }

    public int AddPoint(int point)
    {
        _totalPoint += point;
        _totalPoint = Mathf.Clamp(_totalPoint, 0, 100);
        
        stateBarController.SetStateText(currentState.ToString());

        return _totalPoint;
    }

    public int RemovePoint(int point)
    {
        _totalPoint -= point;
        _totalPoint = Mathf.Clamp(_totalPoint, 0, 100);
        
        SetStatePoint();
        stateBarController.SetStateText(currentState.ToString());

        return _totalPoint;
    }

    public int GetTotalPoint()
    {
        return _totalPoint;
    }

    private float SetStatePoint()
    {
        var prevState = currentState;
        
        if (_totalPoint >= (int)StateType.Genius)
        {
            _percent = _totalPoint / 100f;

            currentState = StateType.Genius;
        }
        else if (_totalPoint >= (float) StateType.Clever)
        {
            _percent = _totalPoint / (float) StateType.Genius;
            
            currentState = StateType.Clever;
        }
        else if (_totalPoint >= (float) StateType.Dummy)
        {
            _percent = _totalPoint / (float) StateType.Clever;
            
            currentState = StateType.Dummy;
        }
        
        if (prevState != currentState)
        {
            ChangeStageObject(currentState);
        }

        return _percent;
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
        for (var i = 0; i < stageAIObjects.Count; i++)
        {
            if (i==num)
            {
                stageAIObjects[i].SetActive(true);

                _aıController.gatherParent.parent = spines[i];
                _aıController.gatherParent.localPosition = _aıController.gatherPos;
                _aıController.gatherParent.eulerAngles = Vector3.zero;

                currentAIObject = stageAIObjects[i];
                
                TurnAnim(stageAIObjects[i]);
            }
            else
            {
                stageAIObjects[i].SetActive(false);
            }
        }
    }

    private void TurnAnim(GameObject go)
    {
        turnParticle.Play();
        LeanTween.value(0, 360, .5f).setOnUpdate((float val) =>
        {
            go.transform.localEulerAngles = new Vector3(0, val, 0);
        }).setOnComplete(() =>
        {
            go.transform.localEulerAngles = Vector3.zero;
            turnParticle.Stop();
        });
    }
}
