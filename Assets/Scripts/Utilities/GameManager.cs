using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Handle Stages
    private static GameManager instance = null;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject pausingGameobject;
    /*List<int> level1 = new List<int> {1,2,3,4};
    List<int> level2 = new List<int> {5,6,7,8};
    List<int> level3 = new List<int> {9,10,11,12};
    List<int> level4 = new List<int> {13,14,15,16};*/
    private List<int> levelsToPlay_ = new List<int> {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};


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
            int nextLevelIndex = Random.Range(0, levelsToPlay_.Count);
        
            int nextLevel = levelsToPlay_[nextLevelIndex];
        
            levelsToPlay_.RemoveAt(nextLevelIndex);
            SceneManager.LoadScene(nextLevel);
            if (levelsToPlay_.Count <= 0)
            {
                SceneManager.LoadScene("Victory");
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
