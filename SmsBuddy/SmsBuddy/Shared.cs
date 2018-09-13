using NullVoidCreations.WpfHelpers.Base;
using SmsBuddy.Gateway;

namespace SmsBuddy
{
    class Shared: NotificationBase
    {
        static object _syncLock;
        static Shared _instance;
        GatewayBase _gateway;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {

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

        public GatewayBase Gateway
        {
            get { return _gateway; }
            set { Set(nameof(Gateway), ref _gateway, value); }
        }

        #endregion
    }
}
