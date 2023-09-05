using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Field
{
    public enum SpeakerType
    {
        MainPlayer,
        CEO,
        Comment
    }

    public enum ResearchObj
    {
        Cable,
        Hairball,
        Files
    }

    [Serializable]
    public class ScriptInfo
    {
        public SpeakerType speakerType;
        public string speakText;
    }

    [Serializable]
    public class ResearchScript
    {
        public ResearchObj researchObj;
        public List<ScriptInfo> scriptInfos = new List<ScriptInfo>();
    }

    [CreateAssetMenu(fileName = "MET", menuName = "ScriptInfoAsset")]
    public class ScriptInfoAsset : ScriptableObject
    {
        public List<Sprite> speakersImg = new List<Sprite>();
        public List<Sprite> researchItemImg = new List<Sprite>();
        public List<ResearchScript> researchScripts = new List<ResearchScript>();
    }
}