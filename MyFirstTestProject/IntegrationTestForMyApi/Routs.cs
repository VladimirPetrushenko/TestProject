namespace IntegrationTestForMyApi
{
    public static class Routs
    {
        public const string Order = "https://localhost/api/order/";
        public const string Product = "https://localhost/api/product/";
        public const string Person = "https://localhost/api/person/";
        public const string ConnectionStrings = "Server=(localdb)\\mssqllocaldb;Database=MyApi;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const string BadRoute = "https://localhost/api/something";
    }
}
