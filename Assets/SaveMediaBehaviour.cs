using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using UnityEngine.Networking;
using static SaveMedia;

public class SaveMediaBehaviour : MonoBehaviour
{
    [SerializeField] string _imageName = "image.png";
    [SerializeField] string _videoName = "video.mp4";
    [SerializeField] string _albumName;
    [SerializeField] Texture2D _texture;
    [SerializeField] string _video;

    public void WriteMedia()
    {
        string path = Path.Combine(Application.persistentDataPath, _imageName);
        File.WriteAllBytes(path, _texture.EncodeToPNG());

        Debug.Log($"Image saved at: {path}");
        StartCoroutine(GetVideo());
    }

    public void SaveImage()
    {
       SaveMedia.SaveImage(imageNameWithExtension: _imageName, _albumName, onSuccess: OnImageSavedSuccess, onError: OnImageSaveError);
    }

    [MonoPInvokeCallback(typeof(OnSuccess))]
    private static void OnImageSavedSuccess(string message)
    {
        Debug.Log($"[UNITY] image saved: {message}");
    }

    [MonoPInvokeCallback(typeof(OnError))]
    private static void OnImageSaveError(string message)
    {
        Debug.Log($"[UNITY] image not saved: {message}");
    }

    public void SaveVideo()
    {
        SaveMedia.SaveVideo(_videoName, _albumName, onSuccess: OnVideoSavedSuccess, onError: OnVideoSaveError);
    }

    [MonoPInvokeCallback(typeof(OnSuccess))]
    private static void OnVideoSavedSuccess(string message)
    {
        Debug.Log($"[UNITY] Video saved: {message}");
    }

    [MonoPInvokeCallback(typeof(OnError))]
    private static void OnVideoSaveError(string message)
    {
        Debug.Log($"[UNITY] Video not saved: {message}");
    }

    IEnumerator GetVideo()
    {
        var request = UnityWebRequest.Get(_video);

        yield return request.SendWebRequest();

        var data = request.downloadHandler.data;

        var path = Path.Combine(Application.persistentDataPath, "video.mp4");

        File.WriteAllBytes(path, data);
        Debug.Log($"Video saved at: {path}");
    }

}
