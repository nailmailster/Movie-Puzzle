using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{
    string gameID = "4075440";
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
    }
}
