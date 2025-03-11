using System;
using UnityEngine.UI;

public sealed class TextObserver : IObserver<string>
{
    private Text _text;

    public TextObserver(Text text)
    {
        _text = text;
    }

    public void OnNext(string value)
    {
        _text.text = value;
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }
}