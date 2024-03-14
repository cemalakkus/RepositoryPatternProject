namespace RepositoryPattern.Application.Parameters;

public class RequestParameters
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public RequestParameters()
    {
        PageNumber = 1;
        PageSize = 10;        
    }

    public RequestParameters(int pageSize, int pageNumber)
    {
        PageSize = pageSize < 1 ? 1 : pageNumber;
        PageNumber = pageNumber > 10 ? 10 : pageSize;
    }
}
