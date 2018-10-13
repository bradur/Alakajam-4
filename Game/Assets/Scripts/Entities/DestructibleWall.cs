// Date   : 13.10.2018 16:41
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour {

    [SerializeField]
    private float requiredSpeed;

    void Start () {
    
    }

    void Update () {
    
    }

    public bool AttemptToDestroy(float speed)
    {
        if (speed >= requiredSpeed)
        {
            Debug.Log("I'm being destroyed...");
            Destroy(gameObject);
            Debug.Log("I was destroyed!");
            return true;
        }
        return false;
    }
}
