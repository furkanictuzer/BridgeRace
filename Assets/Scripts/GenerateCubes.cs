using System.Collections;
using System.Collections.Generic;
using Characters;
using ChemicalMatch.Controllers;
using UnityEngine;

public class GenerateCubes : MonoSingleton<GenerateCubes>
{
    public GameObject cube1, cube2, cube3;

    public List<AIController> aIControllers = new List<AIController>();

    private List<Transform> _parents = new List<Transform>();

    public void GenerateCube(Vector3 pos,Transform parent,int num = 0)
    {
        _parents.Clear();
        
        for (var i = 0; i < parent.childCount; i++)
        {
            _parents.Add(parent.GetChild(i));
        }

        var number = Random.Range(1, 4);

        switch (number)
        {
            case 1://Blue
                StartCoroutine(Generate(cube1, _parents[0], pos));
                break;
            case 2://Yellow
                StartCoroutine(Generate(cube2, _parents[1], pos, aIControllers[0]));
                break;
            case 3://Orange
                StartCoroutine(Generate(cube3, _parents[2], pos, aIControllers[1]));
                break;
        }
    }

    private static IEnumerator Generate(GameObject go, Transform parent, Vector3 pos, AIController aIController = null)
    {
        yield return new WaitForSeconds(3f);

        var cubeObj = Instantiate(go, pos, Quaternion.identity);
        var rotation = Quaternion.Euler(-90, 0, 0);

        cubeObj.transform.localRotation = rotation;
        
        cubeObj.SetActive(true);

        cubeObj.transform.parent = parent;
        
        cubeObj.transform.localPosition = pos;

        if (aIController != null)
            aIController.targets.Add(cubeObj);
        
    }
}
