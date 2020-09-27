public interface ICommand 
{
    void Execute();

    void Undo();

    float GetLag();
}
