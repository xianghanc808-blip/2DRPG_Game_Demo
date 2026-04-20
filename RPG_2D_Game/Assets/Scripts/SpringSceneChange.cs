using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpringSceneChange : MonoBehaviour
{
    public string sceneToLoad;

    public Vector2 borthPosition;
    private Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;

           
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad);
            ao.completed += ChangePlayerPosition;
        }
    }

    private void ChangePlayerPosition(AsyncOperation operation)
    {
        player.position = new Vector2( borthPosition.x, borthPosition.y);
    }

    IEnumerator  DelayFade()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }


}
