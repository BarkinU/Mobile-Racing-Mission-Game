using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RoleShopSystem {
public class GameRoleShopUI : MonoBehaviour
{
 public GameData gameData;
 public SaveLoadData saveLoadData;

 public TextMeshProUGUI totalXpText;
 public TextMeshProUGUI totalMoneyText;

 private void Start(){

     totalXpText.text = " " + gameData.totalXp;
     totalMoneyText.text = " " + gameData.totalMoney; 
     
 }
 
}

}