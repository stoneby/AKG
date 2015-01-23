using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HttpRequester : MonoBehaviour
{
    public string BaseUrl;

    public Dictionary<string, string> RequestMap;

    public bool Running;

    public delegate void Response(string jsonStr);

    public Response OnResponse;

    public void Request()
    {
        if (Running)
        {
            Debug.LogWarning(name + " Http requester is running. ");
            return;
        }

        Running = true;
        StartCoroutine("DoRequest");
    }

    private IEnumerator DoRequest()
    {
        var url = GenerateRequestStr();
        Debug.Log(url);
        var www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("request error: " + www.error);
        }
        else
        {
            Debug.Log("request success");
            Debug.Log("returned data" + www.text);

            if (OnResponse != null)
            {
                OnResponse(www.text);
            }
        }

        Running = false;
    }

    private string GenerateRequestStr()
    {
        var result = RequestMap.Aggregate(string.Empty, (current, pair) => current + string.Format("{0}={1}&", pair.Key, pair.Value));
        result = result.Remove(result.Length - 1);

        return BaseUrl + result;
    }

    private void Awake()
    {
    }
}
