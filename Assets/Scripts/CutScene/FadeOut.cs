using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("ENDGAME");
        SceneManager.LoadScene(2);
    }
}
