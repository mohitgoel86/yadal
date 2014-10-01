using System.Linq;
using NUnit.Framework;

namespace Net.Code.ADONet.Tests.Unit.DbTests
{
    [TestFixture]
    public class DbConfigurationBuilderTests
    {
        private DbConfigurationBuilder _dbConfigurationBuilder;
        private DbConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = new DbConfig();
            _dbConfigurationBuilder = new DbConfigurationBuilder(_config);
        }

        [Test]
        public void Default_PrepareCommand_DoesNothing()
        {
            _dbConfigurationBuilder.FromProviderName("unknown");
            _config.PrepareCommand(null);
        }

        [Test]
        public void SqlServer_PrepareCommand_DoesNothing()
        {
            _dbConfigurationBuilder.FromProviderName("System.Data.SqlClient");
            _config.PrepareCommand(null);
        }

        [Test]
        public void Oracle_PrepareCommand_SetsBindByName()
        {
            _dbConfigurationBuilder.FromProviderName("Oracle.DataAccess.Client");
            var oracleCommand = new FakeOracleDbCommand();
            _config.PrepareCommand(oracleCommand);
            Assert.IsTrue(oracleCommand.BindByName);
        }


        [Test]
        public void GivenDb_WhenOnPrepareIsConfigured__AndExecuteIsCalled_CommandIsPrepared()
        {
            var fakeConnection = new FakeConnection();
            var db = new Db(fakeConnection);
            db.Configure().OnPrepareCommand(c => ((FakeCommand)c).Comment = "PREPARED");
            db.Execute("");
            Assert.AreEqual("PREPARED", fakeConnection.Commands.Single().Comment);
        }
    }
}