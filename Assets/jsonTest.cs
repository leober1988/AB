using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class jsonTest : MonoBehaviour
{
  
    public GameObject BG_TOP_parent;
    void Start()
    {
        //StartCoroutine(GetText(""));
        GetTextFromRescource();
    }
    
    IEnumerator GetText(string url)
    {
        yield return new WaitForSeconds(0.1f);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.responseCode == 200)//等于200表示接受成功
                {

                    Debug.Log(www.downloadHandler.text);
                    AnFD _AnFD = JsonUtility.FromJson<AnFD>(www.downloadHandler.text);
                   
                }
            }
        }
    }


    void  GetTextFromRescource()
    {
        if (!File.Exists(Application.dataPath + "/Resources/Test.json"))
        {
            return;
        }
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/Test.json");
        //FileStream file = File.Open(Application.dataPath + "/Test.json", FileMode.Open, FileAccess.ReadWrite);
        //if (file.Length == 0)
        //{
        //    return null;
        //}

        //string json = (string)bf.Deserialize(file);
        //file.Close();

        string json = sr.ReadToEnd();
        Debug.Log(json);

        string text =( (TextAsset)Resources.Load("Test")).text;
        AnFD _AnFD = JsonUtility.FromJson<AnFD>(json);
        Debug.Log(_AnFD.result[1].area);
          
    }
}
[System.Serializable]
public class AnFD
{
  
    public List<AFDnum> result;
}
[System.Serializable]
public class AFDnum
{
    public string area;
    public string count;
    //public string num;
}
