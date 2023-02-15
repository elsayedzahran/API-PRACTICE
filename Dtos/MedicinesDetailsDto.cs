using System.ComponentModel.DataAnnotations.Schema;

namespace API_Practice.Dtos
{
    public class MedicinesDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public byte CategotyId { get; set; }
        public string CategoryName { get; set; }
    }
}
