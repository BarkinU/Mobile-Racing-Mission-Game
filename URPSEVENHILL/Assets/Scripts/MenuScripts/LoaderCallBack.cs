using System.Collections;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate=true;

    private void Update()
    {
        if(isFirstUpdate){
            isFirstUpdate=false;
            SceneLoader.LoaderCallBack();
        }
    }
}
