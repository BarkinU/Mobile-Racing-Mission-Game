using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FreeToBus : MonoBehaviour
{

    public GameObject questQuestion;
  public Button yesButton;
  public Button noButton;

  private void Start()
  {
    yesButton.onClick.AddListener (() => JobAcceptionBus ());
    noButton.onClick.AddListener(() => JobRejection());
  }
    private void OnTriggerEnter(Collider oyuncu){
      
        if(oyuncu.CompareTag("Player")){
          questQuestion.SetActive(true);
          Time.timeScale=0f;
        }
    }

    private void JobAcceptionBus()
    {
      Time.timeScale=1f;
      PlayerPrefs.SetInt("isGarage",1);
      PlayerPrefs.SetInt("rolePointer",1);
      StartCoroutine(BusEnteringCoroutine());
    }

    private void JobRejection()
    {
      questQuestion.SetActive(false);
      Time.timeScale=1f;
    }

    IEnumerator BusEnteringCoroutine(){
      yield return new  WaitForSeconds(0.3f);
      SceneLoader.Load(SceneLoader.Scene.AwakeScene);
      
    }
}
