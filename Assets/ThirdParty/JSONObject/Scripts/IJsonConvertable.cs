using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJsonConvertable
{
    JSONObject ToJson();
    void FromJson(JSONObject _json);
}