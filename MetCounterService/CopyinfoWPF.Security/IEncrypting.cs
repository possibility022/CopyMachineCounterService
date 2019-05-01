namespace CopyinfoWPF.Security
{
    public interface IEncrypting
    {

        byte[] Protect(byte[] data);
        byte[] Unprotect(byte[] data);

    }
}
