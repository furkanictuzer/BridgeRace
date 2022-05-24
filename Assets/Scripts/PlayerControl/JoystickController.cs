using System;
using ChemicalMatch.Controllers;
using Final;
using UnityEngine;

namespace PlayerControl
{
    public class JoystickController : MonoSingleton<JoystickController>
    {
        [SerializeField] private float turnSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float lerpValue;
        [SerializeField] private float minZ, maxZ;
        
        [SerializeField] private LayerMask rayLayerMask;

        [SerializeField] private FixedJoystick fixedJoystick;

        [SerializeField] private Transform footPoint;

        private Rigidbody _body;

        private void Awake()
        {
            FinishController.Instance.AddMethodFinishEvent(Finish);
        }

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ClampPosition();

            if (Input.GetMouseButton(0) && !LevelController.Instance.isFinish)
            {
                if (!(Mathf.Abs(transform.position.x - PlayerStackController.Instance.playerXBound) > 0.2f)) return;
                
                Joystick();
                PlayerAnimatorController.Instance.SetRunBool(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PlayerAnimatorController.Instance.SetRunBool(false);
            }
            else
            {
                PlayerAnimatorController.Instance.SetRunBool(false);
            }
        }

        private void Joystick()
        {
            if (!Physics.Raycast(footPoint.position,Vector3.down,0.2f))
            {
                transform.position += Vector3.down * Time.deltaTime * moveSpeed;
            }

            var direction = Vector3.right * fixedJoystick.Vertical + Vector3.back * fixedJoystick.Horizontal;
            var pos = transform.position + direction * Time.deltaTime * moveSpeed;
            
            pos.x = Mathf.Clamp(pos.x, -4f, PlayerStackController.Instance.playerXBound-.1f);

            transform.position = pos;
            
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            }
        }

        private void Finish()
        {
            transform.LookAt(transform.position + Vector3.left);
            _body.isKinematic = true;
        }

        private void ClampPosition()
        {
            var tempPos = transform.position;
            tempPos.z = Mathf.Clamp(tempPos.z, minZ, maxZ);
            transform.position = tempPos;
        }
    }
}
