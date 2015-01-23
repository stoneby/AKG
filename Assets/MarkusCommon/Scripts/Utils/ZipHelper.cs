using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if !UNITY_WEBPLAYER

public class ZipHelper : MonoBehaviour
{
    public string UrlPath;
    public bool AutoMode;

    public List<string> EntryList = new List<string>();

    public delegate void LoadFinish();
    public LoadFinish OnLoadFinish;

    private WWW www;
    private bool isUnzipped;
    private string docPath;

    private const int Buffer = 2048;

    public void Download()
    {
        StartCoroutine(DoDownload());
    }

    private IEnumerator DoDownload()
    {
        www = new WWW(UrlPath);

        yield return www;
        if (www.isDone && !isUnzipped)
        {
            Logger.Log(string.Format("Load of {0} complete", UrlPath));

            var data = www.bytes;

            Logger.Log("Write file to path=" + docPath);
            File.WriteAllBytes(docPath, data);

            var basePath = new FileInfo(docPath).Directory;

            using (var s = new ZipInputStream(File.OpenRead(docPath)))
            {
                EntryList.Clear();
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    var fullName = string.Format("{0}/{1}", basePath, theEntry.Name);
                    if (!string.IsNullOrEmpty(fullName))
                    {
                        EntryList.Add(fullName);
                        Logger.Log("Unzipping: " + theEntry.Name);
                        using (var streamWriter = File.Create(fullName))
                        {
                            var fdata = new byte[Buffer];
                            while (true)
                            {
                                var size = s.Read(fdata, 0, fdata.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(fdata, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                isUnzipped = true;
            }

            if (OnLoadFinish != null)
            {
                OnLoadFinish();
            }
        }
    }

    private void Awake()
    {
        var docBasePath = string.Format("{0}/Template", Application.persistentDataPath);
        if (!Directory.Exists(docBasePath))
        {
            Directory.CreateDirectory(docBasePath);
        }
        docPath = string.Format("{0}/Template.zip", docBasePath);

        if (AutoMode)
        {
            Download();
        }
    }
}

#endif