using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Util.Constants;

public class HomeManager : MonoBehaviour
{
    

    private void Start()
    {

    }

    private void Update()
    {
        
    }
    public void OnStartClick ()
    {
        if (CheckIsFirstTimeOpen())
        {
            SceneManager.LoadScene(Tutorial);
        }
        else
        {
            SceneManager.LoadScene(Game);
        }
    }

    private bool CheckIsFirstTimeOpen()
    {
        if (PlayerPrefs.HasKey(FIRST_TIME_KEY))
        {
            if (PlayerPrefs.GetInt(FIRST_TIME_KEY, 1) == 1)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
        else
        {
            return false; 
        }
    }
}
