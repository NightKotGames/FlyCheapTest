using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Utility
{
    public class PanelSelector : MonoBehaviour
    {
        [SerializeField] private Button _runButton;
        [SerializeField] private List<RectTransform> _activatePanels = new();
        [SerializeField] private List<RectTransform> _deActivatePanels = new();
        [SerializeField] private List<Behaviour> _enableComponents = new();
        [SerializeField] private List<Behaviour> _disableComponents = new();
        [Space(20)]
        [SerializeField, TextArea(4, 15)] private string _description;

        private void OnEnable() => _runButton.onClick.AddListener(() => RunSelector());
        private void OnDestroy() => _runButton.onClick.RemoveListener(() => RunSelector());

        private void RunSelector()
        {
            if (_activatePanels.Count > 0)
                _activatePanels.ForEach(pnl => pnl.gameObject.SetActive(true));

            if (_deActivatePanels.Count > 0)
                _deActivatePanels.ForEach(pnl => pnl.gameObject.SetActive(false));

            if (_enableComponents.Count > 0)
                _enableComponents.ForEach(comp => comp.enabled = true);

            if (_disableComponents.Count > 0)
                _disableComponents.ForEach(comp => comp.enabled = false);
        }
    }
}
