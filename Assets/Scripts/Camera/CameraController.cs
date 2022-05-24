using System;
using DG.Tweening;
using Final;
using UnityEngine;


    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float lerpValue;

        [SerializeField] private Transform finalCamPos;

        private Vector3 _offSet;

        private const float FinalCamAnimTime = 1f;

        private void Awake()
        {
            FinishController.Instance.AddMethodFinishEvent(FinalCamPosAnim);
        }

        private void Start()
        {
            _offSet = transform.position - target.position;
        }

        private void LateUpdate()
        {
            if (!LevelController.Instance.isFinish)
            {
                FollowTarget();   
            }
        }

        private void FollowTarget()
        {
            var desPos = target.position + _offSet;

            transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);
        }

        private void FinalCamPosAnim()
        {
            LeanTween.move(gameObject, finalCamPos, FinalCamAnimTime);
            LeanTween.rotateLocal(gameObject, finalCamPos.eulerAngles, FinalCamAnimTime);
        }
    }