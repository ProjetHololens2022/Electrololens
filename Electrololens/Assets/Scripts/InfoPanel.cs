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

    private ConsommateurClass lastConsumer;
    private ProducteurClass lastProducer;
    private ElectricalNetwork lastElectricalNetwork;


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

        modifyDiag(consPol + prodPol, " kg/an", diagInfoRegion.transform.GetChild(0).GetComponentInChildren<ModifyDiagram>());
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
        majConsumer();
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

    void majConsumer(){
        double consommation = lastConsumer.getConsommation();
        double apport = lastConsumer.GetApportElectricite();
        double pollution = lastConsumer.getEmissionCO2();
        int nbHabitants = lastConsumer.getNbHabitants();
        double tauxSatisfaction = lastConsumer.getTauxDeSatisfaction();
        Transform diagrams = infoConsomateur.transform.GetChild(0);
        infoConsomateur.modifyDiag(consommation,'kWh',diagrams.GetChild(0).GetComponent<ModifyDrag>());
        infoConsomateur.modifyDiag(100.0*apport/consommation,"%",diagrams.GetChild(1).GetComponent<ModifyDrag>());
        infoConsomateur.modifyForeground(100.0*apport/consommation,diagrams.GetChild(1).GetComponent<ModifyDrag>());
        infoConsomateur.modifyDiag(pollution,"kg/an",diagrams.GetChild(2).GetComponent<ModifyDrag>());
        infoConsomateur.modifyDiag(nbHabitants,"k",diagrams.GetChild(3).GetComponent<ModifyDrag>());
    }

    void showConsumer(ConsommateurClass consumer){
        infoProducteur.SetActive(false);
        infoConsomateur.SetActive(true);
        lastConsumer = consumer;
    }

    void showProducer(ProducteurClass producer){
        infoProducteur.SetActive(true);
        infoConsomateur.SetActive(false);
    }
}
