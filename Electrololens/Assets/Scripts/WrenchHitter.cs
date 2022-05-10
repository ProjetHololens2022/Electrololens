using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchHitter : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ProducteurClass>() != null)
        {
            other.gameObject.GetComponent<ProducteurClass>().reparationEtat();
            audio.Play();
        }
    }
}
