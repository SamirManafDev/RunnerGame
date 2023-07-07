using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ads : MonoBehaviour
{
    public static Ads Instance;
    public string rewardedAdId;
    public string bannerId;
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    public PlayerController playerController;
    private void Awake()
    {
        playerController=FindObjectOfType<PlayerController>();
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        });
        
        LoadRewardedAd();
    }

    public void RequestBanner()  // public edirik UI tapmaq rahatdi
    {
        bannerView = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.BottomRight);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        Debug.Log("Yeni rewarded ad yuklendi");


        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(rewardedAdId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }
    //public void ShowBanner()
    //{
    //    RequestBanner();
    //}
    public void ShowRewardedAd()
    {
        LoadRewardedAd(); //Her defe bariere deyende reklam cixsin.Ban riskini artirir
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                playerController.showAdds.SetActive(false);
                Time.timeScale = 1;
                playerController.gameObject.transform.position=new Vector3(0,0,playerController.gameObject.transform.position.z+3 );
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }
}
