using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.DataStructures;
using System.Collections.Generic;

namespace SmsBuddy.Gateway
{
    abstract class GatewayBase: NotificationBase
    {
        string _name;
        IEnumerable<Doublet<string, object>> _parameters;

        #region constructor/destructor

        protected GatewayBase(string name)
        {
            Name = name;
        }

        #endregion

        #region properties

        public string Name
        {
            get { return _name; }
            private set { Set(nameof(Name), ref _name, value); }
        }

        public IEnumerable<Doublet<string, object>> Parameters
        {
            get { return _parameters; }
            private set { Set(nameof(Parameters), ref _parameters, value); }
        }

        #endregion

        protected void SetParameterNames(IEnumerable<string> parameterNames)
        {
            var parameters = new List<Doublet<string, object>>();
            foreach (var parameterName in parameterNames)
                parameters.Add(new Doublet<string, object>(parameterName, null));
            Parameters = parameters;
        }

        public abstract bool SendSms(string text);

        public abstract long GetBalance();
    }
}
