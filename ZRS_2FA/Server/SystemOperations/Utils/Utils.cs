using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperations.Utils
{
    public static class Utils
    {
        private const int TimeoutSeconds = 30;
        private const int AllowedAttempts = 3;
        private static readonly TimeSpan LookbackPeriod = TimeSpan.FromMinutes(5);
        public static bool IsLoginAllowed(long userId, Broker broker)
        {
            DateTime lookbackStart = DateTime.UtcNow - LookbackPeriod;
            (int attempts, DateTime? lastAttempt) = broker.GetLoginAttempts(userId, lookbackStart);

            if (attempts >= AllowedAttempts)
            {
                if (lastAttempt.HasValue)
                {
                    TimeSpan timeSinceLastAttempt = DateTime.UtcNow - lastAttempt.Value;
                    if (timeSinceLastAttempt.TotalSeconds < TimeoutSeconds)
                    {
                        // User is still in timeout lockout period
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
