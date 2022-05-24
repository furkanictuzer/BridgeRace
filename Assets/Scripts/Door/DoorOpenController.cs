using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenController : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoors();
            Destroy(gameObject);
        }
    }

    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Door>().OpenDoorAnim();
        }
    }
}
