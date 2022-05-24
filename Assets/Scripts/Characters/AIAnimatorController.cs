using System;
using Final;
using UnityEngine;

namespace Characters
{
    public class AIAnimatorController : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Finish = Animator.StringToHash("Finish");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Win = Animator.StringToHash("Win");
        private static readonly int Lose = Animator.StringToHash("Lose");

        public bool isWin = false;


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void SetFallTrigger()
        {
            _animator.SetTrigger(Fall);
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
}
