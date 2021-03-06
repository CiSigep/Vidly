﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using AutoMapper;
using Vidly.DTO;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQ = _context.Customers.Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQ = customersQ.Where(c => c.Name.Contains(query));
                
                
            var customersDTO = customersQ.ToList().Select(Mapper.Map<Customer, CustomerDTO>);

            return Ok(customersDTO);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDTO);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDTO.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDTO);
        }

        [HttpPut]
        public void UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var DbCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(DbCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDTO, DbCustomer);


            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var DbCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (DbCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(DbCustomer);
            _context.SaveChanges();
        }
    }
}
