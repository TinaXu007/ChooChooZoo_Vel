using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private Sprite GetScreenShot()
    {
        var image = new Texture2D(Screen.width,Screen.height, TextureFormat.RGB24,false);
        image.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0,false);
        image.Apply();
        Rect rect = new Rect(0,0,image.width,image.height);
        var sprite = Sprite.Create(image, rect, new Vector2(0.5f, 0.5f), 100);
        return sprite;
    }

    private void SaveScreenShot(Sprite sprite, string outputfilename)
    {
        Texture2D itemBGTex = sprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();
        File.WriteAllBytes($"{outputfilename}.png", itemBGBytes);
    }
}
