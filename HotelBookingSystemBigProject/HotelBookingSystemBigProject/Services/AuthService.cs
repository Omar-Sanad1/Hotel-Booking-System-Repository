using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using HotelBookingSystemBigProject.Helper;
using HotelBookingSystemBigProject.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingSystemBigProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Customer> _repo;
        private readonly JWT _jwt;
        public AuthService(IGenericRepository<Customer> repo,IOptions<JWT> jwt)
        {
            _repo = repo;
            _jwt = jwt.Value;
        }

        public async Task<CustomerToReturnDTO> RegisterAsync(RegisterModel model)
        {
            var existingUser = _repo.GetAll().FirstOrDefault(c => c.EmailAddress == model.Email);
            if (existingUser is not null)
            {
                throw new Exception("This email is already registered");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var customer = new Customer
            {
                FullName = model.FullName,
                EmailAddress = model.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = model.PhoneNumber,
                AccountStatus = "Active"
            };

            _repo.Add(customer);

            return new CustomerToReturnDTO
            {
                ID = customer.ID,
                FullName = customer.FullName,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                AccountStatus = customer.AccountStatus,
                Token = await CreateTokenAsync(customer)
            };

        }

        public async Task<CustomerToReturnDTO> GetTokenAsync(TokenRequestModel model)
        {
            var customerToReturnDTO = new CustomerToReturnDTO();
            var customerEmail = _repo.GetAll().FirstOrDefault(c => c.EmailAddress == model.Email);
            if(customerEmail is null)
            {
                throw new Exception("This email is incorrect");
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(model.Password , customerEmail.PasswordHash);
            if (!verifyPassword)
            {
                throw new Exception("This password is incorrect");
            }

            return new CustomerToReturnDTO
            {
                ID = customerEmail.ID,
                FullName = customerEmail.FullName,
                EmailAddress = customerEmail.EmailAddress,
                PhoneNumber = customerEmail.PhoneNumber,
                AccountStatus = customerEmail.AccountStatus,
                Token = await CreateTokenAsync(customerEmail)
            };
        }

        public Task<string> CreateTokenAsync(Customer customer)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,customer.FullName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,customer.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier,customer.ID.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            var signingCrediantls = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer:_jwt.Issuer,
                audience:_jwt.Audience,
                claims:claims,
                signingCredentials:signingCrediantls,
                expires:DateTime.Now.AddDays(_jwt.DurationInDays)
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
        }
    }
}
