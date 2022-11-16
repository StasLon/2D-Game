using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager : MonoBehaviour
{
   
   [SerializeField] private GameObject gameOverScreen;

   [SerializeField] AudioClip gameOverSound;
   private Animator anim;
  


private void Awake() 
{
    gameOverScreen.SetActive(false);
    anim = GetComponent<Animator>();
}

    public void FadeTolevel()
        {
            anim.SetTrigger("fade");
            
        }
    public void OnFadeComplete()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    

    public void Restart()
    {
        
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        
      UnityEngine.SceneManagement. SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; 
        
    }

    public void NextLvl()
    {
        anim.SetTrigger("fade");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        
    }


}