using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{
  
    public enum Channels
    {
        [Description("POS #1")]
        POS1 = 1,
        [Description("POS #2")]
        POS2,
        [Description("POS #3")]
        POS3,
        [Description("POS #4")]
        POS4,
        [Description("POS #5")]
        POS5,
        [Description("POS #6")]
        POS6,
        [Description("POS #7")]
        POS7,
        [Description("POS #8")]
        POS8,       

    }

    public static class POSChannelFormatter
    {
        public static string ToCustomFormat(this Channels posChannel)
        {
             
                switch (posChannel)
                {
                    case Channels.POS1:
                        return "POS #1";

                    case Channels.POS2:
                        return "POS #2";

                    case Channels.POS3:
                        return "POS #3";

                    case Channels.POS4:
                        return "POS #4";

                    case Channels.POS5:
                        return "POS #5";

                    case Channels.POS6:
                        return "POS #6";

                    case Channels.POS7:
                        return "POS #7";

                    case Channels.POS8:
                        return "POS #8";

                    default:
                        return string.Empty;
                }
           
        }
    }


}
