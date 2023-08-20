using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class ProgressBar : ValueDisplay
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Graphic _fillGraphic;

        [SerializeField] private Color _maxColor;
        [SerializeField] private Color _minColor;

        public override void SetValue(float value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    "Progress can't be less then 0 and more then 1");

            _slider.value = value;
            _fillGraphic.color = HSBColor.Lerp(
                HSBColor.FromColor(_minColor),
                HSBColor.FromColor(_maxColor),
                value).ToColor();
        }
    }
}