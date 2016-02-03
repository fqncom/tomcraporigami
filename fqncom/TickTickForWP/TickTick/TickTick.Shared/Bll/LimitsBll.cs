using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Bll
{
    public class LimitsBll : BaseBll<Limits>
    {
        private LimitsDal LimitsDal = new LimitsDal();

        private static Limits freeLimits;
        private static Limits proLimits;

        public async Task<Limits> GetLimits(bool isPro)
        {
            Limits ret = await LimitsDal.GetLimits(isPro ? Constants.AccountType.ACCOUNT_TYPE_PREMIUM : Constants.AccountType.ACCOUNT_TYPE_FREE);
            if (ret == null)
            {
                ret = GetDefaultLimits(isPro);
            }
            return ret;
        }
        private Limits GetDefaultLimits(bool isPro)
        {
            return isPro ? GetDefaultProLimits() : GetDefaultFreeLimits();
        }
        private Limits GetDefaultProLimits()
        {
            if (proLimits == null)
            {
                Limits limits = new Limits();
                limits.ProjectNumber = 199;
                limits.ProjectTaskNumber = 999;
                limits.SubTaskNumber = 199;
                limits.ShareUserNumber = 19;
                limits.FileSizeLimit = Limits.DEFAULT_FILE_SIZE_LIMIT_PRO;
                limits.FileCountDailyLimit = Limits.DEFAULT_FILE_COUNT_DAILY_LIMIT_PRO;
                proLimits = limits;
            }
            return proLimits;
        }
        private Limits GetDefaultFreeLimits()
        {
            if (freeLimits == null)
            {
                Limits limits = new Limits();
                limits.ProjectNumber = 19;
                limits.ProjectTaskNumber = 99;
                limits.SubTaskNumber = 19;
                limits.ShareUserNumber = 1;
                limits.FileSizeLimit = Limits.DEFAULT_FILE_SIZE_LIMIT_FREE;
                limits.FileCountDailyLimit = Limits.DEFAULT_FILE_COUNT_DAILY_LIMIT_FREE;
                freeLimits = limits;
            }
            return freeLimits;
        }

        protected override void SetCurrentDal()
        {
            CurrentDal = LimitsDal;
        }
    }
}
