using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarriorProductionController : MonoBehaviour
{
    [SerializeField] private WarriorsProductionListManager _warriorsListManager;
    [SerializeField] internal WarriorData warriorData;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _sliderProgressText;
    [SerializeField] private TextMeshProUGUI _countText;
    private int _count = 1;
    private bool _producing = false;
    private int _timer = 0;
    [SerializeField] private int _refreshRateInSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to the warrior list manager
        _warriorsListManager = GetComponentInParent<WarriorsProductionListManager>();
        // Get access to the progress slider
        _progressSlider = GetComponentInChildren<Slider>();
        // Get access to the slider prgoress text
        _sliderProgressText = _progressSlider.GetComponentInChildren<TextMeshProUGUI>();

        // Get the countTextMesh
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        for(int i = 0; i < texts.Length; i++)
        {
            if (texts[i].gameObject != _sliderProgressText)
            {
                _countText = texts[i];
                break;
            }
        }

        // Ensure that the count text matches the count
        _UpdateTheCountText();

        StartCoroutine(_ProduceWarrior());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void AddWarriorToWaitingList()
    {
        _count++;
        _UpdateTheCountText();
    }


    private IEnumerator _ProduceWarrior()
    {
        _producing = true;

        yield return new WaitForSeconds(_refreshRateInSeconds);

        _timer += _refreshRateInSeconds;
        // Update the Progress slider
        _UpdateProgressSlider();

        if (_timer >= warriorData.productionTimeInSeconds)
        { // The warrior has been produced
            // Generate the warrior

            // Play the sound of a warrior of this type being produced
            

            // Check if there is more warrior of this type to produce
            if(_count > 0)
            {
                _count--;
                _UpdateTheCountText();
                StartCoroutine(_ProduceWarrior());
            }
            else
            {
                // To indicate that this list has finished producing
                _warriorsListManager.NotifyWarriorProducorDestoryed(this);
                Destroy(gameObject); // Remove this 
            }
        }
        else
        {
            StartCoroutine(_ProduceWarrior());
        }
    }


    private void _UpdateProgressSlider()
    {
        // Update the slider progress value
        _progressSlider.value = (float)_timer / warriorData.productionTimeInSeconds;

        // Update the slider progress text
        _sliderProgressText.text = (warriorData.productionTimeInSeconds - _timer) + "s";
    }

    private void _UpdateTheCountText()
    {
        _countText.text = _count.ToString();
    }
}
