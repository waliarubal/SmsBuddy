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

        protected object GetParameterValue(string name)
        {
            foreach (var parameter in _parameters)
                if (parameter.First.Equals(name))
                    return parameter.Second;

            return default(object);
        }

        protected void SetParameterNames(params string[] parameterNames)
        {
            var parameters = new List<Doublet<string, object>>();
            foreach (var parameterName in parameterNames)
                if (!string.IsNullOrWhiteSpace(parameterName))
                    parameters.Add(new Doublet<string, object>(parameterName, null));

            Parameters = parameters;
        }

        public abstract bool SendSms(string text);

        public abstract long GetBalance();
    }
}
