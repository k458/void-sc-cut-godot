namespace  voidsccut.scripts.main;

public interface IMain
{
    void Log(string s);
    void Login(string name, string password);
    void CreateAccount(string name, string password);
    void Logout();
}