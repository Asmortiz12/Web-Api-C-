public class HelloWordServices : IHelloWordServices
    {
        public string GetHelloWord()
        {
            return "Hello Word";
        }  

    }

public interface IHelloWordServices
{
    string GetHelloWord();
}
