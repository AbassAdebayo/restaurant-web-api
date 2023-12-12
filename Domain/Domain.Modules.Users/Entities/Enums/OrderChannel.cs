using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{
    public enum OrderChannel
    {
        Cashier = 1,
        HandHeld,
        PayAtTable,
        TableOrdering

    }
    public static class OrderChannelFormatter
    {
        public static string ToCustomFormat(this OrderChannel channel)
        {

            switch (channel)
            {
                case OrderChannel.Cashier:
                    return "Cashier";

                case OrderChannel.HandHeld:
                    return "Hand-held";

                case OrderChannel.PayAtTable:
                    return "Pay-at-table";

                case OrderChannel.TableOrdering:
                    return "Table-ordering";

                default:
                    return string.Empty;
            }

        }
    }

}
