using System;
using ChemicalMatch.Controllers;
using Final;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StateBarController : MonoBehaviour
    {
        private TextMeshProUGUI _stateText;
        
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;

            _stateText = GetComponent<TextMeshProUGUI>();
            FinishController.Instance.AddMethodFinishEvent(DisableOnFinish);
        }

        private void DisableOnFinish()
        {
            gameObject.SetActive(false);
        }

        public void SetStateText(string text)
        {
            _stateText.text = text;
        }
        
        public void ScaleAnimation(float coefficient)
        {
            gameObject.LeanScale(_scale * coefficient, .5f).setLoopPingPong(1);
        }

        
        

    }
}
