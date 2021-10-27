namespace NiceInc.MinecraftNet.World
{
    /// <summary>
    /// An object representing a single block
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// The block's identifier
        /// </summary>
        Identifier ID { get; }
        /// <summary>
        /// An event indicating the change of the block's metadata (fires when Metadata.Changed is fired too)
        /// </summary>
        event MetadataChangeEventHandler MetadataChanged;
        /// <summary>
        /// The block's metadata
        /// </summary>
        IMetadata Metadata { get; set; }
    }
}
