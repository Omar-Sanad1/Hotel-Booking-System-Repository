using Core.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerToReturnDTO>> GetAllCustomersAsync();
        public Task<CustomerToReturnDTO> GetCustomerByIDAsync(int customerid);
        public Task<CustomerToReturnDTO> UpdateCustomerInformationByIDAsync(int customerid,UpdateCustomerInfoDTO updateCustomerInfo);
        public Task<CustomerToReturnDTO> UpdateAccountStatusAsync(int customerid, string status);
        public Task DeleteCustomerByIDAsync(int customerid);
    }
}
