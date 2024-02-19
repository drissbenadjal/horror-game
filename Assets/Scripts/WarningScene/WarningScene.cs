using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScene : MonoBehaviour
{
    //attendre 10 secondes et aller à la scène suivante
    void Start()
    {
        StartCoroutine(ChangementScene());
    }

    IEnumerator ChangementScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MenuScene");
    }
}
