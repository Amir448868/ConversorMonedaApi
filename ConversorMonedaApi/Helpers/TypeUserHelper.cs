namespace ConversorMonedaApi.Helpers
{
    public static class TypeUserHelper
    {
        public static int GetRole(string typeUser)
        {
            if (typeUser.ToLower() == "admin")
            {
                return 1000000;
            }
            else if (typeUser.ToLower() == "free")
            {
                return 20;
            }
            else if (typeUser.ToLower() == "premium")
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
    }
}
    

