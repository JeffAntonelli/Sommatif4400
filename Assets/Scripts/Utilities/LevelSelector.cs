using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private readonly List<int> scenes_;

    private static LevelSelector instanceLevelSelector_;

    private LevelSelector()
    {
        scenes_ = new List<int>()
        {
            1, 2, 3, 4
        };
    }

    public static LevelSelector InstanceLevelSelector
    {
        get
        {
            if (instanceLevelSelector_ == null)
            {
                instanceLevelSelector_ = new LevelSelector();
            }
            return instanceLevelSelector_;
        }
    }

    public void LoadNextLevel()
    {
        if (scenes_.Count == 0)
            return;
        int randomIndex = UnityEngine.Random.Range(0, scenes_.Count);
        int currentScenes = scenes_[randomIndex];
        scenes_.RemoveAt(randomIndex);
        SceneManager.LoadScene(currentScenes);
    }

    void Start()
    {
        instanceLevelSelector_.LoadNextLevel();
    }

    void Update()
    {
        
    }
}
