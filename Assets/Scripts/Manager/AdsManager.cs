using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public enum TypeAds
{
    video,
    rewardedVideo
}
public class AdsManager : MonoBehaviour
{
    public TypeAds currentTypeAds;
    string typeAds = "";
    string gameID = "3657987";/*colocar id*/
    ShowOptions adCallback;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            Advertisement.Initialize(gameID, false);
        else if (Application.platform == RuntimePlatform.WindowsEditor)
            Advertisement.Initialize(gameID, true);

        adCallback = new ShowOptions();
        adCallback.resultCallback = ResultAds;

        typeAds = currentTypeAds.ToString();
    }


    public void ShowAds()
    {
        if (Advertisement.IsReady())
            Advertisement.Show(typeAds, adCallback);
    }


    void ResultAds( ShowResult resultAds)
    {
        if (resultAds == ShowResult.Finished)
        {
            //RestartableManager.Instance.RestartFromLastWave();
            Main.Instance.sceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else if (resultAds == ShowResult.Skipped)
        {
            Main.Instance.sceneManager.LoadScene("Lose");
        }
        else if (resultAds == ShowResult.Failed)
        { }
    }

}
