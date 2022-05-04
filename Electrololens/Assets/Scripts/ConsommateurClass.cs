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
        return consommation;
    }

    public void setConsommation(double consommation)
    {
        this.consommation = consommation;
    }

    public double GetApportElectricite(){
        return apportElectricite;
    }

    public void SetApportElectricite(double apportElectricite){
        this.apportElectricite = apportElectricite;
    }

    public double getTauxDeSatisfaction()
    {
        return tauxDeSatisfaction;
    }

    public void setTauxDeSatisfaction(double tauxDeSatisfaction)
    {
        this.tauxDeSatisfaction = tauxDeSatisfaction;
    }

    public int getNbHabitants()
    {
        return nbHabitants;
    }

    public void setNbHabitants(int nbHabitants)
    {
        this.nbHabitants = nbHabitants;
    }

    void Start()
    {
        // TODO : On start infoville's ProgressBar should be init
        // It's not done, and it's why on the first attempt the values are 50% everywhere.
       
        infoVille = infoVilleGO.GetComponent<InfoVille>();
        randomizedCityData();
        updateProgessValues();
    }

    void Update()
    {
        if(!isConnected || apportElectricite < consommation){
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1.0f,0.0f,0.0f,1.0f)*10.0f);
        } else {
            this.transform.Find("Sphere").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f,1.0f,0.0f,1.0f)*10.0f);
        }
    }

    void randomizedCityData()
    {
        double randomConsommation = Random.Range(0, 100);
        double randomApportElectriciten = Random.Range(0, 100);
        double randomEmissionCO2 = Random.Range(0, 100);

        consommation = randomConsommation;
        apportElectricite = randomApportElectriciten;
        emissionCO2 = randomEmissionCO2;
    }

    public void updateProgessValues()
    {
        if (infoVille != null 
            && infoVille.getProgressApportLoadingBar() != null
                && infoVille.getProgressConsoLoadingBar() != null
                    && infoVille.getProgressConsoLoadingBar() != null)
        {
            infoVille.updateLoadingBar(infoVille.getProgressConsoLoadingBar(), consommation / 100);
            infoVille.updateLoadingBar(infoVille.getProgressApportLoadingBar(), apportElectricite / 100);
            infoVille.updateLoadingBar(infoVille.getProgressEmissionLoadingBar(), emissionCO2 / 100);
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
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CDM:
                consommation += 20.0;
                emissionCO2 += 20;
                tauxDeSatisfaction += 10;
                nbHabitants += 10;
                break;
            case TypeEvent.HEUREPOINTE:
                apportElectricite -= 20;
                emissionCO2 -= 20;
                break;
            case TypeEvent.EUROVISION:
                consommation += 15;
                nbHabitants += 5;
                break;
        }
    }

    public void RemoveEvent(NoRotationDockable e)
    {
        TypeEvent te = e.GetComponent<EventDockable>().typeEvent;

        switch (te)
        {
            case TypeEvent.CDM:
                consommation -= 20.0;
                emissionCO2 -= 20;
                tauxDeSatisfaction -= 10;
                nbHabitants -= 10;
                break;
            case TypeEvent.HEUREPOINTE:
                apportElectricite += 20;
                emissionCO2 += 20;
                break;
            case TypeEvent.EUROVISION:
                consommation -= 15;
                nbHabitants -= 5;
                break;
        }
    }

}
