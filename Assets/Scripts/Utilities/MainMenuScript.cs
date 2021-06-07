using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
   
   [SerializeField] private GameObject menuPlayButton, menuExitButton;
   public void PlayTheGame()
   {
      SceneManager.LoadScene(Random.Range(1, 16));
      
      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuPlayButton);
   }
   
   public void QuitTheGame()
   {
      Debug.Log("QUIT THE GAME");
      Application.Quit();
      
      EventSystem.current.SetSelectedGameObject(null);
        
      EventSystem.current.SetSelectedGameObject(menuExitButton);
   }
}
