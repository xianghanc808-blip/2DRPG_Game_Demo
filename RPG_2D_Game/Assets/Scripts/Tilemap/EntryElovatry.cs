using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntryElovatry : MonoBehaviour
{
    //public TilemapCollider2D[] mountainCollision;
    //public Collider2D[] boundaryCollision;

    public GameObject mountainGroupCollision;
    public GameObject boundaryGroupCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //foreach (TilemapCollider2D tilemapCollider2D in mountainCollision)
            //{
            //    tilemapCollider2D.enabled = false;
            //}

            //foreach(Collider2D collider2D in boundaryCollision)
            //{
            //    collider2D.enabled = true;
            //}
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 12;
            mountainGroupCollision.SetActive(true);
            boundaryGroupCollision.SetActive(false);
        }
    }
    
}
