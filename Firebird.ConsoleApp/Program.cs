using System;
using FirebirdSql.Data.FirebirdClient;

namespace Firebird.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Set the ServerType to 1 for connect to the embedded server
            string connectionString = @"User=SYSDBA;Password=masterkey;Database=EMPLOYEE.FDB;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;";

            var connString = new FbConnectionStringBuilder(connectionString) {ClientLibrary= @"fbs-x64\fbclient.dll",Database= @"App_Data\EMPLOYEE.FDB" }.ToString();

            FbConnection myConnection1 = new FbConnection(connString);
            FbConnection myConnection2 = new FbConnection(connString);
            FbConnection myConnection3 = new FbConnection(connString);

            try
            {
                // Open two connections.
                Console.WriteLine("Open two connections.");
                myConnection1.Open();
                myConnection2.Open();

                // Now there are two connections in the pool that matches the connection string.
                // Return the both connections to the pool.
                Console.WriteLine("Return both of the connections to the pool.");
                myConnection1.Close();
                myConnection2.Close();

                // Get a connection out of the pool.
                Console.WriteLine("Open a connection from the pool.");
                myConnection1.Open();

                // Get a second connection out of the pool.
                Console.WriteLine("Open a second connection from the pool.");
                myConnection2.Open();

                // Open a third connection.
                Console.WriteLine("Open a third connection.");
                myConnection3.Open();

                // Return the all connections to the pool.
                Console.WriteLine("Return all three connections to the pool.");
                myConnection1.Close();
                myConnection2.Close();
                myConnection3.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
