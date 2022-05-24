using System;
using UnityEngine;

namespace PlayerControl
{
    public class StairsControl : MonoBehaviour
    {
        [SerializeField] private GameObject upperRayObject;
        [SerializeField] private GameObject lowerRayObject;

        [SerializeField] private float stepSmooth = 0.1f;

        [SerializeField] private LayerMask rayLayerMask;

        private void Update()
        {
            StepStair();
        }

        private void StepStair()
        {
            RaycastHit hitLower;
            if(Physics.Raycast(lowerRayObject.transform.position,transform.TransformDirection(Vector3.forward),out hitLower,0.1f,rayLayerMask))
            {
                RaycastHit hitUpper;
                if (Physics.Raycast(upperRayObject.transform.position,transform.TransformDirection(Vector3.forward),out hitUpper,0.1f,rayLayerMask))
                {
                    transform.position += new Vector3(0, stepSmooth, 0);
                }
            }
        }
    }
}
