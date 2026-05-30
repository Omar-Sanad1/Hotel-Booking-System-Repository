using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly HotelBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerService(HotelBookingDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerToReturnDTO>> GetAllCustomersAsync()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerToReturnDTO>>(customers);
        }

        public async Task<CustomerToReturnDTO> GetCustomerByIDAsync(int customerid)
        {
            var specifiedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c=>c.ID == customerid);
            if (specifiedCustomer is null)
                throw new Exception("This is no customer with this id");

            return _mapper.Map<Customer,CustomerToReturnDTO>(specifiedCustomer);
        }
        public async Task DeleteCustomerByIDAsync(int customerid)
        {
            var specifiedCustomer = await _dbContext.Customers.FindAsync(customerid);
            if (specifiedCustomer is null)
                throw new Exception("This is no customer with this id");

            _dbContext.Customers.Remove(specifiedCustomer);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<CustomerToReturnDTO> UpdateAccountStatusAsync(int customerid, string status)
        {
            var specifiedCustomer = await _dbContext.Customers.FindAsync(customerid);
            if (specifiedCustomer is null)
                throw new Exception("This is no customer with this id");

            var validStatuses = new[] { "Active", "Inactive", "Blocked" };
            if(!validStatuses.Contains(status))
                throw new Exception("This status is not valid");

            specifiedCustomer.AccountStatus = status;
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Customer, CustomerToReturnDTO>(specifiedCustomer);
        }

        public async Task<CustomerToReturnDTO> UpdateCustomerInformationByIDAsync(int customerid, UpdateCustomerInfoDTO updateCustomerInfo)
        {
            var specifiedCustomer = await _dbContext.Customers.FindAsync(customerid);
            if (specifiedCustomer is null)
                throw new Exception("This is no customer with this id");

            specifiedCustomer.FullName = updateCustomerInfo.FullName;
            specifiedCustomer.PhoneNumber = updateCustomerInfo.PhoneNumber;
            specifiedCustomer.EmailAddress = updateCustomerInfo.EmailAddress;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Customer, CustomerToReturnDTO>(specifiedCustomer);
        }
    }
}
