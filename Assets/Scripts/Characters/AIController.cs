using System;
using System.Collections;
using System.Collections.Generic;
using Brick_Bridge;
using CollectableObjects;
using DG.Tweening;
using Final;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Characters
{
    /*public enum CurrentPlace
    {
        FirstPlace,
        SecondPlace,
        ThirdPlace
    }*/
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private CubeColor targetCubeType;

        [SerializeField] private GameObject prevObject;
        
        [SerializeField] private List<GameObject> targetParents = new List<GameObject>();

        private List<GameObject> _collectedCubes = new List<GameObject>();
        
        private GameObject _initPrevObj;
        
        private Transform _targetRope;

        private const float Radius = 1.5f;
        private const float RequiredCubes = 5f;

        private const float MinRequiredCube = 0f;

        private int _currentFloor = 1;
        
        private bool _haveTarget = false;
        
        [HideInInspector] public Vector3 targetTransform;

        [HideInInspector] public NavMeshAgent agent;
        
        [HideInInspector] public Vector3 gatherPos;
        
        public Transform gatherParent;
        
        public int collectCubePoint = 10;
        public int removeCubePoint = 8;

        public List<Transform> ropesNonActiveChild;
        
        public List<GameObject> targets = new List<GameObject>();
        private readonly List<GameObject> _targets01 = new List<GameObject>();
        private readonly List<GameObject> _targets02 = new List<GameObject>();
        private readonly List<GameObject> _finalTargets = new List<GameObject>();

        private void Awake()
        {
            gatherPos = gatherParent.localPosition;

            FinishController.Instance.AddMethodFinishEvent(DisableOnFinish,ClearDiplomas);
        }

        private void Start()
        {
            _currentFloor = 1;

            _targetRope = BricksController.Instance.bricks01[(int) targetCubeType - 1];
            _initPrevObj = prevObject;
            
            foreach (Transform brick in _targetRope)
            {
                if (!brick.GetComponent<MeshRenderer>().enabled )
                {
                    ropesNonActiveChild.Add(brick);
                }
            }
            
            for (var i = 0; i < targetParents[0].transform.childCount; i++)
                _targets01.Add(targetParents[0].transform.GetChild(i).gameObject);

            for (var i = 0; i < targetParents[1].transform.childCount; i++)
                _targets02.Add(targetParents[1].transform.GetChild(i).gameObject);

            for (var i = 0; i < targetParents[2].transform.childCount; i++)
                _finalTargets.Add(targetParents[2].transform.GetChild(i).gameObject);

            targets = _targets01;

            agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            if (!_haveTarget && targets.Count > 0)
                ChooseTarget();
        }

        private void ChooseTarget()
        {
            if (_collectedCubes.Count >= RequiredCubes)//The part that the character will go to bridge
            {
                if (ropesNonActiveChild.Count != 0)
                {
                    targetTransform = ropesNonActiveChild[ropesNonActiveChild.Count - 1].position;
                }
            }
            else//The part that the character will take cubes
            {
                var hitColliders = Physics.OverlapSphere(transform.position, Radius);
                var cubes = new List<Vector3>();

                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponent<Cube>()?.cubeColor == targetCubeType)
                    {
                        cubes.Add(hitCollider.transform.position);
                    }
                }
                if (cubes.Count > 0)
                {
                    targetTransform = cubes[0];
                }
                else
                {
                    var random = Random.Range(0, targets.Count);

                    targetTransform = targets[random].transform.position;
                }
                
            }

            if (!agent.enabled) return;
            
            agent.SetDestination(targetTransform);
            _haveTarget = true;


        }
        
        private void ClearDiplomas()
        {
            gatherParent.gameObject.SetActive(false);
        }

        #region OnTrigger

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Cube>()?.cubeColor == targetCubeType)
            {
                var cube = other.transform;
                
                var cubePos = cube.localPosition;
                var parent = cube.parent.parent;
                
                other.transform.SetParent(gatherParent);

                var pos = _initPrevObj.transform.localPosition;
                var rotation = Quaternion.Euler(-90, 0, 90);
                //var scale = Vector3.one * .5f;

                _collectedCubes.Add(other.gameObject);
                
                pos += new Vector3(0, 0.005f, 0f) * _collectedCubes.Count;
                
                //other.transform.localScale = scale;

                cube.localRotation = rotation;

                other.transform.DOLocalMove(pos, 0.2f);
                
                prevObject = other.gameObject;

                targets.Remove(cube.gameObject);
                
                other.GetComponent<Collider>().enabled = false;

                _haveTarget = false;

                GenerateCubes.Instance.GenerateCube(cubePos, parent);

            }
            else if ((other.gameObject.GetComponent<Brick>()?.brickType == BrickType.Empty || 
                      (other.gameObject.GetComponent<Brick>()?.brickType == BrickType.Painted &&
                       other.gameObject.GetComponent<Brick>()?.brickCubeColor != targetCubeType)) && 
                     other.gameObject.GetComponent<Cube>() == null)
            {
                if (other.gameObject.CompareTag("LastBrick"))
                {
                    SetUpperFloorCubes();
                    _haveTarget = false;
                }
                
                if (_collectedCubes.Count > MinRequiredCube)
                {
                    var go = _collectedCubes[_collectedCubes.Count - 1];
                    var color=go.GetComponent<Cube>().cube_Color;
                
                    _collectedCubes.Remove(go);
                    Destroy(go);

                    CubeToBrick(other.gameObject,color);

                    prevObject = _collectedCubes.Count != 0 ?_collectedCubes[_collectedCubes.Count-1].gameObject : _initPrevObj;
                }
                else
                {
                    prevObject = _initPrevObj;
                    _haveTarget = false;
                }
            }
        }

        #endregion

        private void SetUpperFloorCubes()
        {
            _currentFloor += 1;
            
            if (_currentFloor == 2)
            {
                targets.Clear();
                
                targets = _targets02;
                
                ropesNonActiveChild.Clear();

                _targetRope = BricksController.Instance.bricks02[(int) targetCubeType - 1];
                
                foreach (Transform brick in _targetRope)
                {
                    if (!brick.GetComponent<MeshRenderer>().enabled )
                    {
                        ropesNonActiveChild.Add(brick);
                    }
                }
            }else if (_currentFloor == 3)
            {
                targets.Clear();
                
                targets = _finalTargets;
                
                ropesNonActiveChild.Clear();

                _targetRope = BricksController.Instance.finalBricks[0];
                
                foreach (Transform brick in _targetRope)
                {
                    if (!brick.GetComponent<MeshRenderer>().enabled )
                    {
                        ropesNonActiveChild.Add(brick);
                    }
                }
            }
        }

        private void CubeToBrick(GameObject go, Color cubeColor)
        {
            go.GetComponent<MeshRenderer>().material.color = cubeColor;
            go.GetComponent<MeshRenderer>().enabled = true;

            go.GetComponent<Brick>().brickCubeColor = targetCubeType;
            go.GetComponent<Brick>().brickType = BrickType.Painted;
        }

        private void DisableOnFinish()
        {
            agent.enabled = false;
            transform.LookAt(transform.position + Vector3.left);
        }
    }
}
