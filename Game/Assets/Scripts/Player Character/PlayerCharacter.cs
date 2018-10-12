// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

    void Start () {
        Debug.Log("PlayerCharacterWasSpawned");
        GameManager.main.SetUpPlayer(this);
    }

    void Update () {
    
    }
}
