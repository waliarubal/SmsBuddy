using LiteDB;
using NullVoidCreations.WpfHelpers;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace SmsBuddy
{
    class Shared
    {
        const string PASSWORD = "!Control*88";

        static object _syncLock;
        static Shared _instance;
        LiteDatabase _database;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {
            AssemblyInfo = new AssemblyInformation(Assembly.GetExecutingAssembly());
            StartupDirectory = Application.Current.GetStartupDirectory();
            DatabaseFile = Path.Combine(StartupDirectory, "Database.litedb");
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

                    return _instance;
                } 
            }
        }

        public AssemblyInformation AssemblyInfo { get; }

        public string StartupDirectory { get; }

        public string DatabaseFile { get; }

        public LiteDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = GetDatabase();

                return _database;
            }
        }

        #endregion

        LiteDatabase GetDatabase()
        {
            var connectionString = new ConnectionString
            {
                CacheSize = 5000,
                Filename = DatabaseFile,
                Flush = true,
                InitialSize = 0,
                Journal = true,
                Log = Logger.NONE,
                Mode = LiteDB.FileMode.Shared,
                Password = PASSWORD,
                Timeout = TimeSpan.FromMinutes(5),
                Upgrade = true,
                UtcDate = false
            };

            var database = new LiteDatabase(connectionString);
            return database;
        }
    }
}
