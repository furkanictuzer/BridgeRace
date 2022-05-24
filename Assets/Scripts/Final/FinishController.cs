using System;
using Characters;
using ChemicalMatch.Controllers;
using UnityEngine;

namespace Final
{
    public sealed class FinishController : MonoSingleton<FinishController>
    {
        private event Action FinishEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerStackController>() != null || other.gameObject.GetComponent<AIController>() != null)
            {
                OnFinishEvent();
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }

        public void AddMethodFinishEvent(params Action[] methods)
        {
            foreach (var method in methods)
            {
                FinishEvent += method;
            }
        }

        private void OnFinishEvent()
        {
            FinishEvent?.Invoke();
        }
    }
}
