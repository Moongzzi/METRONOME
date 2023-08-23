using MET.Util;
using MET.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : Singleton<IngameManager>
{
    public CharacterControl playerUnit;
    public FollowCamera followCamera;


    /// <summary>
    /// 인게임에서 필요한 Init 과정 여기서 수행
    /// </summary>
    public void Start()
    {
        followCamera.Init();
        playerUnit.cameraDirection = followCamera.cachedTransform;
    }
}
