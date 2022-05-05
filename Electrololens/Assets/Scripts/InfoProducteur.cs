using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Threading.Tasks;

public class InfoProducteur : MonoBehaviour
{
    [SerializeField]
    private GameObject progressConso = null;
    [SerializeField]
    private GameObject progressEmission = null;
    [SerializeField]
    private GameObject progressEtat = null;

    private IProgressIndicator progressConsoLoadingBar;
    private IProgressIndicator progressEmissionLoadingBar;
    private IProgressIndicator progressEtatLoadingBar;


    void Start()
    {
        progressConsoLoadingBar = progressConso.GetComponent<IProgressIndicator>();
        progressEmissionLoadingBar = progressEmission.GetComponent<IProgressIndicator>();
        progressEtatLoadingBar = progressEtat.GetComponent<IProgressIndicator>();
    }
    
    public IProgressIndicator getProgressConsoLoadingBar()
    {
        return progressConsoLoadingBar;
    }

    public IProgressIndicator getProgressEmissionLoadingBar()
    {
        return progressEmissionLoadingBar;
    }

    public IProgressIndicator getProgressEtatLoadingBar()
    {
        return progressEtatLoadingBar;
    }

    public async void updateLoadingBar(IProgressIndicator progressIndicator, double progress)
    {
        if (progressIndicator != null)
        {
            await progressIndicator.AwaitTransitionAsync();

            if (progressIndicator.State != ProgressIndicatorState.Open 
                && progressIndicator.State != ProgressIndicatorState.Opening)
            {
                await progressIndicator.OpenAsync();
                progressIndicator.Progress = (float) progress;
            }
        }
    }

    public async void closeProgressAsync(IProgressIndicator progressIndicator)
    {
        if(progressIndicator != null)
        {
            await progressIndicator.AwaitTransitionAsync();

            if(progressIndicator.State != ProgressIndicatorState.Closed 
                && progressIndicator.State != ProgressIndicatorState.Closing)
            {
                await progressIndicator.CloseAsync();
            }
        }
    }

}
