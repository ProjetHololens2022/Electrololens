using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNetwork : MonoBehaviour
{
    List<GameObject> producers = new List<GameObject>();
    List<GameObject> consumers = new List<GameObject>();

    public void addBuilding(GameObject go){
        if(go.GetComponent<ProducteurClass>() != null){
            producers.Add(go);
        }
        if(go.GetComponent<ProducteurClass>() != null){
            consumers.Add(go);
        }
    }

    public double getProduction(){
        double totalProduction = 0;
        foreach(GameObject go in producers){
            totalProduction += go.GetComponent<ProducteurClass>().getProduction();
        }
        return totalProduction;
    }

    public double getConsumption(){
        return getProduction()/(double)consumers.Count;
    }

}
