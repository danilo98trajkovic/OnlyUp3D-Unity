using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour {
    public InterstitialAd interstitial;
    public BannerView bannerView;
    public RewardedAd rewarded;
    bool loadGo = false;
    bool canContinue;
    bool rewardedLoaded;
    bool canShow = false;

#if UNITY_ANDROID
    //init
    //private string initAd = "ca-app-pub"; //android
    private string initAd = "ca-app-pub-3940256099942544/1033173712"; //test ad
#elif UNITY_IPHONE
    //init
    private string initAd = ""; //iOS
    //private string initAd = "ca-app-pub-3940256099942544/4411468910"; //test ad
#else
    private string initAd = "";
    private string rewardedAd = "";
#endif


    private static Admob instance = null;


    void Awake() {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelWasLoaded(int level) {
        if (SceneManager.GetActiveScene().name == "Menu" ) {
            ShowBanner();
        } else if (SceneManager.GetActiveScene().name == "Level"){
            HideBanner();
        }
    }


    private void Start() {
        canContinue = false;
        rewardedLoaded = false;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            canContinue = true;
        } else {
            bannerView = new BannerView("", AdSize.Banner, AdPosition.Top);
        }

        if (canContinue) {

            RequestAdBanner();

            LoadNewAd();

            StartCoroutine(WaitAndCanShowAd(1f));

        }
    }


    public void LoadNewAd() {
        if (canContinue) {
            AdRequest request = new AdRequest();
            InterstitialAd.Load(initAd, request,
              (InterstitialAd ad, LoadAdError error) => {

                  if (error != null || ad == null) {
                      Debug.LogError("interstitial ad failed to load: " + error);
                      StartCoroutine(WaitAndLoadAd());
                      return;
                  }

                  Debug.Log("Interstitial ad loaded: " + ad.GetResponseInfo());

                  interstitial = ad;
              });
        }
    }

    private void RequestAdBanner() {
        if (canContinue) {
            string adUnitId = "";

            if (Application.platform == RuntimePlatform.Android)
                //adUnitId = "ca-app-pub";
                adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test ad
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                adUnitId = "";
                //adUnitId = "ca-app-pub-3940256099942544/2934735716"; //test ad


            if (adUnitId != "") {
                bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
                AdRequest request = new AdRequest();

                bannerView.LoadAd(request);
                //bannerView.Hide();
            }
        }
    }

    public void ShowBanner() {
        if (canContinue) {
            bannerView.Show();
        }
    }

    public void HideBanner() {
        if (canContinue) {
            bannerView.Hide();
        }
    }

    public void ShowAdInit() {
        if (canContinue) {
            if (interstitial != null) {
                if (interstitial.CanShowAd() && canShow) {
                    interstitial.Show();
                    canShow = false;

                    StartCoroutine(WaitAndLoadAd());
                    StartCoroutine(WaitAndCanShowAd(35f));
                }
            }
        }
    }

    IEnumerator WaitAndLoadAd() {
        yield return new WaitForSeconds(2f);

        LoadNewAd();
    }

    IEnumerator WaitAndCanShowAd(float timeWait) {
        yield return new WaitForSeconds(timeWait);

        canShow = true;
    }
}
