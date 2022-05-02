using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ConsommateurClass : MonoBehaviour
{
    private string nom;
    private double consommation;
    private double apportElectricite;
    private double emissionCO2;
    private double tauxDeSatisfaction;
    private int nbHabitants;



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

    public void ApplyEvent(string e)
    {
        switch (e)
        {
            case "Event1":
                consommation += 20.0;
                emissionCO2 += 20;
                tauxDeSatisfaction += 10;
                nbHabitants += 10;
                break;
            case "Event2":
                apportElectricite -= 20;
                emissionCO2 -= 20;
                break;
            case "Event3":
                consommation += 10.0;
                emissionCO2 += 10;
                break;
            case "Event4":
                consommation += 15;
                nbHabitants += 5;
                break;
        }
    }

}
