namespace PetStore.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "PetStore";

        public const string AdministratorRoleName = "Administrator";

        public static class Validation
        {
            //Categories
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 30;
            public const string RequiredError = "This field is required!";
            public const string LengthError = "The length is incorrect!";
        }
    }
}
