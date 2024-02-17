namespace SoftUniBazar.Data.DataConstants
{
    public static class ValidationConstants
    {
        public const int AdNameMinLength = 5;
        public const int AdNameMaxLength = 25;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 15;

        public const int DescriptionMinLength = 15;
        public const int DescriptionMaxLength = 250;


        public const string DataFormat = "yyyy-MM-dd H:mm";
        public const string LengthErrorMessage = "The field {0} must be between {2} and {1} characters long!";
    }
}
