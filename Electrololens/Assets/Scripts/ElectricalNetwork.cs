using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNetwork : MonoBehaviour
{
    List<GameObject> producers = new List<GameObject>();
    List<GameObject> consumers = new List<GameObject>();

    [SerializeField]
    private GameObject linePrefab;

    public void addBuilding(GameObject go){
        if(go.GetComponent<ProducteurClass>() != null){
            if(!producers.Contains(go)){
                producers.Add(go);
                print("ajout d'un producteur.");
                GameObject line = Instantiate(linePrefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
                line.transform.parent = this.transform;
                line.GetComponent<LineConnector>().SetStart(go.transform.Find("Sphere"));
                line.GetComponent<LineConnector>().SetEnd(this.transform.Find("Sphere"));
            }
        }
        if(go.GetComponent<ConsommateurClass>() != null){
            if(!consumers.Contains(go)){
                consumers.Add(go);
                print("ajout d'un consommateur.");
                GameObject line = Instantiate(linePrefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
                line.transform.parent = this.transform;
                line.GetComponent<LineConnector>().SetStart(this.transform.Find("Sphere"));
                line.GetComponent<LineConnector>().SetEnd(go.transform.Find("Sphere"));
            }
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
