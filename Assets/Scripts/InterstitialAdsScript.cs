using UnityEngine;
using UnityEngine.Advertisements;
// using UnityEngine.SceneManagement;

public class InterstitialAdsScript : MonoBehaviour
{
    string gameID = "4075440";
    bool testMode = false;
    string interstitialID = "Interstitial_Android";

    void Start()
    {
        // int buildIndex = SceneManager.GetActiveScene().buildIndex;
        Advertisement.Initialize(gameID, testMode);

        // if (buildIndex > 0 && buildIndex < 6)
            ShowInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
            Advertisement.Show(interstitialID);
    }
}
