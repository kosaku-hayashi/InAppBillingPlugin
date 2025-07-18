
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.InAppBilling
{
    /// <summary>
    /// Interface for InAppBilling
    /// </summary>
    [Preserve(AllMembers = true)]
    public interface IInAppBilling : IDisposable
    {
        /// <summary>
        /// Determines if it is connected to the backend actively (Android).
        /// </summary>
        public bool IsConnected { get; set; }
        /// <summary>
        /// Gets or sets if in testing mode
        /// </summary>
        bool InTestingMode { get; set; }


        /// <summary>
        /// If you want to ignore invalid products when getting details
        /// </summary>
        bool IgnoreInvalidProducts { get; set; }

        /// <summary>
        /// Represenation of the storefront if available
        /// </summary>
        Storefront Storefront { get; }


        /// <summary>
        /// Manually acknowledge/finalize a purchase
        /// </summary>
        /// <param name="transactionIdentifier"></param>
        /// <returns>if all were acknowledged/finalized</returns>
        Task<IEnumerable<(string Id, bool Success)>> FinalizePurchaseAsync(string[] transactionIdentifier, CancellationToken cancellationToken = default);

        /// <summary>
        /// Manually acknowledge/finalize a product id
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns>if all were acknowledged/finalized</returns>
        Task<IEnumerable<(string Id, bool Success)>> FinalizePurchaseOfProductAsync(string[] productIds, CancellationToken cancellationToken = default);


        /// <summary>
        /// Connect to billing service
        /// </summary>
        /// <returns>If Success</returns>
        Task<bool> ConnectAsync(bool enablePendingPurchases = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Disconnect from the billing service
        /// </summary>
        /// <returns>Task to disconnect</returns>
        Task DisconnectAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get product information of a specific product
        /// </summary>
        /// <param name="itemType">Type of product offering</param>
        /// <param name="productIds">Sku or Id of the product(s)</param>
        /// <returns>List of products</returns>
        Task<IEnumerable<InAppBillingProduct>> GetProductInfoAsync(ItemType itemType, string[] productIds, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get all current purchases for a specific product type. If you use verification and it fails for some purchase, it's not contained in the result.
		/// </summary>
		/// <param name="itemType">Type of product</param>
		/// <returns>The current purchases</returns>
		Task<IEnumerable<InAppBillingPurchase>> GetPurchasesAsync(ItemType itemType, CancellationToken cancellationToken = default);


        /// <summary>
        /// Android only: Returns the most recent purchase made by the user for each SKU, even if that purchase is expired, canceled, or consumed.
        /// </summary>
        /// <param name="itemType">Type of product</param>
        /// <returns>The current purchases</returns>
        Task<IEnumerable<InAppBillingPurchase>> GetPurchasesHistoryAsync(ItemType itemType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Purchase a specific product or subscription
        /// </summary>
        /// <param name="productId">Sku or ID of product</param>
        /// <param name="itemType">Type of product being requested</param>
        /// <param name="obfuscatedAccountId">Android: Specifies an optional obfuscated string that is uniquely associated with the user's account in your app.</param>
        /// <param name="obfuscatedProfileId">Android: Specifies an optional obfuscated string that is uniquely associated with the user's profile in your app.</param>
        /// <returns>Purchase details</returns>
        /// <exception cref="InAppBillingPurchaseException">If an error occurs during processing</exception>
        Task<InAppBillingPurchase> PurchaseAsync(string productId, ItemType itemType, string obfuscatedAccountId = null, string obfuscatedProfileId = null, string subOfferToken = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// (Android specific) Upgrade/Downgrade a previously purchased subscription
        /// </summary>
        /// <param name="newProductId">Sku or ID of product that will replace the old one</param>
        /// <param name="purchaseTokenOfOriginalSubscription">Purchase token of original subscription (can not be null)</param>
        /// <param name="replacementMode">Proration mode (1 - ImmediateWithTimeProration, 2 - ImmediateAndChargeProratedPrice, 3 - ImmediateWithoutProration, 4 - Deferred)</param>
        /// <returns>Purchase details</returns>
        /// <exception cref="InAppBillingPurchaseException">If an error occurs during processing</exception>
        Task<InAppBillingPurchase> UpgradePurchasedSubscriptionAsync(string newProductId, string purchaseTokenOfOriginalSubscription, SubscriptionReplacementMode replacementMode = SubscriptionReplacementMode.WithTimeProration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Consume a purchase with a purchase token.
        /// </summary>
        /// <param name="productId">Product id or sku</param>
        /// <param name="transactionIdentifier">Original Purchase Token</param>
		/// <returns>If consumed successful</returns>
        /// <exception cref="InAppBillingPurchaseException">If an error occurs during processing</exception>
        Task<bool> ConsumePurchaseAsync(string productId, string transactionIdentifier, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get receipt data on iOS
        /// </summary>
        string ReceiptData { get; }

        /// <summary>
        /// Gets if user is allowed to make a payment.
        /// </summary>
        bool CanMakePayments { get; }


        /// <summary>
        /// iOS: Displays a sheet that enables users to redeem subscription offer codes that you configure in App Store Connect.
        /// </summary>
        void PresentCodeRedemption();
    }
}
