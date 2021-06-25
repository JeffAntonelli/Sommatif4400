using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager2 : MonoBehaviour
{
    //Handle Stages
    private static GameManager2 instance = null;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject pausingGameobject;


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
        
        if (enemy.Length == 0)
        {
            SceneManager.LoadScene(Random.Range(5,8));
            
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
