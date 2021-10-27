namespace NiceInc.MinecraftNet.Peeker
{
    public interface IPeeker<T> : IPeekable<T>
    {
        T Next();
        T Current { get; }
        int Offset { get; }
        bool Ended { get; }
    }
}
