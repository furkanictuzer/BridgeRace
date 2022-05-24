using System;
using System.Collections;
using Final;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject finishCanvas;
        [SerializeField] private Canvas inGameCanvas;

        private const float EnableTime = 4f;

        private void Awake()
        {
            finishCanvas.SetActive(false);
            
            FinishController.Instance.AddMethodFinishEvent(Finish);
        }

        private void Finish()
        {
            StartCoroutine(CanvasActivityCoroutine(inGameCanvas.gameObject, false, 0));
            
            StartCoroutine(CanvasActivityCoroutine(finishCanvas, true, EnableTime));
        }

        private static IEnumerator CanvasActivityCoroutine(GameObject canvas, bool isActive, float time)
        {
            yield return new WaitForSeconds(time);
            canvas.SetActive(isActive);
        }
    }
}
