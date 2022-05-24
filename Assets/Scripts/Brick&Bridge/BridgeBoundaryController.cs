using System.Collections.Generic;
using CollectableObjects;
using UnityEngine;

namespace Brick_Bridge
{
    public class BridgeBoundaryController : MonoBehaviour
    {
        [SerializeField] private CubeColor boundedCubeColor;
        [SerializeField] private GameObject boundaryObject;
        [SerializeField] private GameObject bricksParent;
        [SerializeField] private List<GameObject> bricks = new List<GameObject>();

        private readonly Vector3 _tempPos = new Vector3(0f, 10f, 0f);

        private Vector3 _lastPaintedBrick = Vector3.zero;

        private void Awake()
        {
            foreach (Transform brick in bricksParent.transform)
            {
                bricks.Add(brick.gameObject);
            }

            boundaryObject.transform.position = bricks[0].transform.position;
        }

        /*public void CheckForPlayer()
        {
            Debug.Log("brick");
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].GetComponent<Brick>().brickType == BrickType.Painted && bricks[i].GetComponent<Brick>().brickCubeColor == boundedCubeColor)
                {
                    _lastPaintedBrick = bricks[i + 1] != null ? bricks[i + 1].transform.position : _tempPos;
                }
            }

            boundaryObject.transform.position = _lastPaintedBrick != Vector3.zero ? _lastPaintedBrick : boundaryObject.transform.position;
        }*/
    }
}
