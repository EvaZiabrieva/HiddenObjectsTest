using UnityEngine;

public class DefaultVisualFeedback : MonoBehaviour, IVisualFeedback
{
    [SerializeField] private GameObject _item;
    public void PlayFoundVisuals()
    {
        _item.SetActive(false);
    }
}
