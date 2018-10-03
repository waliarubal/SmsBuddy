using LiteDB;
using NullVoidCreations.WpfHelpers;
using SmsBuddy.Models;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

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
        DispatcherTimer _timer;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {
            
        }

        ~Shared()
        {
            if (_timer != null)
            {
                _timer.Tick -= SchedulerTick;
                _timer.Stop();
                _timer = null;
            }

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

        void SchedulerTick(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            var messages = Database.GetCollection<SmsModel>().Find(sms => sms.RepeatDaily && sms.Hour == time.Hour && sms.Minute == time.Minute);
            foreach(var message in messages)
            {
                var sentMessage = message.Gateway.Send(message, message.MobileNumbersScheduled);
                sentMessage.Save();
            }
        }

        public void StartScheduler()
        {
            if (_timer != null)
                return;

            _timer = new DispatcherTimer(DispatcherPriority.Normal, Application.Current.Dispatcher);
            _timer.Tick += SchedulerTick;
            _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Start();
        }
    }
}
