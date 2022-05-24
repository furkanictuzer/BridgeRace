using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
   private Animator _animator;

   private void Awake()
   {
       _animator = GetComponent<Animator>();
       _animator.speed = 0f;
   }

   public void OpenDoorAnim()
   {
       _animator.speed = 1f;
   }
}
