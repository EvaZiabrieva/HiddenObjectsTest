using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageObserver : IObserver<Sprite>
{
    private Image _image;

    public ImageObserver(Image image)
    {
        _image = image;
    }

    public void OnNext(Sprite value)
    {
        _image.sprite = value;
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }
}
