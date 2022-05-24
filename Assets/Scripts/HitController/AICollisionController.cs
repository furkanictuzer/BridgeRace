using System;
using System.Collections;
using Characters;
using ChemicalMatch.Controllers;
using UnityEngine;

namespace HitController
{
    public class AICollisionController : MonoSingleton<AICollisionController>
    {
        [SerializeField] private float hitDistance = .7f;

        private AIAnimatorController _aıAnimatorController;

        private Collider _collider;
        
        private const float HitAnimTime = 1f;
        

        private void Awake()
        {
            _aıAnimatorController = GetComponent<AIAnimatorController>();

            _collider = gameObject.GetComponent<Collider>();
        }

        public void HitPlayer(Transform hitTransform)
        {
            var force = CalculateForceVector(hitTransform);
            var pos = force.normalized * hitDistance + transform.position;

            LeanTween.move(gameObject, pos, HitAnimTime).setEaseOutExpo();

            _aıAnimatorController.SetFallTrigger();
            StartCoroutine(AgentDisableCoroutine());
            
            
        }

        private Vector3 CalculateForceVector(Transform hitTransform)
        {
            return gameObject.transform.position - hitTransform.position;
        }
        private IEnumerator AgentDisableCoroutine()
        {
            var aIController = gameObject.GetComponent<AIController>();
            var targetTransform = aIController.targetTransform;

            _collider.enabled = false;
            
            aIController.agent.enabled = false;
            
            yield return new WaitForSeconds(HitAnimTime);

            aIController.agent.enabled = true;
            aIController.agent.SetDestination(targetTransform);
            
            _collider.enabled = true;
        }
    }
}

