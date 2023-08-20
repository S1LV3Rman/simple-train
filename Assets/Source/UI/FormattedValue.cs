using TMPro;
using UnityEngine;

namespace Source
{
    public class FormattedValue : ValueDisplay
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _format;

        public override void SetValue(float value)
        {
            _text.text = string.Format(_format, value);
        }
    }
}