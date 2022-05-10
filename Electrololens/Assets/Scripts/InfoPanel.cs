using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private ConsommateurClass lastConsumer = null;
    private ProducteurClass lastProducer = null;
    private ElectricalNetwork lastElectricalNetwork = null;


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
        // Debug.Log(prodEnergie + " " + consEnergie);
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
        MajConsumer();
        MajProducer();
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

    void MajConsumer()
    {
        if(lastConsumer != null)
        {
            double consommation = lastConsumer.getConsommation();
            double apport = lastConsumer.GetApportElectricite();
            double pollution = lastConsumer.getEmissionCO2();
            int nbHabitants = lastConsumer.getNbHabitants();
            //double tauxSatisfaction = lastConsumer.getTauxDeSatisfaction();
        
            infoConsomateur.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().SetText(lastConsumer.name);

            Transform diagrams = infoConsomateur.transform.GetChild(0);
            modifyDiag(consommation,"kWh",diagrams.GetChild(0).GetComponent<ModifyDiagram>());
            modifyDiag(100.0*apport/consommation,"%",diagrams.GetChild(1).GetComponent<ModifyDiagram>());
            modifyForeground(apport/consommation,diagrams.GetChild(1).GetComponent<ModifyDiagram>());
            modifyDiag(pollution,"kg/an",diagrams.GetChild(2).GetComponent<ModifyDiagram>());
            modifyDiag(nbHabitants,"k",diagrams.GetChild(3).GetComponent<ModifyDiagram>());
        }
    }


    void MajProducer()
    {
        if (lastProducer != null)
        {
            double etat = lastProducer.getEtat();
            double pollution = lastProducer.getEmissionCO2();
            double production = lastProducer.getProduction();
            double maxProduction = lastProducer.MaxProduction();

            infoProducteur.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().SetText("Usine - " + lastProducer.getType().ToString());

            Transform diagrams = infoProducteur.transform.GetChild(0);
            double prodpercent = Math.Round((production / maxProduction) * 100.0, 2);
            double etatpercent = Math.Round((etat / 100) * 100.0, 2);
            modifyDiag(prodpercent, "%", diagrams.GetChild(0).GetComponent<ModifyDiagram>());
            modifyForeground(prodpercent / 100, diagrams.GetChild(0).GetComponent<ModifyDiagram>());
            modifyDiag(etat, "%", diagrams.GetChild(1).GetComponent<ModifyDiagram>());
            modifyForeground(etat / 100, diagrams.GetChild(1).GetComponent<ModifyDiagram>());
            modifyDiag(pollution, "kg/an", diagrams.GetChild(2).GetComponent<ModifyDiagram>());
        }
    }

    void showConsumer(ConsommateurClass consumer){
        lastConsumer = consumer;
        infoProducteur.SetActive(false);
        infoConsomateur.SetActive(true);
    }

    void showProducer(ProducteurClass producer){
        lastProducer = producer;
        infoConsomateur.SetActive(false);
        infoProducteur.SetActive(true);
    }
}
