namespace API_Practice.Services
{
    public interface IMedicinesService
    {
        Task<IEnumerable<Medicine>> GetAll(byte CategoryId = 0);
        Task<Medicine> GetMedicineById(int id);
        Task<Medicine> Add(Medicine medicine);
        Medicine Update(Medicine medicine);
        Medicine Delete(Medicine medicine);
    }
}
