using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNetwork : MonoBehaviour
{
    List<GameObject> producers = new List<GameObject>();
    List<GameObject> consumers = new List<GameObject>();
    private Hashtable objLines = new Hashtable();

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
                this.objLines.Add(go, line);
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
                this.objLines.Add(go, line);
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

    /**
     * Deconnecte un obj du resaux electrique et suprimme
     */
    public void disconnect(GameObject other)
    {
        if (this.producers.Contains(other))
        {
            this.producers.Remove(other);
        }

        if (this.consumers.Contains(other))
        {
            this.consumers.Remove(other);
        }

        // On detruit la ligne reliant le resaux et l'obj.
        GameObject line = this.findLine(other);
        if (line != null)
        {
            Destroy(line);
        }
    }

    /**
     * Renvoie la ligne qui connecte vers un object, si cette ligne existe et est connectee
     */
    private GameObject findLine(GameObject other)
    {
        if (this.objLines.ContainsKey(other))
        {
            return (GameObject) this.objLines[other];
        }
        return null;
    }

}
