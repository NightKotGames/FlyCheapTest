
using System;
using UnityEngine;

namespace Countries.ForAnimate
{
    public class DropDownTempateAreaDispather : MonoBehaviour
    {
        public static event Action OnTemplateEnable; 
        public static event Action OnTemplateDisable; 

        private void OnEnable() => OnTemplateEnable?.Invoke();
        private void OnDisable() => OnTemplateDisable?.Invoke();
    }
}