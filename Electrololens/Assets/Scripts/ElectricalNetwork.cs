using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNetwork : MonoBehaviour
{
    List<GameObject> producers = new List<GameObject>();
    List<GameObject> consumers = new List<GameObject>();

    public void addBuilding(GameObject go){
        if(go.GetComponent<ProducteurClass>() != null){
            if(!producers.Contains(go)){
                producers.Add(go);
                print("ajout d'un producteur.");
                drawLine(go);
            }
        }
        if(go.GetComponent<ConsommateurClass>() != null){
            if(!consumers.Contains(go)){
                consumers.Add(go);
                print("ajout d'un consommateur.");
                drawLine(go);
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

    private void drawLine(GameObject go){
        GameObject lineGo = new GameObject();
        lineGo.AddComponent<LineRenderer>();
        LineRenderer line = lineGo.GetComponent<LineRenderer>();
        line.positionCount = 100;
        line.widthMultiplier = 0.002f;
        Vector3 pos1 = go.transform.Find("Sphere").position;
        Vector3 pos3 = this.transform.Find("Sphere").position;
        Vector3 pos2 = pos1 + (pos3-pos1)/2.0f;
        pos2.y -= 0.02f;
        Vector3[] positions = new Vector3[100];
        for(int i = 0; i < 100; ++i){
            float t = ((float) i) / 99.0f;
            Vector3 lerp12 = Vector3.Lerp(pos1,pos2,t);
            Vector3 lerp23 = Vector3.Lerp(pos2,pos3,t);
            positions[i] = Vector3.Lerp(lerp12,lerp23,t);
        }
        line.SetPositions(positions);
        line.alignment = LineAlignment.View;
        line.loop = true;
        lineGo.transform.parent = this.transform;
    }

}
