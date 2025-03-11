using UnityEngine;
using UnityEngine.Events;

public class PopupSystem : MonoBehaviour
{
    [SerializeField] private PopupContainer _popupPrefab;
    private PopupContainer _currentPopup;
    public void SetUpPopup(UnityAction action)
    {
        _currentPopup = Instantiate(_popupPrefab, transform);
        _currentPopup.SetUp(action);
    }
    public void TooglePopup(bool isActive)
    {
        _currentPopup.gameObject.SetActive(isActive);
    }
}
