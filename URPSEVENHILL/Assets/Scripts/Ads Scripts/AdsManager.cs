/*
using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour {
    public TextMeshProUGUI adText;
    string App_ID = "ca-app-pub-6087247198248059~6137775898";
    string Interstitial_AD_ID = "ca-app-pub-3940256099942544/1033173712";
    string Rewarded_AD_ID = "ca-app-pub-3940256099942544/5224354917";

    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    void Start () {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial () {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd (Interstitial_AD_ID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder ().Build ();
        this.interstitial.LoadAd (request);

        ShowInterstitialAd();

    }

    public void ShowInterstitialAd () {
        if (this.interstitial.IsLoaded ()) {
            this.interstitial.Show ();
        }
    }

    public void RequestRewardBasedVideo () {
        this.rewardedAd = new RewardedAd (Rewarded_AD_ID);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder ().Build ();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd (request);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        ShowRewardedAd();
    }

    public void ShowRewardedAd () {
        if (this.rewardedAd.IsLoaded ()) {
            this.rewardedAd.Show ();
        }
    }

    public void HandleOnAdLoaded (object sender, EventArgs args) {
        adText.text = "Ad Loaded";
    }

    public void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
        adText.text = "AdFailedToLoad";
    }

    public void HandleOnAdOpened (object sender, EventArgs args) {
        MonoBehaviour.print ("HandleAdOpened event received");
    }

    public void HandleOnAdClosed (object sender, EventArgs args) {
        MonoBehaviour.print ("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication (object sender, EventArgs args) {
        MonoBehaviour.print ("HandleAdLeavingApplication event received");
    }


    //////////////////////////////////


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

}
*/