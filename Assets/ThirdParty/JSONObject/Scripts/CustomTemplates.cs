using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class JSONTemplates
{
    public static void AddFieldObject(JSONObject _json, string _field, object _val)
    {
        if      (_val is bool)       _json.AddField(_field, (bool)_val);
        else if (_val is int)        _json.AddField(_field, (int)_val);
        else if (_val is long)       _json.AddField(_field, (long)_val);
        else if (_val is float)      _json.AddField(_field, (float)_val);
        else if (_val is string)     _json.AddField(_field, (string)_val);
        else if (_val is Vector3)    _json.AddField(_field, FromVector3((Vector3)_val));
        else if (_val is Color)      _json.AddField(_field, FromColor((Color)_val));
        else                         _json.AddField(_field, _val.ToString());
    }

    public static JSONObject ListToJsonArray<T>(List<T> _list) where T : IJsonConvertable
    {
        JSONObject resJson = new JSONObject();

        if (_list == null || _list.Count == 0)
            return resJson;

        for (int i = 0; i < _list.Count; ++i)
            resJson.Add(_list[i].ToJson());

        return resJson;
    }

    public static List<T> JsonArrayToList<T>(JSONObject _json) where T : IJsonConvertable, new()
    {
        List<T> resList = new List<T>();

        if (_json == null || !_json.IsArray)
            return resList;

        List<JSONObject> jsonArray = _json.list;
        if (jsonArray == null || jsonArray.Count == 0)
            return resList;

        for (int i = 0; i < jsonArray.Count; ++i)
        {
            T item = new T();
            item.FromJson(jsonArray[i]);
            resList.Add(item);
        }

        return resList;
    }

    public static JSONObject LongListToJsonArray(List<long> _list)
    {
        JSONObject resJson = new JSONObject();

        if (_list == null || _list.Count == 0)
            return resJson;

        for (int i = 0; i < _list.Count; ++i)
            resJson.Add(_list[i]);

        return resJson;
    }

    public static List<long> JsonArrayToLongList(JSONObject _json)
    {
        List<long> resList = new List<long>();

        if (_json == null || !_json.IsArray)
            return resList;

        List<JSONObject> jsonArray = _json.list;
        if (jsonArray == null || jsonArray.Count == 0)
            return resList;

        for (int i = 0; i < jsonArray.Count; ++i)
            resList.Add(jsonArray[i]?.i ?? 0);

        return resList;
    }

    public static JSONObject FromBounds(Bounds _b)
    {
        JSONObject result = JSONObject.obj;
        if (_b.center  != Vector3.zero) result.AddField("center", FromVector3(_b.center));
        if (_b.extents != Vector3.zero) result.AddField("extents", FromVector3(_b.extents));
        return result;
    }
    public static Bounds ToBounds(JSONObject _obj)
    {
        Bounds b = new Bounds();
        for (int i = 0; i < _obj.Count; i++)
        {
            switch (_obj.keys[i])
            {
                case "center": b.center = ToVector3(_obj[i]); break;
                case "extents": b.extents = ToVector3(_obj[i]); break;
            }
        }
        return b;
    }
}