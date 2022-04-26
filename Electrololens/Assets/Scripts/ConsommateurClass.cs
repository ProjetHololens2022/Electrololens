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
}
