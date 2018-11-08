// Date   : 02.11.2018 20:10
// Project: PortableLoopingMachine
// Author : bradur

using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour {

    //private VirtualCamera
    [SerializeField]
    private CinemachineVirtualCamera virtualEffectCamera;
    [SerializeField]
    private CinemachineVirtualCamera virtualPlayerCamera;

    [SerializeField]
    private PlayableDirector levelEndPlayable;
    [SerializeField]
    private PlayableDirector secretLevelEndPlayable;

    void Start () {
    
    }

    void Update () {
    
    }

    public void SwitchToEffect()
    {
        virtualEffectCamera.Priority = 11;
    }

    public void SwitchToPlayer()
    {
        virtualEffectCamera.Priority = 5;
    }

    public void StartLevelEndTimeLine()
    {
        levelEndPlayable.Play();
    }

    public void StartSecretLevelEndTimeLine()
    {
        secretLevelEndPlayable.Play(); ;
    }
}
