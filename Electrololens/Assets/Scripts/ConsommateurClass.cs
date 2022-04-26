using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ConsommateurClass : MonoBehaviour
{
    private string nom;
    private int consommation;
    private int tauxDeSatisfaction;
    private int nbHabitants;

    [SerializeField]
    private GameObject infoVilleGO;
    private InfoVille infoVille;

    public string getNom()
    {
        return nom;
    }

    public int getConsommation()
    {
        return consommation;
    }

    public void setConsommation(int consommation)
    {
        this.consommation = consommation;
    }

    public int getTauxDeSatisfaction()
    {
        return tauxDeSatisfaction;
    }

    public void setTauxDeSatisfaction(int tauxDeSatisfaction)
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
        infoVille = infoVilleGO.GetComponent<InfoVille>();
        randomizedCityData();
    }

    void randomizedCityData()
    {
        int randomConsommation = Random.Range(0, 100);
        int randomTauxDeSatisfaction = Random.Range(0, 100);
        int randomNbHabitants = Random.Range(0, 100);

        consommation = randomConsommation;
        tauxDeSatisfaction = randomTauxDeSatisfaction;
        nbHabitants = randomNbHabitants;
    }

    public void showInfo()
    {
        /*        Debug.Log("Nom : " + nom);
                Debug.Log("Consommation : " + consommation);
                Debug.Log("Taux de satisfaction : " + tauxDeSatisfaction);
                Debug.Log("Nombre d'habitants : " + nbHabitants);*/
        print(infoVille);

        infoVille.updateLoadingBar(infoVille.getProgressConsoLoadingBar(), (float)consommation / 100);
        infoVille.updateLoadingBar(infoVille.getProgressConsoLoadingBar(), (float)tauxDeSatisfaction / 100);
        infoVille.updateLoadingBar(infoVille.getProgressEmissionLoadingBar(), (float)nbHabitants / 100);

        infoVilleGO.SetActive(true);
    }

    public void hideInfo()
    {
        infoVilleGO.SetActive(false);
    }
}
