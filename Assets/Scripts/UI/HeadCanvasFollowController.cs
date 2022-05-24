using System;
using Final;
using UnityEngine;

namespace UI
{
    public class HeadCanvasFollowController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private float _distanceYTarget, _distanceXTarget;

        

        private void Awake()
        {
            _distanceYTarget = transform.position.y - target.position.y;
            _distanceXTarget = transform.position.x - target.position.x;
            
            FinishController.Instance.AddMethodFinishEvent(DisableObject);
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            var targetPosition = target.position;
            var pos = transform;
            pos.position = new Vector3(targetPosition.x+_distanceXTarget, targetPosition.y + _distanceYTarget, targetPosition.z);
        }

        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
