using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUIOnTrigger : MonoBehaviour
{
    [SerializeField] private string _tag = "UI";
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_tag))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
