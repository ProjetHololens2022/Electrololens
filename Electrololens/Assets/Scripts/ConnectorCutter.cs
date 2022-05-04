using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorCutter : MonoBehaviour
{


    private GameObject lastGameObject = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            // Si on touche un Consumer
            if (other.gameObject.GetComponent<ConsommateurClass>() != null)
            {
                // Si le dernier obj est un BigElectricPole
                if (this.lastGameObject.GetComponent<ElectricalNetwork>() != null)
                {
                    // On deconnecte l'obj touche
                    this.lastGameObject.disconnect(other);
                }
                this.lastGameObject = other.gameObject;
            }

            // Si on touche un Producteur
            if (other.gameObject.GetComponent<ProducteurClass>() != null)
            {
                if (this.lastGameObject.GetComponent<ElectricalNetwork>() != null)
                {
                    this.lastGameObject.disconnect(other);
                }
                this.lastGameObject = other.gameObject;
            }

            // Si on touche un BigElectricPole
            if (other.gameObject.GetComponent<ElectricalNetwork>() != null)
            {
                // Si on avait deja touche un obj, on le deco du BigElectricPole
                if (this.lastGameObject != null)
                {
                    other.disconnect(this.lastGameObject);
                }
                this.lastGameObject = other.gameObject;
            }
        }
    }
}
