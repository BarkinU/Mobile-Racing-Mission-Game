using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLeaderBoardAndAchievements : MonoBehaviour
{
    public static ShowLeaderBoardAndAchievements instance { get; private set;}

    private void Start()
    {
        instance=this;
    }
    public void ShowLeaderBoards()
    {
        GPGSManager.ShowLeaderBoardsUI();
    }

    public void ShowAchievements()
    {
        GPGSManager.ShowAchievementsUI();
    }
}
