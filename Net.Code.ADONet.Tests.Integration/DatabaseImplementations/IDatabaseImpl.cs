namespace Net.Code.ADONet.Tests.Integration
{
    public interface IDatabaseImpl
    {
        string CreatePersonTable { get; }
        string InsertPerson { get; }
        bool SupportsMultipleResultSets { get; }
        string ProviderName { get; }
        bool SupportsTableValuedParameters { get; }
        string EscapeChar { get; }
        void DropAndRecreate();
        IDb CreateDb();
        Person Project(dynamic d);
    }
}