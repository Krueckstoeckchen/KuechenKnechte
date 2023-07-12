public interface IInteractable
{
    public void Select();
    public void Unselect();

    public void Interact(CharacterInteract characterInteract);
    public void InteractAlternate(CharacterInteract characterInteract);
}
