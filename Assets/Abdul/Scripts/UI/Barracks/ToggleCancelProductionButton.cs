using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleCancelProductionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _cancelText;

    private void Start()
    {
        _countText.gameObject.SetActive(true);
        _cancelText.gameObject.SetActive(false);
    }

    public void HoverEnter()
    {
        _countText.gameObject.SetActive(false);
        _cancelText.gameObject.SetActive(true);
    }

    public void HoverExit()
    {
        _countText.gameObject.SetActive(true);
        _cancelText.gameObject.SetActive(false);
    }
}
