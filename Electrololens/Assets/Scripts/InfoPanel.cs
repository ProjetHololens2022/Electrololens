using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{

    [SerializeField]
    private GameObject infoRegion;
    [SerializeField]
    private GameObject infoProducteur;
    [SerializeField]
    private GameObject infoConsomateur;

    private GameObject[] cons, prod;

    private int maxPol = 1500;

    void getAllAgents()
    {
        cons = GameObject.FindGameObjectsWithTag("Consumer");
        prod = GameObject.FindGameObjectsWithTag("Producer");
    }

    double getPollutionCons(GameObject c)
    {
        return c.GetComponent<ConsommateurClass>().getEmissionCO2();
    }

    double getPollutionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().getEmissionCO2();
    }

    public void getAllPollution()
    {
        Debug.LogWarning("getall");
        double consPol = 0;
        double prodPol = 0;
        foreach (var c in cons)
        {
           consPol = getPollutionCons(c);
        }
        foreach (var p in prod)
        {
            prodPol = getPollutionProd(p);
        }

        double percentPol = (consPol + prodPol / maxPol) * 100.0;
        Debug.Log(percentPol);
    }



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        getAllAgents();
        getAllPollution();
    }

    // Update is called once per frame
    void Update()
    {
        getAllAgents();
        getAllPollution();
    }
}
