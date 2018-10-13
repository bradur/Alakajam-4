// Date   : 13.10.2018 09:31
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;

public class FollowTargetSnapToGrid : MonoBehaviour
{


    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;

    [SerializeField]
    private bool followZ = false;

    private Transform target;

    private Vector3 newPosition;

    private bool following = true;
    private bool looping = false;
    private int areaX;
    private int areaY;

    [SerializeField]
    float end = 4.55f;
    [SerializeField]
    float start = -2.55f;
    [SerializeField]
    float endY = 4.5f;
    [SerializeField]
    float startY = -2.5f;

    [SerializeField]
    private float loopingButtonMinInterval = 1f;
    private float loopingButtonTimer = 0f;

    [SerializeField]
    private LoopAreaCreator loopAreaCreator;

    private void Start()
    {
        //aspectRatio = Screen.width / Screen.height;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (looping)
        {
            Vector3 newPos = target.transform.position;
            float difX = newPos.x - transform.position.x;
            float difY = newPos.y - transform.position.y;

            if (difX > end || difX < start || difY > endY || difY < startY)
            {
                if (difX > end)
                {
                    newPos.x -= 7f;
                }
                else if (difX < start)
                {
                    newPos.x += 7f;
                }
                if (difY > endY)
                {
                    newPos.y -= 7f;
                }
                else if (difY < startY)
                {
                    newPos.y += 7f;
                }
                SoundManager.main.PlaySound(SoundType.Teleport);
                target.transform.position = newPos;
            }
            //Debug.Log();
        }
    }

    private void Update()
    {
        if (target != null)
        {
            if (following)
            {
                newPosition = transform.position;
                if (followX)
                {
                    newPosition.x = Mathf.FloorToInt(target.position.x - 0.5f);
                }
                if (followY)
                {
                    newPosition.y = Mathf.FloorToInt(target.position.y - 0.5f);
                }
                if (followZ)
                {
                    newPosition.z = Mathf.FloorToInt(target.position.z - 0.5f);
                }

                transform.position = newPosition;
            }
            else if (looping)
            {
                loopingButtonTimer += Time.deltaTime;
            }

        }
        if (KeyManager.main.GetKeyDown(Action.CreateLoopArea))
        {
            if (!looping)
            {
                following = false;
                looping = true;
                Debug.Log("Creating area!");
                areaX = (int)transform.position.x + 1;
                areaY = (int)transform.position.y + 1;
                loopAreaCreator.Initialize(areaX, areaY);
                GameManager.main.SetCameraTarget(null);
            }
            else if (loopingButtonTimer > loopingButtonMinInterval)
            {
                loopingButtonTimer = 0f;
                looping = false;
                following = true;
                loopAreaCreator.Clear();
                GameManager.main.SetCameraTarget(target);
            }
        }
    }
}
