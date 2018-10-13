// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    private Rigidbody2D rb2d;

    void Start()
    {
        Debug.Log("PlayerCharacterWasSpawned");
        rb2d = GetComponent<Rigidbody2D>();
        GameManager.main.SetUpPlayer(this);
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            GameManager.main.LoadNextLevel();
        }
        if (collision.tag == "SecretEnd")
        {
            Debug.Log("open secret level!");
            GameManager.main.LoadSecretLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DestructibleWall")
        {
            DestructibleWall wall = collision.gameObject.GetComponent<DestructibleWall>();
            if (wall != null && wall.AttemptToDestroy(collision.relativeVelocity.y))
            {
                Debug.Log("Paow!");
            }
            else
            {
                Debug.Log(string.Format("Not enough ({0})", collision.relativeVelocity.y));
            }
        }
    }
}
