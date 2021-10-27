using NiceInc.MinecraftNet.NBT;
using System;

namespace NiceInc.MinecraftNet.World
{
    public enum WorldChunkStacking
    {
        None,
        X = 1,
        Y = 2,
        Z = 4,
        Horizontal = X | Y,
        All = X | Y | Z,
    }

    public struct BlockLocation
    {
        public long X { get; }
        public long Y { get; }
        public long Z { get; }

        public BlockLocation(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class MetadataChangeEventArgs
    {
        public IMetadata OldMetadata { get; }
        public IMetadata NewMetadata { get; }

        public MetadataChangeEventArgs(IMetadata oldMetadata, IMetadata newMetadata)
        {
            OldMetadata = oldMetadata;
            NewMetadata = newMetadata;
        }
    }
    public class BlockChangeEventArgs
    {
        public Block OldBlock { get; }
        public Block NewBlock { get; }
        public BlockLocation Location { get; }

        public BlockChangeEventArgs(Block oldBlock, Block newBlock, BlockLocation location)
        {
            OldBlock = oldBlock;
            NewBlock = newBlock;
            Location = location;
        }
    }
    public delegate void BlockChangeEventHandler(object sender, BlockChangeEventArgs args);
    public delegate void MetadataChangeEventHandler(object sender, MetadataChangeEventArgs args);
    
    public interface IMetadataParser
    {
        IMetadata Parse(NBTCompoundTag nbt);
    }
    public interface IMetadata
    {
        NBTCompoundTag ToNBT();
    }

    public class EmptyMetadataParser : IMetadataParser
    {
        public IMetadata Parse(NBTCompoundTag nbt)
        {
            if (nbt is null) throw new ArgumentNullException(nameof(nbt));
            return new EmptyMetadata();
        }
    }
    public class EmptyMetadata : IMetadata
    {
        public NBTCompoundTag ToNBT() => new NBTCompoundTag();
    }

    public class Block
    {
        public IMetadata Metadata { get; set; }
    }
    /// <summary>
    /// An object representin a single minecraft chunk
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// The width of the chunk
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// The height of the chunk
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// The depth of the chunk
        /// </summary>
        public int Depth { get; }
        private Block[,,] blocks;

        /// <summary>
        /// An event indicating the change of a block in the chunk
        /// </summary>
        public event BlockChangeEventHandler BlockChanged;

        /// <summary>
        /// Gets or sets a block in the chunk
        /// </summary>
        /// <param name="x">The x coordinate of the block</param>
        /// <param name="y">The y coordinate of the block</param>
        /// <param name="z">The z coordinate of the block</param>
        /// <returns>The block at the specified coordinates in the chunk</returns>
        public Block this[int x, int y, int z] {
            get => blocks[x, y, z];
            set {
                if (value != blocks[x, y, z]) {
                    var e = new BlockChangeEventArgs(blocks[x, y, z], value, new BlockLocation(x, y, z));
                    BlockChanged?.Invoke(this, e);
                    blocks[x, y, z] = value;
                }
            }
        }

        public Chunk(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }
    }
    /// <summary>
    /// An object representing a minecraft world
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// In what dimentions will the chunks contained by the world be stacked
        /// </summary>
        WorldChunkStacking ChunkStacking { get; }
        /// <summary>
        /// Gets a chunk at the specified location
        /// </summary>
        /// <param name="x">The x coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <param name="y">The y coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <param name="z">The z coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <returns>The chunks at <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/></returns>
        Chunk GetChunk(int x, int y, int z);
        /// <summary>
        /// Sets the chunk at the specified location
        /// </summary>
        /// <param name="x">The x coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <param name="y">The y coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <param name="z">The z coordinate of the chunk (will be ignored if stacking for this dimension is not specified)</param>
        /// <param name="chunk">The chunk to set</param>
        void SetChunk(int x, int y, int z, Chunk chunk);


    }
    public class World
    {


    }
}
