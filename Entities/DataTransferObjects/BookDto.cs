namespace Entities.DataTransferObjects
{
    [Serializable] //serileştirilebilir bir nesne

    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; init; }
    }

    
}

//Data Transfer Object özellikleri
//readonly 
//immutable değişmez
//LINQ
//Ref Type
//Constructor olmalı(DTO)