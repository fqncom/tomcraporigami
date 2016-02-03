using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;

namespace TickTick.Helper
{
    public class LimitHelper
    {

        private LimitsBll LimitsBll = new LimitsBll();
        private Limits LimitsFree;
        private Limits LimitsPro;

        private static LimitHelper _staticLimitHelper;

        public static LimitHelper StaticLimitHelper
        {
            get
            {
                if (_staticLimitHelper == null)
                {
                    lock ("create")
                    {
                        if (_staticLimitHelper != null)
                        {
                            _staticLimitHelper = new LimitHelper();
                        }
                    }
                }
                return _staticLimitHelper;
            }
            set { LimitHelper._staticLimitHelper = value; }
        }

        public async Task<Limits> GetLimits(bool isPro)
        {
            if (isPro)
            {
                return await GetLimitsPro();
            }
            else
            {
                return await GetLimitsFree();
            }

        }
        public async Task<Limits> GetLimitsPro()
        {
            if (LimitsPro == null)
            {
                LimitsPro = await LimitsBll.GetLimits(true);
            }
            return LimitsPro;
        }
        public async Task<Limits> GetLimitsFree()
        {
            if (LimitsFree == null)
            {
                LimitsFree = await LimitsBll.GetLimits(false);
            }
            return LimitsFree;
        }

    }
}
