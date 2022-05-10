using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorCutter : MonoBehaviour
{
    
    [SerializeField]
    private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            // Si on touche un Consumer
            if (other.gameObject.GetComponent<ConsommateurClass>() != null
                && other.gameObject.GetComponent<ConsommateurClass>().electricalNetwork != null)
            {
                other.gameObject.GetComponent<ConsommateurClass>().electricalNetwork.GetComponent<ElectricalNetwork>().disconnect(other.gameObject);
                audio.Play();
            }

            // Si on touche un Producteur
            if (other.gameObject.GetComponent<ProducteurClass>() != null
                && other.gameObject.GetComponent<ProducteurClass>().electricalNetwork != null)
            {
                other.gameObject.GetComponent<ProducteurClass>().electricalNetwork.GetComponent<ElectricalNetwork>().disconnect(other.gameObject);
                audio.Play();
            }
        }
    }
}
