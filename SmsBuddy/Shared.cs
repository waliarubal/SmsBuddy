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
        string _databaseFile, _startupDirectory;
        AssemblyInformation _assemblyInformation;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {
            
        }

        ~Shared()
        {
            if (Database != null)
                Database.Dispose();
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

        public AssemblyInformation AssemblyInfo
        {
            get
            {
                if (_assemblyInformation == null)
                    _assemblyInformation = new AssemblyInformation(Assembly.GetExecutingAssembly());

                return _assemblyInformation;
            }
        }

        public string StartupDirectory
        {
            get
            {
                if (_startupDirectory == null)
                    _startupDirectory = Application.Current.GetStartupDirectory();

                return _startupDirectory;
            }
        }

        public string DatabaseFile
        {
            get
            {
                if (_databaseFile == null)
                    _databaseFile = Path.Combine(StartupDirectory, "Database.litedb");

                return _databaseFile;
            }
        }

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
