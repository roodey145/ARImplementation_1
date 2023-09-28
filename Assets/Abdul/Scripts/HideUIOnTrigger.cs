using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUIOnTrigger : MonoBehaviour
{
    [SerializeField] private string _tag = "UI";
    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if(other.CompareTag(_tag))
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            other.gameObject.SetActive(true);
        }
    }
}
