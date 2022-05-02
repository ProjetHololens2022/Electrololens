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
    // private int 


    void Start()
    {
        Debug.LogWarning("start init infoville");
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

    public async void updateLoadingBar(IProgressIndicator progressIndicator, float progress)
    {
        if (progressIndicator != null)
        {
            Debug.Log("update : " + progressIndicator.State);
            await progressIndicator.AwaitTransitionAsync();

            if (progressIndicator.State != ProgressIndicatorState.Open 
                && progressIndicator.State != ProgressIndicatorState.Opening)
            {
                await progressIndicator.OpenAsync();
                progressIndicator.Progress = progress;
            }
            Debug.Log("after update : " + progressIndicator.State);
        }
    }

    public async void closeProgressAsync(IProgressIndicator progressIndicator)
    {
        if(progressIndicator != null)
        {
            Debug.Log("before close : " + progressIndicator.State);
            await progressIndicator.AwaitTransitionAsync();

            if(progressIndicator.State != ProgressIndicatorState.Closed 
                && progressIndicator.State != ProgressIndicatorState.Closing)
            {
                await progressIndicator.CloseAsync();
            }
            Debug.Log("after close : " + progressIndicator.State);
        }
    }

}
