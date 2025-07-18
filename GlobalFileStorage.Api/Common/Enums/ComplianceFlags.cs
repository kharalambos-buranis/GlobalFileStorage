namespace GlobalFileStorage.Api.Common.Enums
{
    [Flags]

    public enum ComplianceFlags
    {
        None = 0,
        GDPR = 1,
        HIPAA = 2,
        SOX = 4,
        PCI_DSS = 8
    }
}
