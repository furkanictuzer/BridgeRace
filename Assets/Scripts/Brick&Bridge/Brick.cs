using System;
using System.Collections;
using System.Collections.Generic;
using CollectableObjects;
using UnityEngine;

namespace Brick_Bridge
{
    public enum BrickType
    {
        Empty,
        Painted
    }
    public class Brick : MonoBehaviour
    {
        public BrickType brickType = BrickType.Empty;
        public CubeColor brickCubeColor;

    }
}