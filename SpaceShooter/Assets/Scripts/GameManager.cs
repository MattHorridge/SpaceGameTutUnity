using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool IsGameOver = false;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && IsGameOver)
        {
            SceneManager.LoadScene(1); 
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
}
