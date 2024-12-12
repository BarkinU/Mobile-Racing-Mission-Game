using System.Collections;
using UnityEngine;
using RoleShopSystem;
using CarShopSystem;
using System.IO;


public static class ReadWriteAllRoles
{
    public static BusShopData ReadBusProp(BusShopData busShopData)
    {
        string busTempPath= Path.Combine(Application.persistentDataPath + "/BusShopData.txt");
        string busShopDataString = System.IO.File.ReadAllText (busTempPath);
        busShopDataString=SecureHelper.EncryptDecrypt(busShopDataString,SaveLoadData.key);
        busShopData = JsonUtility.FromJson<BusShopData> (busShopDataString);
        
        return busShopData;
    }

    public static GarbageShopData ReadGarbageProp(GarbageShopData garbageShopData)
    {
        string garbageTempPath= Path.Combine(Application.persistentDataPath + "/GarbageShopData.txt");
        string garbageShopDataString = System.IO.File.ReadAllText (garbageTempPath);
        garbageShopDataString=SecureHelper.EncryptDecrypt(garbageShopDataString,SaveLoadData.key);
        garbageShopData = JsonUtility.FromJson<GarbageShopData> (garbageShopDataString);
        
        return garbageShopData;
    }

    public static FireShopData ReadFireProp(FireShopData fireShopData)
    {
        string fireTempPath= Path.Combine(Application.persistentDataPath + "/FireShopData.txt");
        string fireShopDataString = System.IO.File.ReadAllText (fireTempPath);
        fireShopDataString=SecureHelper.EncryptDecrypt(fireShopDataString,SaveLoadData.key);
        fireShopData = JsonUtility.FromJson<FireShopData> (fireShopDataString);
        
        return fireShopData;
    }

    public static PoliceShopData ReadPoliceProp(PoliceShopData policeShopData)
    {
        string policeTempPath= Path.Combine(Application.persistentDataPath + "/PoliceShopData.txt");
        string policeShopDataString = System.IO.File.ReadAllText (policeTempPath);
        policeShopDataString=SecureHelper.EncryptDecrypt(policeShopDataString,SaveLoadData.key);
        policeShopData = JsonUtility.FromJson<PoliceShopData> (policeShopDataString);
        
        return policeShopData;
    }

    public static AmbulanceShopData ReadAmbulanceProp(AmbulanceShopData ambulanceShopData)
    {
        string ambulanceTempPath= Path.Combine(Application.persistentDataPath + "/AmbulanceShopData.txt");
        string ambulanceShopDataString = System.IO.File.ReadAllText (ambulanceTempPath);
        ambulanceShopDataString=SecureHelper.EncryptDecrypt(ambulanceShopDataString,SaveLoadData.key);
        ambulanceShopData = JsonUtility.FromJson<AmbulanceShopData> (ambulanceShopDataString);
        
        return ambulanceShopData;
    }

    public static TaxiShopData ReadTaxiProp(TaxiShopData taxiShopData)
    {
        string taxiTempPath= Path.Combine(Application.persistentDataPath + "/TaxiShopData.txt");
        string taxiShopDataString = System.IO.File.ReadAllText (taxiTempPath);
        taxiShopDataString=SecureHelper.EncryptDecrypt(taxiShopDataString,SaveLoadData.key);
        taxiShopData = JsonUtility.FromJson<TaxiShopData> (taxiShopDataString);
        
        return taxiShopData;
    }

    public static GameData ReadGameProp(GameData gameData)
    {
        string gameTempPath= Path.Combine(Application.persistentDataPath + "/GameData.txt");
        string gameDataString = System.IO.File.ReadAllText (gameTempPath);
        gameDataString=SecureHelper.EncryptDecrypt(gameDataString,SaveLoadData.key);
        gameData = JsonUtility.FromJson<GameData> (gameDataString);
        
        return gameData;
    }

    public static CarShopData ReadCarProp(CarShopData carShopData)
    {
        string carTempPath= Path.Combine(Application.persistentDataPath + "/CarShopData.txt");
        string carShopDataString = System.IO.File.ReadAllText (carTempPath);
        carShopDataString=SecureHelper.EncryptDecrypt(carShopDataString,SaveLoadData.key);
        carShopData = JsonUtility.FromJson<CarShopData> (carShopDataString);
        
        return carShopData;
    }

    public static void WriteBusProp(BusShopData busShopData)
    {
        string busShopDataString = JsonUtility.ToJson(busShopData);
        string busTempPath= Path.Combine(Application.persistentDataPath + "/BusShopData.txt");
        System.IO.File.WriteAllText(busTempPath,SecureHelper.EncryptDecrypt(busShopDataString,SaveLoadData.key));
        
    }

    public static void WriteGarbageProp(GarbageShopData garbageShopData)
    {
        string garbageShopDataString = JsonUtility.ToJson(garbageShopData);
        string garbageTempPath= Path.Combine(Application.persistentDataPath + "/GarbageShopData.txt");
        System.IO.File.WriteAllText(garbageTempPath,SecureHelper.EncryptDecrypt(garbageShopDataString,SaveLoadData.key));
        
    }

    public static void WriteFireProp(FireShopData fireShopData)
    {
        string fireShopDataString = JsonUtility.ToJson(fireShopData);
        string fireTempPath= Path.Combine(Application.persistentDataPath + "/FireShopData.txt");
        System.IO.File.WriteAllText(fireTempPath,SecureHelper.EncryptDecrypt(fireShopDataString,SaveLoadData.key));
        
    }

    public static void WritePoliceProp(PoliceShopData policeShopData)
    {
        string policeShopDataString = JsonUtility.ToJson(policeShopData);
        string policeTempPath= Path.Combine(Application.persistentDataPath + "/PoliceShopData.txt");
        System.IO.File.WriteAllText(policeTempPath,SecureHelper.EncryptDecrypt(policeShopDataString,SaveLoadData.key));
        
    }

    public static void WriteAmbulanceProp(AmbulanceShopData ambulanceShopData)
    {
        string ambulanceShopDataString = JsonUtility.ToJson(ambulanceShopData);
        string ambulanceTempPath= Path.Combine(Application.persistentDataPath + "/AmbulanceShopData.txt");
        System.IO.File.WriteAllText(ambulanceTempPath,SecureHelper.EncryptDecrypt(ambulanceShopDataString,SaveLoadData.key));
        
    }

    public static void WriteTaxiProp(TaxiShopData taxiShopData)
    {
        string taxiShopDataString = JsonUtility.ToJson(taxiShopData);
        string taxiTempPath= Path.Combine(Application.persistentDataPath + "/TaxiShopData.txt");
        System.IO.File.WriteAllText(taxiTempPath,SecureHelper.EncryptDecrypt(taxiShopDataString,SaveLoadData.key));
        
    }

    public static void WriteGameProp(GameData gameData)
    {
        string gameDataString = JsonUtility.ToJson(gameData);
        string gameTempPath= Path.Combine(Application.persistentDataPath + "/GameData.txt");
        System.IO.File.WriteAllText(gameTempPath,SecureHelper.EncryptDecrypt(gameDataString,SaveLoadData.key));
        
    }

    public static void WriteCarProp(CarShopData carShopData)
    {
        string carShopDataString = JsonUtility.ToJson(carShopData);
        string carTempPath= Path.Combine(Application.persistentDataPath + "/CarShopData.txt");
        System.IO.File.WriteAllText(carTempPath,SecureHelper.EncryptDecrypt(carShopDataString,SaveLoadData.key));
        
    }
    
}
