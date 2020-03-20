using Library.Entities;
using Library.Models.DTO;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Controllers
{
    public class BookBorrowsInitTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BookBorrowsInitTests()
        {
            _server = ServerFactory.GetServerInstance();
            _client = _server.CreateClient();


            using (var scope = _server.Host.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<LibraryContext>();

     

            }
        }

        [Fact]
        public async Task AddBookBorrow_201Created() {

            var newBook = new BookBorrowDto
            {
                IdUser = 1,
                IdBook = 1,
                Comment = "sample text"
            };

            var serialized = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");


            var httpResponse = await _client.PostAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows", content);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<BookBorrow>(result);

            Assert.True(response.IdUser == 1);
            Assert.True(response.IdBook == 1);
            

        }

        [Fact]
        public async Task UpdateBookBorrow_200Ok() {
             
            var updated = new UpdateBookBorrowDto
            {
                IdBookBorrow = 2,
                IdUser = 2,
                IdBook = 2,
                DateFrom = new DateTime(),
                DateTo = new DateTime(),
                Comments = "sample text"
            };

            var serialized = JsonConvert.SerializeObject(updated);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PutAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows/{updated.IdBook}", content);

            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<UpdateBookBorrowDto>(result);

          
            Assert.True(response.IdUser == 2);
            Assert.True(response.IdBook == 2);

        }

    }
}
