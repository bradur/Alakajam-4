// Date   : 13.10.2018 12:44
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public enum KeyType
{
    None,
    Red,
    Purple,
    Yellow
}

public class CollectableKey : MonoBehaviour {

    [SerializeField]
    private KeyType keyType;

    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private CircleCollider2D cc2D;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Light light;

    public KeyType KeyType { get { return keyType; } }

    void Start () {
    
    }

    void Update () {
    
    }

    void Hide()
    {
        sr.enabled = false;
        cc2D.enabled = false;
        animator.enabled = false;
        light.enabled = false;
    }

    public CollectableKey Collect()
    {
        Hide();
        return this;
    }
}
