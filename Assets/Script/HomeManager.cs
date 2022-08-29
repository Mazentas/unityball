using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    AudioSource audioData;

    private void Start()
    {
        //audioData = GetComponent<AudioSource>();
        //audioData.Play(0);
        //Debug.Log("started");
    }

    private void Update()
    {
        
    }
    public void OnStartClick ()
    {
        SceneManager.LoadScene("Game");
    }
}
