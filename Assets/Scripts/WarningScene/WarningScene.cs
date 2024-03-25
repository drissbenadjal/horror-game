using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScene : MonoBehaviour
{
    void Start()
    {
        //wait 5 seconds before loading the menu scene
        StartCoroutine(ChangementScene());
    }

    IEnumerator ChangementScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MenuScene");
    }
}
