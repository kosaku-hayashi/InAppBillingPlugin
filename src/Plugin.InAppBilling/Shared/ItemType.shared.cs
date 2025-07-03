using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.InAppBilling
{
    /// <summary>
    /// Product item type
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Single purchase (managed)
        /// </summary>
        InAppPurchase,
        /// <summary>
        /// Single purchase that needs to be consumed manually
        /// </summary>
        InAppPurchaseConsumable,
        /// <summary>
        /// On going subscription
        /// </summary>
        Subscription
    }

    /// <summary>
    /// Subscription replacement mode
    /// </summary>
    public enum SubscriptionReplacementMode
    {
        UnknownReplacementMode = 0,
        WithTimeProration = 1,
        ChargeProratedPrice = 2,
        WithoutProration = 3,
        ChargeFullPrice = 5,
        Deferred = 6
    }
}