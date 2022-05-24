using System;
using System.Collections;
using System.Collections.Generic;
using ChemicalMatch.Controllers;
using UnityEngine;

public class PlayerAnimatorController : MonoSingleton<PlayerAnimatorController>
{
    [SerializeField] private Animator _animator;
    
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Lose = Animator.StringToHash("Lose");

    public bool isWin;

    private void Awake()
    {
        //_animator = transform.GetComponent<Animator>();
    }

    public void SetRunBool(bool isRun)
    {
        _animator.SetBool(Run, isRun);
    }
    
    public void SetTriggerFinish()
    {
        if (isWin)
        {
            SetWinTrigger();
        }
        else
        {
            SetLoseTrigger();
        }
    }

    private void SetWinTrigger()
    {
        _animator.SetTrigger(Win);
    }

    private void SetLoseTrigger()
    {
        _animator.SetTrigger(Lose);
    }
}
