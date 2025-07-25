public interface IInteractable
{
    void Interact();
    void OnFocus();     // Called when player is looking at it
    void OnLoseFocus(); // Called when player looks away
}