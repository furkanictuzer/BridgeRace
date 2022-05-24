using System;
using UnityEngine;

namespace CollectableObjects
{
    public class Cube : CollectableObject
    {
        public CubeColor cubeColor;

        private GameObject _cubePrefab;
        [HideInInspector] public Color cube_Color;

        private void Awake()
        {
            _cubePrefab = cubeColor switch
            {
                CubeColor.Blue => GenerateCubes.Instance.cube1,
                CubeColor.Yellow => GenerateCubes.Instance.cube2,
                CubeColor.Orange => GenerateCubes.Instance.cube3,
                _ => _cubePrefab
            };
            
            
            cube_Color = cubeColor switch
            {
                CubeColor.Blue => Color.blue,
                CubeColor.Yellow => Color.yellow,
                CubeColor.Orange => new Color(255 / 255f, 128 / 255f, 0),
                _ => cube_Color
            };
        }
    }
}
