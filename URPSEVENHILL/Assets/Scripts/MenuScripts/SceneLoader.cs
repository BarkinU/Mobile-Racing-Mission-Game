using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SceneLoader
{
    private class LoadingMonoBehaviour : MonoBehaviour{ }
    public enum Scene{
        AwakeScene,
        Loading,
        FreeDriveScene,
        RealScene
    }

    private static Action onLoaderCallBack;
    private static AsyncOperation loadingAsyncOperation;
    public static void Load(Scene scene){
        //Set the loader callback action to load the target scene
        onLoaderCallBack = () => {
            GameObject loadingGameObject = new GameObject("Loading Game Oject");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        //Load the loading scene   
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static IEnumerator LoadSceneAsync(Scene scene){
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while(!loadingAsyncOperation.isDone){
            yield return null;
        }
    }

    public static float GetLoadingProgress(){
        if(loadingAsyncOperation != null){
            return loadingAsyncOperation.progress;
        } else{
            return 1f;
        }
    }
    public static void LoaderCallBack(){
        //Triggered after the first update which lets the screen refresh
        //Execute the loader callback action which will load the target scene
        if(onLoaderCallBack != null){
            onLoaderCallBack();
            onLoaderCallBack=null;
        }
    }
}
