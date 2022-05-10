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
    [SerializeField]
    private GameObject infoNetwork;
    [SerializeField]
    private GameObject infoNone;

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
            print(p);
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
            if(isConnectedProd(p)){
                prodEnergie += getProductionProd(p);
            }
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
            if(isConnectedProd(p)){
                prodEnergie += getProductionProd(p);
            }
        }
        foreach (var c in cons)
        {
            if(isConnectedCons(c)){
                consEnergie += getConsommationCons(c);
            }
        }
        double percentage;
        if(prodEnergie > 0.0){
            percentage = ((prodEnergie - consEnergie) / prodEnergie) * 100.0;
        } else {
            percentage = 0.0;
        }
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
        if(lastConsumer != null){
            majConsumer();
        }
        if(lastProducer != null){
            majProducer();
        }
        if(lastElectricalNetwork != null){
            majNetwork();
        }
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

    bool isConnectedCons(GameObject c){
        return c.GetComponent<ConsommateurClass>().isConnected;
    }

    double getPollutionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().getEmissionCO2();
    }

    double getProductionProd(GameObject p)
    {
        return p.GetComponent<ProducteurClass>().getProduction();
    }

    bool isConnectedProd(GameObject p){
        return p.GetComponent<ProducteurClass>().isConnected;
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
        modifyDiag(Math.Round(consommation, 2),"kWh",diagrams.GetChild(0).GetComponent<ModifyDiagram>());
        modifyDiag(Math.Round(100.0*apport/consommation, 2),"%",diagrams.GetChild(1).GetComponent<ModifyDiagram>());
        modifyForeground(apport/consommation,diagrams.GetChild(1).GetComponent<ModifyDiagram>());
        modifyDiag(Math.Round(pollution, 2),"kg/an",diagrams.GetChild(2).GetComponent<ModifyDiagram>());
        modifyDiag(nbHabitants,"k",diagrams.GetChild(3).GetComponent<ModifyDiagram>());
    }

    void majProducer(){
        double production = lastProducer.getProduction();
        double maxProd = lastProducer.MaxProduction();
        double etat = lastProducer.getEtat();
        double pollution = lastProducer.getEmissionCO2();
        Transform diagrams = infoProducteur.transform.GetChild(0);
        modifyDiag(Math.Round(100.0*production/maxProd, 2),"%",diagrams.GetChild(0).GetComponent<ModifyDiagram>());
        modifyForeground(production/maxProd,diagrams.GetChild(0).GetComponent<ModifyDiagram>());
        modifyDiag(Math.Round(etat, 2),"%",diagrams.GetChild(1).GetComponent<ModifyDiagram>());
        modifyForeground(etat/100.0,diagrams.GetChild(1).GetComponent<ModifyDiagram>());
        modifyDiag(Math.Round(pollution, 2),"kg/an",diagrams.GetChild(2).GetComponent<ModifyDiagram>());
    }

    void majNetwork(){
        double[] prodNetwork = lastElectricalNetwork.GetComponent<ElectricalNetwork>().GetProd();
        double[] consNetwork = lastElectricalNetwork.GetComponent<ElectricalNetwork>().GetCons();
        infoNetwork.GetComponent<InfoNetwork>().SetDatas(prodNetwork,consNetwork);
    }

    void showConsumer(ConsommateurClass consumer){
        hideLastBuilding();
        infoConsomateur.SetActive(true);
        lastConsumer = consumer;
        consumer.gameObject.GetComponent<ConsommateurClass>().isSelected();
    }

    void showProducer(ProducteurClass producer){
        hideLastBuilding();
        infoProducteur.SetActive(true);
        lastProducer = producer;
        producer.gameObject.GetComponent<ProducteurClass>().isSelected();
    }

    void showNetwork(ElectricalNetwork network){
        hideLastBuilding();
        infoNetwork.SetActive(true);
        lastElectricalNetwork = network;
        network.gameObject.GetComponent<ElectricalNetwork>().isSelected();
    }

    void hideLastBuilding(){
        infoNone.SetActive(false);
        infoProducteur.SetActive(false);
        infoConsomateur.SetActive(false);
        infoNetwork.SetActive(false);
        if(lastConsumer != null){
            lastConsumer.gameObject.GetComponent<ConsommateurClass>().isNotSelected();
            lastConsumer = null;
        }
        if(lastProducer != null){
            lastProducer.gameObject.GetComponent<ProducteurClass>().isNotSelected();
            lastProducer = null;
        }
        if(lastElectricalNetwork != null){
            lastElectricalNetwork.gameObject.GetComponent<ElectricalNetwork>().isNotSelected();
            lastElectricalNetwork = null;
        }
    }
}
