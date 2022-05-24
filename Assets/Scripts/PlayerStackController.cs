using System;
using System.Collections;
using System.Collections.Generic;
using Brick_Bridge;
using ChemicalMatch.Controllers;
using CollectableObjects;
using UnityEngine;
using DG.Tweening;
using Final;

public class PlayerStackController : MonoSingleton<PlayerStackController>
{
    
    public Transform gatherParent;
    [HideInInspector] public Vector3 gatherPos;
    
    private const float MinRequiredBook = 0f;
    
    private List<GameObject> _collectedBooks = new List<GameObject>();
    
    private GameObject _initPrevObj;

    private const int CollectBookPoint = 5;
    private const int RemoveBookPoint = 4;

    public CubeColor targetCubeColor;

    public GameObject prevObject;
    
    public float playerXBound = 7f;

    private void Awake()
    {
        gatherPos = gatherParent.localPosition;
        _initPrevObj = prevObject;
        FinishController.Instance.AddMethodFinishEvent(ClearDiplomas);
    }

    private void ClearDiplomas()
    {
        gatherParent.gameObject.SetActive(false);
    }

    #region OnTrigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cube>()?.cubeColor == targetCubeColor)
        {
            var cube = other.transform;
            var bookPos = cube.localPosition;
            var parent = cube.parent.parent;
            
            other.transform.SetParent(gatherParent);

            var pos = _initPrevObj.transform.localPosition;
            var rotation = Quaternion.Euler(-90, 0, 90);
            var scale = Vector3.one * .5f;

            pos += new Vector3(0, 0.005f, 0f) * _collectedBooks.Count;
            
            //other.transform.localScale = scale;

            cube.localRotation = rotation;
            
            other.transform.DOLocalMove(pos, 0.2f);
            prevObject = other.gameObject;
            _collectedBooks.Add(cube.gameObject);
                    
            other.GetComponent<Collider>().enabled = false;

            GenerateCubes.Instance.GenerateCube(bookPos, parent,1); 
        }
        else if ((other.gameObject.GetComponent<Brick>()?.brickType == BrickType.Empty || 
                  (other.gameObject.GetComponent<Brick>()?.brickType == BrickType.Painted &&
                   other.gameObject.GetComponent<Brick>()?.brickCubeColor != targetCubeColor)) && 
                 other.gameObject.GetComponent<Cube>() == null)
        {
            if (other.CompareTag("LastBrick") && other.gameObject.GetComponent<Brick>()?.brickCubeColor == targetCubeColor)
            {
                playerXBound = other.transform.position.x + 13;
            }
            
            if (_collectedBooks.Count > MinRequiredBook)
            {
                var go = _collectedBooks[_collectedBooks.Count - 1];
                var bookColor=go.GetComponent<Cube>().cube_Color;
                
                _collectedBooks.Remove(go);
                Destroy(go);

                other.gameObject.GetComponent<MeshRenderer>().material.color = bookColor;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;

                other.gameObject.GetComponent<Brick>().brickCubeColor = targetCubeColor;
                other.gameObject.GetComponent<Brick>().brickType = BrickType.Painted;

                playerXBound = other.transform.position.x + .05f;

                prevObject = _collectedBooks.Count != 0 ?_collectedBooks[_collectedBooks.Count-1].gameObject : _initPrevObj;
            }
            else
            {
                prevObject = _initPrevObj;
            }
                
        }
        else if (other.gameObject.GetComponent<Brick>()?.brickCubeColor == targetCubeColor)
        {
            playerXBound = other.transform.position.x + .05f;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LastBrick") && other.gameObject.GetComponent<Brick>()?.brickCubeColor == targetCubeColor)
        {
            playerXBound = other.transform.position.x + 13;
        }
    }

    #endregion
}
