using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Service.DataBase
{
    public class CUDDataBase<T>
    {
        public static SQLiteAsyncConnection connection;

        public async Task<int> SaveAsync(T obj) => await Save(obj);
        public async Task<int> UpdateAsync(T obj) => await Update(obj);
        public async Task<int> DeleteAsync(T obj) => await Delete(obj);

        public static async Task<int> Save(T obj)
        {
            try
            {
                return await connection.InsertAsync(obj);
            }
            catch
            {
                return -1;
            }
        }

        public static async Task<int> Delete(T obj)
        {
            try
            {
                return await connection.DeleteAsync(obj);
            }
            catch
            {
                return -1;
            }
        }

        public static async Task<int> Update(T obj)
        {
            try
            {
                return await connection.UpdateAsync(obj);
            }
            catch
            {
                return -1;
            }
        }
    }
}
