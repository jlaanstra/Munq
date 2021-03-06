﻿using Munq.DI.Configuration;
using FinalApp.Interfaces;

namespace FinalApp.Authentication
{
    public class AuthenticationRegistrar : IMunqConfig
    {
        #region IMunqConfig Members

        public void RegisterIn(Munq.DI.Container container)
        {
            container.Register<IFormsAuthentication>(ioc => new FormsAuthenticationService());
        }

        #endregion
    }
}
