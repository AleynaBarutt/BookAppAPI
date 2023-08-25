using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record BookDtoForUpdate : BookDtoForManipulaiton
    {
        [Required]
        public int Id { get; set; }
    }
    
}

//Data Transfer Object özellikleri
//readonly 
//immutable değişmez
//LINQ
//Ref Type
//Constructor olmalı(DTO)