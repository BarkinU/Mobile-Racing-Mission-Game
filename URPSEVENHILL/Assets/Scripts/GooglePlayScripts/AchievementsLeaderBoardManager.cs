using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsLeaderBoardManager : MonoBehaviour
{
    public static AchievementsLeaderBoardManager Instance { get; private set;}

    void Start()
    {
        Instance =this; 
    }
}
