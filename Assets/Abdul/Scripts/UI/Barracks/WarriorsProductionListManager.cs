using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorsProductionListManager : MonoBehaviour
{
    [SerializeField] private WarriorProductionController[] _warriorsProductionUI;
    [SerializeField] private string _warriorsProductionUIPath;
    [SerializeField] private List<WarriorProductionController> _warriorsToProduce = new List<WarriorProductionController>();

    // Start is called before the first frame update
    void Start()
    {
        _warriorsProductionUI = Resources.LoadAll<WarriorProductionController>(_warriorsProductionUIPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void NotifyWarriorProducorDestoryed(WarriorProductionController producor)
    {
        _warriorsToProduce.Remove(producor);

        // Notify the next producor in line to start producing warriors
        _OrderNextProducorToStart();

    }

    internal void AddWarriorToProduce(WarriorData warriorData)
    {
        // Check if there is already a UI of the same warriorType
        for(int i = 0; i < _warriorsToProduce.Count; i++)
        {
            if (_warriorsToProduce[i].warriorData.warriorType == warriorData.warriorType)
            { // A UI with warrior producing functionlity has already been added
                // Add the new warrior to the production wait list
                _warriorsToProduce[i].AddWarriorToWaitingList();
                return; // No need to go anyfurther
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        // The code below will only run if there is no UI of the desired warriorType previousely intialized //
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // Run through the UIs to see which one matches the warrior type
        for (int i = 0; i < _warriorsProductionUI.Length; i++)
        {
            // Check if this UI has the desired warrior type
            if(_warriorsProductionUI[i].warriorData.warriorType == warriorData.warriorType)
            { // This is the UI that should be added
                // Instanitate the UI which will controll the production of this type of warriors
                WarriorProductionController productionController = 
                    Instantiate(_warriorsProductionUI[i], transform).GetComponent<WarriorProductionController>();
               
                // Add the ui to the warriros production to be able to add a new warrior to the wait list if needed
                _warriorsToProduce.Add(
                    productionController
                );

            }
        }

        // Start producing if the production did not start yet
        _OrderNextProducorToStart();
    }

    private void _OrderNextProducorToStart()
    {
        if(_warriorsToProduce.Count > 0)
        {
            // Start the to produce warrior if there is any producor that is not done producing yet
            _warriorsToProduce[0].StartProduction();
        }
        
    }
}
