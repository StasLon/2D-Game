using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlChanger : MonoBehaviour
{
    public void NextLvl()
    {
    UnityEngine.SceneManagement. SceneManager.LoadScene(1);
    }
}
