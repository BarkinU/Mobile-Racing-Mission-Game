using System.Collections;
using RoleShopSystem;
using UnityEngine;

public class WaterTankFulling : MonoBehaviour {

    public FireGameManager fireGameManage;
    private FireShopData fireShopData;


    private void OnTriggerStay (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            if (fireGameManage.water < fireGameManage.waterTankCapacity) {
                fireGameManage.water += 5 * Time.fixedDeltaTime;
                if ((int) fireGameManage.water == fireGameManage.waterTankCapacity) {
                    WaterTankFullAchievement ();

                }
            }
        }

    }
    private void WaterTankFullAchievement () {
        fireShopData=ReadWriteAllRoles.ReadFireProp(fireShopData);
        fireShopData.fireAchievementItem.xFillWaterTankValue++;
        Debug.Log ("FillWaterTankAchieveSaved");
        
        ReadWriteAllRoles.WriteFireProp(fireShopData);
    }

}