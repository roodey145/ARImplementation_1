using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattaleProgress : MonoBehaviour
{
    [SerializeField] private AudioClip _losingAudioClip;

    private static string _wallsTag = "Wall";
    private static List<IDableBuilding> _buildings = new List<IDableBuilding>();

    private bool _hasLost = false;

    public static void RegisterBuilding(IDableBuilding building)
    {
        // Check if this building is a wall
        if(!building.CompareTag(_wallsTag))
        {
            _buildings.Add(building);
        }
    }

    public static void UnregisterBuilding(IDableBuilding building)
    {
        for(int i = 0; i < _buildings.Count; i++)
        {
            if (_buildings[i].ID == building.ID)
            {
                _buildings.RemoveAt(i);
                break;
            }
        }
    }

    private void Start()
    {
        StartCoroutine(_EnsureNoNull());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasLost && _buildings.Count <= 0 && waveSpawner.spawningStarted)
        { // The player lost
            _hasLost = true;
            waveSpawner.ResetState();
            StartCoroutine(_Lose());
        }
        else
        {
            //print($"Buildings Count {_buildings.Count}, Has Lost {_hasLost}");
        }
    }


    private IEnumerator _Lose()
    {
        SoundManager.Instance.PlayActionSound(_losingAudioClip);

        yield return new WaitForSeconds(_losingAudioClip.length);

        // Replay the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    private IEnumerator _EnsureNoNull()
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            if (_buildings[i] == null) _buildings.RemoveAt(i);
        }

        yield return new WaitForSeconds(1);

        if(!_hasLost)
            StartCoroutine(_EnsureNoNull());
    }
}
