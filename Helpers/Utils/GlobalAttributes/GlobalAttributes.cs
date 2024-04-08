namespace SuperHeros.Helpers.Utils.GlobalAttributes
{
    public static class GlobalAttributes
    {
        public static MySqlConnection mySqlConnection = new MySqlConnection();
    }

    public class MySqlConnection
    {
        public string ConnectionString { get; set; }
    }
}
