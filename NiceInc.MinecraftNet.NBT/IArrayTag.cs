namespace NiceInc.MinecraftNet.NBT
{
    public interface IArrayTag : ITag
    {
        object this[int i] { get; set; }
        int Length { get; }
    }
    public interface IArrayTag<T> : IArrayTag
    {
        T this[int i] { get; set; }

        object IArrayTag.this[int i] {
            get => this[i];
            set => this[i] = (T)value;
        }
    }
}
