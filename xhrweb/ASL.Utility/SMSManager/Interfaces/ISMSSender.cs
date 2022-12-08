using ASL.Utility.SMSManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASL.Utility.SMSManager.Interfaces
{
    public interface ISMSSender
    {
        Task<string> SendSMSEnglishAsync(List<SMSContent> contents, SMSGatewaySettings settings);
    }
}
