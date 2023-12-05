using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] internal UpgradeFeature feature;
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _upgradeFill;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationCurve _sliderProgressCurve;
    public string message = "";

    // Start is called before the first frame update
    void Start()
    {
        //_upgradeFill = transform.Find(upgradeFillName).GetComponent<RectTransform>();
        //_slider = GetComponentInChildren<Slider>();

        //UpdateData(100, 50, 25);
    }

    public void UpdateData(UpgradeFeatureDetails details)
    {
        if (_sliderProgressCurve == null) _sliderProgressCurve = AnimationCurve.Linear(0, 0, 1, 1);

        _slider.value = _sliderProgressCurve.Evaluate( details.currentValue / details.maxValue );
        _upgradeFill.anchorMax = new Vector2(_sliderProgressCurve.Evaluate( (details.currentValue + details.extraValueAfterUpgrade) / details.maxValue ), _upgradeFill.anchorMax.y);
        _text.text = message + ": " + details.currentValue + " + " + details.extraValueAfterUpgrade;
    }
}
