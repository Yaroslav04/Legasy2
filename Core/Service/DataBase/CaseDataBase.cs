
using System.Formats.Asn1;

namespace Legasy2.Core.Service.DataBase
{
    public class CaseDataBase : CUDDataBase<CaseClass>
    {
        public CaseDataBase(string _connectionString)
        {
            connection = new SQLiteAsyncConnection(_connectionString);
            connection.CreateTableAsync<CaseClass>().Wait();
        }

        public async Task<List<CaseClass>> GetListAsync()
        {
            return await connection.Table<CaseClass>().ToListAsync();
        }

        public async Task<bool> IsCaseExist(string _case)
        {
            var items = await connection.Table<CaseClass>().Where(x => x.CriminalNumber == _case).ToListAsync();
            if (items.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<string>> GetQualificationsAsync()
        {
            List<int> qualificationsInt = new List<int>();
            List<string> qualifications = new List<string>();
            var list = await GetListAsync();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    qualificationsInt.Add(Convert.ToInt32(item.Qualification));
                }

                qualificationsInt = qualificationsInt.Distinct().ToList();
                qualificationsInt = qualificationsInt.OrderBy(x => x).ToList();

                foreach (var item in qualificationsInt)
                {
                    qualifications.Add(item.ToString());
                }
            }

            return qualifications;
        }
    }
}
