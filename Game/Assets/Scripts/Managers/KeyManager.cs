// Date   : 23.04.2017 11:09
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum Action
{
    None,
    Quit,
    Restart,
    CloseMenu,
    OpenMenu,
    Jump
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public Action action;
}

public class KeyManager : MonoBehaviour
{

    [SerializeField]
    private bool debug;

    public static KeyManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("KeyManager").Length == 0)
        {
            main = this;
            gameObject.tag = "KeyManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public bool GetKeyDown(Action action)
    {
        if (Input.GetKeyDown(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} pressed down to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public bool GetKeyUp(Action action)
    {
        if (Input.GetKeyUp(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} let up to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public bool GetKey(Action action)
    {
        if (Input.GetKey(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} held to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public KeyCode GetKeyCode(Action action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.key;
            }
        }
        return KeyCode.None;
    }

    public string GetKeyString(Action action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                string keyString = gameKey.key.ToString();
                if (gameKey.key == KeyCode.Return)
                {
                    keyString = "Enter";
                }
                else if (gameKey.key == KeyCode.RightControl)
                {
                    keyString = "Right Ctrl";
                }
                return keyString;
            }
        }
        return "";
    }
}
