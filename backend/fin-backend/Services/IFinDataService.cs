namespace fin_backend.Services
{
    public interface IFinDataService
    {
        Task<List<Symbol>> SearchForSymbol(string symbol);
        //Task<string> SearchForSymbol(string symbol);
    }
}
