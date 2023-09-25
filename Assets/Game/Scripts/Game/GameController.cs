using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void GameOver()
    {
        StartCoroutine(WaitToLoadScene());
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level_1");
    }
}
