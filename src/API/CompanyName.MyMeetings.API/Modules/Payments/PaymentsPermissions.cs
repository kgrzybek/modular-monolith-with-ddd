namespace CompanyName.MyMeetings.API.Modules.Payments
{
    public class PaymentsPermissions
    {
        public const string RegisterPayment = "RegisterPayment";
        public const string BuySubscription = "BuySubscription";
        public const string RenewSubscription = "RenewSubscription";
        public const string CreatePriceListItem = "CreatePriceListItem";
        public const string ActivatePriceListItem = "ActivatePriceListItem";
        public const string DeactivatePriceListItem = "DeactivatePriceListItem";
        public const string ChangePriceListItemAttributes = "ChangePriceListItemAttributes";
        public const string GetAuthenticatedPayerSubscription = "GetAuthenticatedPayerSubscription";
        public const string GetPriceListItem = "GetPriceListItem";
    }
}