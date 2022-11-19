namespace Legasy2.Core.Service.DataBase
{
    public class DataBase
    {
        public CaseDataBase Case;
        public DataBase(string _connectionString, List<string> _dataBaseName)
        {
            Case = new CaseDataBase(Path.Combine(_connectionString, _dataBaseName[0]));
        }
    }
}
