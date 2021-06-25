using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    [SerializeField] Text enemyCountText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCountText.text = "Enemies : " + enemies.Length.ToString();
    }
}
