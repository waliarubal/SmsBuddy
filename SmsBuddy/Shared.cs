using LiteDB;
using NullVoidCreations.WpfHelpers;
using System;
using System.IO;
using System.Reflection;

namespace SmsBuddy
{
    class Shared
    {
        static object _syncLock;
        static Shared _instance;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {
            AssemblyInfo = new AssemblyInformation(Assembly.GetExecutingAssembly());
            DatabaseFile = Path.Combine(App.Current.GetStartupDirectory(), "Assets", "Database.db");
            
        }

        #region properties

        public static Shared Instance
        {
            get
            {
                lock(_syncLock)
                {
                    if (_instance == null)
                        _instance = new Shared();
                }

                return _instance;
            }
        }

        public AssemblyInformation AssemblyInfo { get; }

        public string DatabaseFile { get; }

        #endregion

        public LiteDatabase GetDatabase()
        {
            var connectionString = new ConnectionString();
            connectionString.CacheSize = 5000;
            connectionString.Filename = DatabaseFile;
            connectionString.Flush = true;
            connectionString.InitialSize = 0;
            connectionString.Journal = true;
            connectionString.Log = Logger.NONE;
            connectionString.Mode = LiteDB.FileMode.Shared;
            connectionString.Password = null;
            connectionString.Timeout = TimeSpan.FromMinutes(5);
            connectionString.Upgrade = true;
            connectionString.UtcDate = false;

            var database = new LiteDatabase(connectionString);
            return database;
        }
    }
}
