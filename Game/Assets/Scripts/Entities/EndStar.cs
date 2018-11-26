// Date   : 22.11.2018 18:50
// Project: PortableLoopingMachine
// Author : bradur

using UnityEngine;
using System.Collections;

public class EndStar : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    public void Die()
    {
        // animate, probably!
        Destroy(gameObject);
    }
}
