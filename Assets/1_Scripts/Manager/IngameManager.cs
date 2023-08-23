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
    /// �ΰ��ӿ��� �ʿ��� Init ���� ���⼭ ����
    /// </summary>
    public void Start()
    {
        followCamera.Init();
        playerUnit.cameraDirection = followCamera.cachedTransform;
    }
}
