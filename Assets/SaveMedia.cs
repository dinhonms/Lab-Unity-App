using System.Runtime.InteropServices;

public static class SaveMedia
{
    public delegate void OnSuccess(string value);
    public delegate void OnError(string value);

    [DllImport("__Internal")]
    private static extern void saveImage(string imageNameWithExtension, string albumName, OnSuccess onSuccess, OnError onError);

    [DllImport("__Internal")]
    private static extern void saveVideo(string videoNameWithExtension, string albumName, OnSuccess onSuccess, OnError onError);

    public static void SaveImage(string imageNameWithExtension, string albumName, OnSuccess onSuccess, OnError onError)
    {
        saveImage(imageNameWithExtension: imageNameWithExtension, albumName: albumName, onSuccess: onSuccess, onError: onError);
    }

    public static void SaveVideo(string videoNameWithExtension, string albumName, OnSuccess onSuccess, OnError onError)
    {
        saveVideo(videoNameWithExtension: videoNameWithExtension, albumName: albumName, onSuccess: onSuccess, onError: onError);
    }

}

