using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.DataStructures;
using System;
using System.Collections.Generic;

namespace SmsBuddy.Gateway
{
    abstract class GatewayBase: NotificationBase
    {
        string _name;
        Version _version;
        Uri _providerUrl;
        IEnumerable<Doublet<string, object>> _parameters;

        #region constructor/destructor

        protected GatewayBase(string name)
        {
            Name = name;
            Version = new Version(1, 0);
        }

        #endregion

        #region properties

        public string Name
        {
            get { return _name; }
            private set { Set(nameof(Name), ref _name, value); }
        }

        public Version Version
        {
            get { return _version; }
            protected set { Set(nameof(Version), ref _version, value); }
        }

        public Uri ProviderUrl
        {
            get { return _providerUrl; }
            protected set { Set(nameof(ProviderUrl), ref _providerUrl, value); }
        }

        public IEnumerable<Doublet<string, object>> Parameters
        {
            get { return _parameters; }
            private set { Set(nameof(Parameters), ref _parameters, value); }
        }

        protected object this[string name]
        {
            get { return GetParameter(name); }
            set { SetParameter(name, value); }
        }

        #endregion

        object GetParameter(string name)
        {
            foreach (var parameter in _parameters)
                if (parameter.First.Equals(name))
                    return parameter.Second;

            return default(object);
        }

        void SetParameter(string name, object value)
        {
            foreach(var parameter in _parameters)
            {
                if (parameter.First.Equals(name))
                {
                    parameter.Second = value;
                    return;
                }
            }  
        }

        protected void SetParameterNames(params string[] names)
        {
            var parameters = new List<Doublet<string, object>>();
            foreach (var name in names)
                if (!string.IsNullOrWhiteSpace(name))
                    parameters.Add(new Doublet<string, object>(name, null));

            Parameters = parameters;
        }

        public abstract bool SendSms(string text, params string[] mobileNumbers);

        public virtual long GetBalance()
        {
            return 0L;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
