using System;
using Characters;
using UnityEngine;

namespace HitController
{
    public class PlayerCollisionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AIController>() != null)
            {
                other.GetComponent<AICollisionController>().HitPlayer(transform);
            }
        }
    }
}
