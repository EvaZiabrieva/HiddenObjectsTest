public class PopupState : BaseState
{
    public override bool IsFinished => _isFinished;
    private bool _isFinished = false;

    private PopupSystem _popupSystem;

    public PopupState(PopupSystem popupSystem)
    {
        _popupSystem = popupSystem;
    }
    public override void Start()
    {
        _isFinished = false;
        _popupSystem.SetUpPopup(OnComplete);
        _popupSystem.TooglePopup(true);
    }

    public override void Stop()
    {
        _isFinished = true;
    }
    private void OnComplete()
    {
        _popupSystem.TooglePopup(false);
        Stop();
    }
}
