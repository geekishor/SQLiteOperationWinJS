using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using SQLite;
using Windows.Storage;

namespace WinRuntimes
{
    [Table("Countries")]
    public sealed class Country
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string Name { get; set; }

        public string CapitalCity { get; set; }

    }
    public sealed class Database
    {
        internal static string dbName = "CountryDb.sqlite";

        public Database()
        {
            SQLiteAsyncConnection dbCon = new SQLiteAsyncConnection(dbName);
        }

        #region IAsyncOperations
        public IAsyncOperation<string> CreateDatabase()
        {
            return CreateDatabaseHelper().AsAsyncOperation();
        }

        public IAsyncOperation<string> InsertRecords(string countryName, string capitalCity)
        {
            return InsertRecordsHelper(countryName, capitalCity).AsAsyncOperation();
        }

        public IAsyncOperation<string> ReadDatabase()
        {
            return ReadDatabaseHelper().AsAsyncOperation();
        }

        public IAsyncOperation<string> UpdateDatabase()
        {
            return UpdateDatabaseHelper().AsAsyncOperation();
        }

        public IAsyncOperation<string> DeleteRecord()
        {
            return DeleteRecordHelper().AsAsyncOperation();
        }
        #endregion

        #region Helpers
        private async Task<string> CreateDatabaseHelper()
        {
            try
            {
                //Database will be created in local folder
                SQLiteAsyncConnection connection = new SQLiteAsyncConnection(dbName);
                await connection.CreateTableAsync<Country>();
                return "success";
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        private async Task<string> InsertRecordsHelper(string countryName, string capitalCity)
        {
            try
            {
                SQLiteAsyncConnection connection = new SQLiteAsyncConnection(dbName);
                var Country = new Country()
                {
                    Name = countryName,
                    CapitalCity = capitalCity
                };
                await connection.InsertAsync(Country);
                return "success";
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        private async Task<string> ReadDatabaseHelper()
        {
            try
            {
                string countryName = string.Empty;
                string capitalCity = string.Empty;
                SQLiteAsyncConnection dbCon = new SQLiteAsyncConnection(dbName);
                var result = await dbCon.QueryAsync<Country>("Select * from Countries LIMIT 1");
                foreach (var item in result)
                {
                    countryName = item.Name;
                    capitalCity = item.CapitalCity;
                }

                return countryName + "," + capitalCity;
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        private async Task<string> UpdateDatabaseHelper()
        {
            try
            {
                SQLiteAsyncConnection dbCon = new SQLiteAsyncConnection(dbName);
                var Country = await dbCon.Table<Country>().Where(country => country.Name.StartsWith("France")).FirstOrDefaultAsync();

                if (Country != null)
                {
                    Country.CapitalCity = "Paris";
                    await dbCon.UpdateAsync(Country);
                }
                return "success";
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        private async Task<string> DeleteRecordHelper()
        {
            try
            {
                SQLiteAsyncConnection dbCon = new SQLiteAsyncConnection(dbName);
                var Country = await dbCon.Table<Country>().Where(country => country.Name.StartsWith("France")).FirstOrDefaultAsync();

                if (Country != null)
                {
                    await dbCon.DeleteAsync(Country);
                }
                return "success";
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }
        #endregion
    }
}
