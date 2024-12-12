using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour {
    private void Start () {
       // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
        //PlayGamesPlatform.InitializeInstance (config);
        //PlayGamesPlatform.Activate ();   
        
        SignInGooglePlayGames ();
    }

    private void SignInGooglePlayGames () {
       Social.localUser.Authenticate(success => { });
    }

    private void SignoutGooglePlay () {
        //PlayGamesPlatform.Instance.SignOut ();
        SignInGooglePlayGames ();
    }

    #region  Achievements

    public static void UnlockAchievement (string id) {
        Social.ReportProgress (id, 100, success => { });
    }

    public static void IncrementAchievement (string id, int stepsToIncrement) {
        //PlayGamesPlatform.Instance.IncrementAchievement (id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI () {
        Social.ShowAchievementsUI ();
    }

    #endregion /Achievements

    #region LeaderBoards
    public static void AddScoreToLeaderBoard (string leaderBoardId, long score) {
        Social.ReportScore (score, leaderBoardId, success => { });
    }

    public static void ShowLeaderBoardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /LeaderBoards
}

