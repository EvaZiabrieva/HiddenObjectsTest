using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupContainer : MonoBehaviour
{
    [SerializeField] private Button _button;
    public void SetUp(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}
