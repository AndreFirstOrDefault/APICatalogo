namespace APICatalogo.Pagination;

public class ProdutosParameters
{
    const int maxPageSixe = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSixe ) ? maxPageSixe : value;
        }
    }
}
