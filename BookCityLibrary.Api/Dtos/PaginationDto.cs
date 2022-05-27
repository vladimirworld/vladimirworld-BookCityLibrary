namespace BookCityLibrary.Api.Dtos;

public class PaginationDto
{
    public int Page { get; set; } = 1;
    
    public int QuantityPerPage { get; set; } = 10;
}
