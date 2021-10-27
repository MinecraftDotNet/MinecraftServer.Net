namespace NiceInc.MinecraftNet.Peeker
{
    public interface IPeekable<T>
    {
        bool Peeking { get; }
        IPeeker<T> Peek(bool commitable = false);
        void CommitPeek();
        void CancelPeek();
    }
}
