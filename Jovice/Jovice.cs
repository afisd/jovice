﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Aphysoft.Share;

namespace Center
{
    public class Jovice
    {
        #region Database

        private static Database jovice = null;

        public static Database Database
        {
            get
            {
                if (jovice == null)
                {
#if DEBUG
                    jovice = new Database(Aphysoft.Protected.Project.Database("JOVICE_DEBUG"), DatabaseType.SqlServer);
#else
                    jovice = new Database(Aphysoft.Protected.Project.Database("JVOICE_RELEASE"), DatabaseType.SqlServer);
#endif
                }
                return jovice;
            }
        }

        #endregion
    }
}
