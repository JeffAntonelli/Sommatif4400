using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class GameManager4 : MonoBehaviour
{
    //Handle Stages
    private static GameManager4 instance = null;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject pausingGameobject;
    [SerializeField] Text enemyCountText;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("enemy");
        enemyCountText.text = "Enemies : " + enemy.Length.ToString();

        if (enemy.Length == 0)
        {
            SceneManager.LoadScene("Victory");
            
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            StartingPausingMenu();
        }
    }
    
    private void StartingPausingMenu()
    {
        pausingGameobject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
 
    public void ReturnGame() 
    {
        Time.timeScale = 1;
        pausingGameobject.SetActive(false);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
