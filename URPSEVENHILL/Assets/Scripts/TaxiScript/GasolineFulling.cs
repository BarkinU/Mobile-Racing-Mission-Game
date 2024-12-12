using System.Collections;
using UnityEngine;
using RoleShopSystem;

public class GasolineFulling : MonoBehaviour
{

    public TaxiGameManager taxiGameManage;
    TaxiShopData taxiShopData;
    private void Awake(){
        taxiShopData= ReadWriteAllRoles.ReadTaxiProp(taxiShopData);

    }
    private void OnTriggerStay (Collider oyuncu) {

        if (oyuncu.tag == "Player") {
            if(taxiGameManage.gasoline<taxiGameManage.gasolineCapacity){
               taxiGameManage.gasoline+=2*Time.fixedDeltaTime;
               if((int)taxiGameManage.gasoline==taxiGameManage.gasolineCapacity){
                   GasolineFullAchievement();
                
               }    
            }          
        }

        }
    private void GasolineFullAchievement(){
        taxiShopData.taxiAchievementItem.gasolineFullingValue++;
        Debug.Log("FillGasolineTankAchieveSaved");
        ReadWriteAllRoles.WriteTaxiProp(taxiShopData);
    }
   
}
