using System.Collections.Generic;
using ChemicalMatch.Controllers;
using UnityEngine;

namespace Brick_Bridge
{
    public class BricksController : MonoSingleton<BricksController>
    {
        public List<Transform> bricks01;
        public List<Transform> bricks02;
        public List<Transform> finalBricks;
    }
}
