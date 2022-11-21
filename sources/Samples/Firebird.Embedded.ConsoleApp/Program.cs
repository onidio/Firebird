using System;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace Firebird.Embedded.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Set the ServerType to 1 for connect to the embedded server
            var connectionString = @"user=SYSDBA;password=masterkey;database=EMPLOYEE.FDB;servertype=1;client library=fbclient.dll";

            #region MyRegion

            var binDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var rootDirectory = Environment.CurrentDirectory;
            var dataDirectory = Path.GetFullPath(Path.Combine(rootDirectory, "App_Data"));
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            //var @lock=Environment.GetEnvironmentVariable("FIREBIRD_LOCK");
            //var tmp = Environment.GetEnvironmentVa''riable("FIREBIRD_TMP");

            //Environment.SetEnvironmentVariable("FIREBIRD_TMP", Path.Combine(dataDirectory, "FIREBIRD_TMP"));
            //Environment.SetEnvironmentVariable("FIREBIRD_LOCK", Path.Combine(dataDirectory, "FIREBIRD_LOCK"));

#if AnyCPU
            var fbserverDirectory = "FES-x64";
#endif
#if x86
            var fbserverDirectory = "FES-x86";
#endif
#if x64
            var fbserverDirectory = "FES-x64";
#endif
            var connection = new FbConnectionStringBuilder(connectionString);
            connection.ClientLibrary = Path.Combine(Path.Combine(binDirectory, fbserverDirectory), Path.GetFileName(connection.ClientLibrary));
            connection.Database = Path.Combine(dataDirectory, Path.GetFileName(connection.Database));
            connectionString = connection.ToString();
            if (!File.Exists(connection.Database))
            {
                FbConnection.CreateDatabase(connectionString, pageSize: 16384);
            }
            #endregion

            var myConnection1 = new FbConnection(connectionString);
            var myConnection2 = new FbConnection(connectionString);
            var myConnection3 = new FbConnection(connectionString);

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
