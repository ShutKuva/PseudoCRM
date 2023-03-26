namespace Core.Email.Additional
{
    [Flags]
    public enum ServerProtocols
    {
        Pop = 1,
        Imap = 2,
        Smtp = 4
    }
}