using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Image Image;
    private void Awake()
    {
        Image=transform.GetComponent<Image>();
    }

    private void Update()
    {
        Image.fillAmount=SceneLoader.GetLoadingProgress();
    }
}
