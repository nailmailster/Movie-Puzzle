using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4075440";
    private string bannerID = "Banner_Android";
    private string interstitialID = "Interstitial_Android";

    public bool TestMode = false;
    public Button showInterstitial;
    public Text gemsAmt;

    void Start()
    {
        Advertisement.Initialize(gameID, TestMode);
        showInterstitial.interactable = Advertisement.IsReady(interstitialID);

        Advertisement.AddListener(this);
    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void ShowRewardedVideo()
    {

    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementID)
    {
        Debug.Log(placementID);
        if (placementID == interstitialID)
        {
            showInterstitial.interactable = true;
        }

        if (placementID == bannerID)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerID);
        }
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            GetReward();
        }
        else if (showResult == ShowResult.Skipped)
        {

        }
        else if (showResult == ShowResult.Failed)
        {

        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementID)
    {

    }

    public void GetReward()
    {
        if (PlayerPrefs.HasKey("gems"))
        {
            int gemAmount = PlayerPrefs.GetInt("gems");
            PlayerPrefs.SetInt("gems", gemAmount + 50);
        }
        else
        {
            PlayerPrefs.SetInt("gems", 50);
        }

        gemsAmt.text = "Gems " + PlayerPrefs.GetInt("gems").ToString();
    }
}
