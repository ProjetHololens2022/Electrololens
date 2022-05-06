using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ConsommateurClass : MonoBehaviour
{
    private string nom;
    private double consommation; //Besoin
    private double apportElectricite; //Reçu
    private double emissionCO2;
    private double tauxDeSatisfaction;
    private int nbHabitants;

    private double consommationEvent = 0.0; 
    private double apportElectriciteEvent = 0.0;
    private double emissionCO2Event = 0.0;
    private double tauxDeSatisfactionEvent = 0.0;
    private int nbHabitantsEvent = 0;



    public bool isConnected = false;
    public GameObject electricalNetwork = null;


    [SerializeField]
    private GameObject infoVilleGO;
    private InfoVille infoVille;

    public string getNom()
    {
        return nom;
    }

    public double getConsommation()
    {
        return consommation + consommationEvent > 0.0 ? consommation + consommationEvent : 0.0;
    }

    public void setConsommation(double consommation)
    {
        this.consommation = consommation;
    }

    public double GetApportElectricite(){
        return  apportElectricite + apportElectriciteEvent > 0.0 ? apportElectricite + apportElectriciteEvent : 0.0;
    }

    public void SetApportElectricite(double apportElectricite){
        this.apportElectricite = apportElectricite;
    }

    public double getTauxDeSatisfaction()
    {
        return tauxDeSatisfaction + tauxDeSatisfactionEvent > 0.0 ? tauxDeSatisfaction + tauxDeSatisfactionEvent : 0.0;
    }

    public void setTauxDeSatisfaction(double tauxDeSatisfaction)
    {
        this.tauxDeSatisfaction = tauxDeSatisfaction;
    }

    public int getNbHabitants()
    {
        return nbHabitants + nbHabitantsEvent > 0 ? nbHabitants + nbHabitantsEvent : 0;
    }

    public void setNbHabitants(int nbHabitants)
    {
        this.nbHabitants = nbHabitants;
    }

    public double getEmissionCO2(){
        return emissionCO2 + emissionCO2Event > 0 ? emissionCO2 + emissionCO2Event : 0;
    }

    public void setNbHabitants(double emissionCO2)
    {
        this.emissionCO2 = emissionCO2;
    }

    void Start()
    {
        // TODO : On start infoville's ProgressBar should be init
        // It's not done, and it's why on the first attempt the values are 50% everywhere.
       
        infoVille = infoVilleGO.GetComponent<InfoVille>();
        randomizedCityData();
        updateProgessValues();
        List<NoRotationDockable> events = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager>().GetEvents();
        foreach(NoRotationDockable e in events){
            ApplyEvent(e);
        }
    }

    void Update()
    {
        if(!isConnected || GetApportElectricite() < getConsommation()){
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1.0f,0.0f,0.0f,1.0f)*10.0f);
        } else {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f,1.0f,0.0f,1.0f)*10.0f);
        }
    }

    void randomizedCityData()
    {
        double randomConsommation = Random.Range(0, 100);
        double randomEmissionCO2 = Random.Range(0, 100);

        apportElectricite = 0.0;
        consommation = randomConsommation;
        emissionCO2 = randomEmissionCO2;
    }

    public void updateProgessValues()
    {
        if (infoVille != null 
            && infoVille.getProgressApportLoadingBar() != null
                && infoVille.getProgressConsoLoadingBar() != null
                    && infoVille.getProgressConsoLoadingBar() != null)
        {
            infoVille.updateLoadingBar(infoVille.getProgressConsoLoadingBar(), getConsommation() / 100);
            infoVille.updateLoadingBar(infoVille.getProgressApportLoadingBar(), GetApportElectricite() / 100);
            infoVille.updateLoadingBar(infoVille.getProgressEmissionLoadingBar(), getEmissionCO2() / 100);
        }
    }

    public void closeProgressBar()
    {
        if (infoVille != null 
            && infoVille.getProgressApportLoadingBar() != null
                && infoVille.getProgressConsoLoadingBar() != null
                    && infoVille.getProgressConsoLoadingBar() != null)
        {
            infoVille.closeProgressAsync(infoVille.getProgressConsoLoadingBar());
            infoVille.closeProgressAsync(infoVille.getProgressApportLoadingBar());
            infoVille.closeProgressAsync(infoVille.getProgressEmissionLoadingBar());
        }
    }

    public void showInfo()
    {
        updateProgessValues();
        infoVilleGO.SetActive(true);
    }

    public void hideInfo()
    {
        infoVilleGO.SetActive(false);
        closeProgressBar();
    }

    public void ApplyEvent(NoRotationDockable e)
    {
        print("consommateur");

        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CDM:
                consommationEvent += 20.0;
                emissionCO2Event += 20.0;
                tauxDeSatisfactionEvent += 10.0;
                nbHabitantsEvent += 10;
                break;
            case TypeEvent.HEUREPOINTE:
                apportElectriciteEvent -= 20.0;
                emissionCO2Event -= 20.0;
                break;
            case TypeEvent.EUROVISION:
                consommationEvent += 15.0;
                nbHabitantsEvent += 5;
                break;
        }
    }

    public void RemoveEvent(NoRotationDockable e)
    {
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CDM:
                consommationEvent -= 20.0;
                emissionCO2Event -= 20.0;
                tauxDeSatisfactionEvent -= 10.0;
                nbHabitantsEvent -= 10;
                break;
            case TypeEvent.HEUREPOINTE:
                apportElectriciteEvent += 20.0;
                emissionCO2Event += 20.0;
                break;
            case TypeEvent.EUROVISION:
                consommationEvent -= 15.0;
                nbHabitantsEvent -= 5;
                break;
        }
    }

    public void Delete(){
        if(isConnected){
            this.electricalNetwork.GetComponent<ElectricalNetwork>().disconnect(this.gameObject);
        }
        Destroy(this.gameObject);
    }

}
