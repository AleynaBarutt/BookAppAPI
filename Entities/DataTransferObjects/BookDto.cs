namespace Entities.DataTransferObjects
{
    [Serializable] //serileştirilebilir bir nesne

    public record BookDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }
    
}

//Data Transfer Object özellikleri
//readonly 
//immutable değişmez
//LINQ
//Ref Type
//Constructor olmalı(DTO)