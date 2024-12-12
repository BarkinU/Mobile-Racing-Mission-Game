/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;


public class GeneralTools : Editor
{
    [MenuItem("Tools/General Tools/Change Asset Address to Asset Name")]
    public static void ChangeAddressToAssetName()
    {
        for(int j=0; j<Selection.objects.Length; j++)
        {
            EditorUtility.DisplayProgressBar("Converting asset addresses to asset name :" +((float)j) / ((float)Selection.objects.Length),"Converting", ((float)j) / ((float)Selection.objects.Length));
            AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.objects[j]))).address =Selection.objects[j].name;
        }

        EditorUtility.ClearProgressBar();
    }

}
*/