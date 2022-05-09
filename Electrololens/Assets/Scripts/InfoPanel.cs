using System;
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

    private GameObject diagInfoRegion;
    private GameObject[] cons, prod;



    public void getAllPollution()
    {
        double consPol = 0;
        double prodPol = 0;
        foreach (var c in cons)
        {
            consPol += getPollutionCons(c);
        }
        foreach (var p in prod)
        {
            prodPol += getPollutionProd(p);
        }

        modifyDiag(consPol + prodPol, " Kg/CO2", diagInfoRegion.transform.GetChild(0).GetComponentInChildren<ModifyDiagram>());
    }

    void getAllEnergieProd()
    {
        double prodEnergie = 0.0;
        double prodMax = 0.0;
        foreach (var p in prod)
        {
            prodEnergie += getProductionProd(p);
            prodMax += getMaxProductionProd(p);
        }
        double percentage = (prodEnergie / prodMax) * 100.0;
        double realPercentage = Math.Round(percentage, 2);
        modifyDiag(realPercentage, "%", diagInfoRegion.transform.GetChild(1).GetComponentInChildren<ModifyDiagram>());
        modifyForeground(realPercentage / 100, diagInfoRegion.transform.GetChild(1).GetComponentInChildren<ModifyDiagram>());
    }

    void getAllEnergiePerdue()
    {
        double consEnergie = 0.0;
        double prodEnergie = 0.0;
        foreach (var p in prod)
        {
            prodEnergie += getProductionProd(p);
        }
        foreach (var c in cons)
        {
            consEnergie += getConsommationCons(c);
        }
        Debug.Log(prodEnergie + " " + consEnergie);
        double percentage = ((prodEnergie - consEnergie) / prodEnergie) * 100.0;
        double realPercentage = percentage >= 0.0 ? Math.Round(percentage, 2) : 0.0;
        modifyDiag(realPercentage, "%", diagInfoRegion.transform.GetChild(2).GetComponentInChildren<ModifyDiagram>());

        modifyForeground(realPercentage / 100, diagInfoRegion.transform.GetChild(2).GetComponentInChildren<ModifyDiagram>());
    }

    void modifyDiag(double val, string unit, ModifyDiagram modDiag)
    {
        modDiag.updateValue(val, unit);
    }

    void modifyForeground(double val, ModifyDiagram modDiag)
    {
        modDiag.updateForegroud(val);
    }


    // Start is called before the first frame update
    void Start()
    {
        diagInfoRegion = infoRegion.transform.GetChild(0).gameObject;
        getAllAgents();
    }

    // Update is called once per frame
    void Update()
    {
        getAllAgents();
        getAllPollution();
        getAllEnergieProd();
        getAllEnergiePerdue();
    }

    int getAllAgents()
    {
        cons = GameObject.FindGameObjectsWithTag("Consumer");
        prod = GameObject.FindGameObjectsWithTag("Producer");
        return cons.Length + prod.Length;
    }

    double getPollutionCons(GameObject c)
    {
        return c.GetComponent<ConsommateurClass>().getEmissionCO2();
    }
    double getConsommationCons(GameObject c)
    {
        return c.GetComponent<ConsommateurClass>().getConsommation();
    }

    double getPollutionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().getEmissionCO2();
    }

    double getProductionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().getProduction();
    }

    double getMaxProductionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().MaxProduction();
    }

    void showDiag(TypeAgent type)
    {
        switch(type)
        {
            case TypeAgent.CONSUMER:
                infoProducteur.SetActive(false);
                infoConsomateur.SetActive(true);
                break;
            case TypeAgent.PRODUCER:
                infoConsomateur.SetActive(false);
                infoProducteur.SetActive(true);
                break;
        }
    }
}
