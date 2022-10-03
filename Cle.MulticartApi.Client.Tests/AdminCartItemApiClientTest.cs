using Cle.MulticartApi.Client.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Cle.MulticartApi.Client.Tests
{
    public class AdminCartItemApiClientTest: IClassFixture<AdminApiFixture>
    {
        private readonly IAdminCartItemClient adminCartItemClient;

        public AdminCartItemApiClientTest(AdminApiFixture apiFixture)
        { 
            adminCartItemClient = apiFixture.Services.GetRequiredService<IAdminCartItemClient>();
        }

        [Fact]
        public async Task GetCartItemTest()
        {
            var id = new Guid("00bac85e-9388-4862-a6e4-e4e36479f6dd");
            var cartItem = await adminCartItemClient.CartItemGetAsync(id);

            Assert.Equal(200, cartItem.StatusCode);
            Assert.NotNull(cartItem.Result);
            Assert.Equal(id, cartItem.Result.Id);
        }
    }
}