namespace TestProject.Core.Fetures.Basket_ss.Queries.Response
{
    public class GetBasket_sByBasketIdResponse
    {
        public int Id { get; set; }


        public int basketId { get; set; }



        public int itemId { get; set; }

        public int quantity { get; set; }

        public decimal totoal { get; set; }

        public DateTime date { get; set; }

        public string itemName { get; set; }
    }
}

