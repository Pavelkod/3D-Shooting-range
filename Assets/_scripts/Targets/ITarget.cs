public interface ITarget
{
    bool IsActive { get; }

    void Deselect();
    void Select();
}
