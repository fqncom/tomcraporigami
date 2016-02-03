using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Models;

namespace TickTick.Handler
{
    public class BatchHandler
    {
        protected TickTickApplicationBase application;
        protected string userId;
        protected SyncResult mSyncResult;

        public BatchHandler(String userId, SyncResult syncResult)
        {
            this.userId = userId;
            //this.application = TickTickApplicationBase.StaticApplication;
            this.mSyncResult = syncResult;
        }
    }
}
