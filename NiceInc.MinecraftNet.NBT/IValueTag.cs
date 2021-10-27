namespace NiceInc.MinecraftNet.NBT
{
    public interface IValueTag : ITag
    {
        object Value { get; set; }
    }
    public interface IValueTag<T> : IValueTag
    {
        T Value { get; set; }
        object IValueTag.Value {
            get => Value;
            set => Value = (T)value;
        }
    }
}
