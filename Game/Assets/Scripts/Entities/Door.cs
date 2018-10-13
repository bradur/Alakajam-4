// Date   : 13.10.2018 12:59
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [SerializeField]
    private KeyType requiredKeyType;
    public KeyType RequiredKeyType { get { return requiredKeyType; } }

    public void Open()
    {
        Destroy(gameObject);
    }
}
