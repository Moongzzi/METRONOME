using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Field
{
    public class ScriptManager : MonoBehaviour
    {
        public ScriptInfoAsset scriptInfoAsset;

        public ResearchScript GetResearchInfo(ResearchObj researchObj)
        {
            ResearchScript researchScript = scriptInfoAsset.researchScripts.Find(e => e.researchObj == researchObj);
            return researchScript;
        }

        public Sprite GetSpeakerImg(SpeakerType speaker)
        {
            Sprite image = scriptInfoAsset.speakersImg[(int)speaker];
            return image;
        }

        public Sprite GetResearchItemImg(ResearchObj researchObj)
        {
            Sprite image = scriptInfoAsset.researchItemImg[(int)researchObj];
            return image;
        }
    }
}