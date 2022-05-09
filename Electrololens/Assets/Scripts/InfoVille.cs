using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Threading.Tasks;

public class InfoVille : MonoBehaviour
{
    [SerializeField]
    private GameObject progressConso = null;
    [SerializeField]
    private GameObject progressApport = null;
    [SerializeField]
    private GameObject progressEmission = null;

    private IProgressIndicator progressConsoLoadingBar;
    private IProgressIndicator progressApportLoadingBar;
    private IProgressIndicator progressEmissionLoadingBar;

    public double consommation
    // private int 


    void Start()
    {
        progressConsoLoadingBar = progressConso.GetComponent<IProgressIndicator>();
        progressApportLoadingBar = progressApport.GetComponent<IProgressIndicator>();
        progressEmissionLoadingBar = progressEmission.GetComponent<IProgressIndicator>();
    }
    
    public IProgressIndicator getProgressConsoLoadingBar()
    {
        return progressConsoLoadingBar;
    }

    public IProgressIndicator getProgressApportLoadingBar()
    {
        return progressApportLoadingBar;
    }

    public IProgressIndicator getProgressEmissionLoadingBar()
    {
        return progressEmissionLoadingBar;
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

    void modifyDiag(double val, string unit, ModifyDiagram modDiag)
    {
        modDiag.updateValue(val, unit);
    }

    void modifyForeground(double val, ModifyDiagram modDiag)
    {
        modDiag.updateForegroud(val);
    }

}
