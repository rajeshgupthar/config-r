using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ConfigR
{
    public class App : DynamicObject
    {
        private static dynamic _appInstance = new App();
        public static dynamic Settings
        {
            get
            {
                return _appInstance;
            }
        }

        protected App()
        {
        }

        private Dictionary<string, dynamic> _settings = new Dictionary<string, dynamic>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!_settings.ContainsKey(binder.Name))
            {
                _settings.Add(binder.Name, value);
                return true;
            }
            return base.TrySetMember(binder, value);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_settings.ContainsKey(binder.Name))
            {
                result = _settings[binder.Name];
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }
    }
}
