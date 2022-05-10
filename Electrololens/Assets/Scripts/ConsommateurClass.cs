using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.IO;
using System.Linq;

public class ConsommateurClass : MonoBehaviour
{
    private string nom = "Chambéry";
    private double consommation = 0.0; //Besoin
    private double apportElectricite = 0.0; //Reçu
    private double emissionCO2 = 0.0;
    private double tauxDeSatisfaction = 0.0;
    private int nbHabitants = 0;

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

    void Start()
    {   
        infoVille = infoVilleGO.GetComponent<InfoVille>();
        if (nom == null)
        {
            randomizedCityData();
        }
        List<NoRotationDockable> events = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager>().GetEvents();
        foreach (NoRotationDockable e in events)
        {
            ApplyEvent(e);
        }
    }

    void Update()
    {
        if (!isConnected || GetApportElectricite() < getConsommation())
        {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1.0f, 0.0f, 0.0f, 1.0f) * 10.0f);
            apportElectricite = 0.0;
        }
        else
        {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 1.0f, 0.0f, 1.0f) * 10.0f);
        }
    }

    public string getNom()
    {
        return nom;
    }

    public void setNom(string nom)
    {
        this.nom = nom;
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

    public void setEmissionCO2(double emissionCO2)
    {
        this.emissionCO2 = emissionCO2;
    }

    private List<string> TextAssetToList(TextAsset ta)
    {
        var listToReturn = new List<string>();
        var arrayString = ta.text.Split('\n');
        foreach (var line in arrayString)
        {
            listToReturn.Add(line);
        }
        return listToReturn;
    }


    void randomizedCityData()
    {
        double randomConsommation = Random.Range(0, 100);
        double randomEmissionCO2 = Random.Range(0, 100);
        int nomId = Random.Range(0, 27537);
        var cities = Resources.Load<TextAsset>("cities");

        List<string> l = TextAssetToList(cities);
        //nom = File.ReadLines(cities).ElementAtOrDefault(nomId);
        nom = l[nomId];
        print(nom);

        apportElectricite = 0.0;
        consommation = randomConsommation;
        emissionCO2 = randomEmissionCO2;
        nbHabitants = Random.Range(0, 100);
    }

    public void showInfo()
    {
        GameObject.FindGameObjectWithTag("GraphManager").GetComponent<InfoPanel>().SendMessage("showConsumer", this);
    }

    public void isSelected(){
        this.gameObject.GetComponent<Outline>().OutlineWidth = 4.0f;
    }

    public void isNotSelected(){
        this.gameObject.GetComponent<Outline>().OutlineWidth = 0.0f;
    }

    public void ApplyEvent(NoRotationDockable e)
    {

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
