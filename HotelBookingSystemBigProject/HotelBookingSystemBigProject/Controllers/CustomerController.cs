using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace HotelBookingSystemBigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _repo;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        public CustomerController(IGenericRepository<Customer> repo, IMapper mapper,ICustomerService customerService)
        {
            _repo = repo;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet("getallcustomers")]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("getcustomerbyid")]
        public async Task<IActionResult> GetCustomerByIDAsync(int customerid)
        {
            var customer = await _customerService.GetCustomerByIDAsync(customerid);
            return Ok(customer);
        }

        [HttpPut("updatecustomerinfo")]
        public async Task<IActionResult> UpdateCustomerInformationByIDAsync(int customerid, UpdateCustomerInfoDTO updateCustomerInfo)
        {
            var updatedCustomer = await _customerService.UpdateCustomerInformationByIDAsync(customerid, updateCustomerInfo);
            return Ok(updatedCustomer);
        }

        [HttpPut("updateaccountstatus")]
        public async Task<IActionResult> UpdateAccountStatusAsync(int customerid, string status)
        {
            var updatedAccount = await _customerService.UpdateAccountStatusAsync(customerid, status);
            return Ok(updatedAccount);
        }

        [HttpDelete("deletecustomerbyid")]
        public async Task DeleteCustomerByIDAsync(int customerid)
        {
            await _customerService.DeleteCustomerByIDAsync(customerid);
        }
    }
}
