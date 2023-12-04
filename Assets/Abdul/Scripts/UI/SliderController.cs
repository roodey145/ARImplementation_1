using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _upgradeFill;
    [SerializeField] private TextMeshProUGUI _text;
    public string message = "";

    // Start is called before the first frame update
    void Start()
    {
        //_upgradeFill = transform.Find(upgradeFillName).GetComponent<RectTransform>();
        //_slider = GetComponentInChildren<Slider>();

        UpdateData(100, 50, 25);
    }

    public void UpdateData(float maxValue, float currentValue, float extraValueAfterUpgrade)
    {
        _slider.value = currentValue/maxValue;
        _upgradeFill.anchorMax = new Vector2((currentValue + extraValueAfterUpgrade)/maxValue, _upgradeFill.anchorMax.y);
        _text.text = message + ": " + currentValue + " + " + extraValueAfterUpgrade;
    }
}
