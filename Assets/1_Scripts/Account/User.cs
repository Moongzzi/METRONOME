using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MET.Account
{
    public class User : System.Object, IJsonConvertable
    {
        public int sn = -1;
        public string nickname = string.Empty;


        public void FromJson(JSONObject _json)
        {
            if (_json == null || _json.IsNull)
                return;

            _json.GetField(out sn, "acutSn", sn);
            _json.GetField(out nickname, "nickNm", nickname);
        }

        public JSONObject ToJson()
        {
            JSONObject _json = new JSONObject();

            if (sn >= 0)
                _json.AddField("acutSn", sn);

            if (!(nickname == null || nickname.Equals("")))
                _json.AddField("nickNm", nickname);

            return _json;
        }
    }
}