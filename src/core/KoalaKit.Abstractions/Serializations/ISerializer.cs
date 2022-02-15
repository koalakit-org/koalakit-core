namespace KoalaKit.Serializations
{
    public interface ISerializer<TData>
    {
        /// binary serializer not applied because of https://aka.ms/binaryformatter
        TData? Deserialize(byte[] bytes);
        byte[] Serialize(TData message);
    }
}
