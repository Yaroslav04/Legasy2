
using Legasy2.Core.Service;
using Legasy2.Core.Service.DataBase;

namespace Legasy2;

public partial class App : Application
{
    static DataBase dataBase;
    public static DataBase DataBase
    {
        get
        {
            if (dataBase == null)
            {
                dataBase = new DataBase(FileManager.GetPath("Data"), new List<string> {
                        "CaseDataBase.db3",
                    });
            }
            return dataBase;
        }
    }
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
