using EF_Training.Models;
using EF_Training;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
// See https://aka.ms/new-console-template for more information



Console.WriteLine("Hello, World!");

var dbContext = new ApplicationDBContext();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

//
// ДОБАВЛЕНИЕ ДАННЫХ
//

// Goods
var macBook = new Good
{
    Name = "Apple MacBook",
    Tags = new[] {"Apple", "Notebook"}
};
var iPhone = new Good
{
    Name = "Apple IPhone",
    Tags = new[] { "Apple", "SmartPhone" }
};
var hotDog = new Good
{
    Name = "HotDog",
    Tags = new[] { "Food", "Meat" }
};


//Shops
var IDom = new Shop
{
    Name = "IDom"
};
var HotChillyDogs = new Shop
{
    Name = "HotChillyDogs",
    Director = "Motya"
};


//Goods to Shops
var GoodsShops = new[]
{
    new GoodShop
    {
        Good = macBook,
        Shop = IDom,
        HowMany = 2
    },
    new GoodShop
    {
        Good = iPhone,
        Shop = IDom,
        HowMany = 13
    },
    new GoodShop
    {
        Good = hotDog,
        Shop = HotChillyDogs,
        HowMany = 54
    }
};

dbContext.AddRange(GoodsShops);


//Clients
var Motya = new Client
{
    Nickname = "Motya"
};

var Doych = new Client
{
    Nickname = "Nastuha"
};


//ORDERS
var orderToIdom = new Order
{
    Shop = IDom,
    Client = Doych
};
var orderIphone = new GoodOrder
{
    Order = orderToIdom,
    Good = iPhone,
    HowMany = 1
};
var orderMacBook = new GoodOrder
{
    Order = orderToIdom,
    Good = macBook,
    HowMany = 2
};
dbContext.Add(orderMacBook);
dbContext.Add(orderIphone);

var orderHotDog = new GoodOrder
{
    Order = new Order
    {
        Shop = HotChillyDogs,
        Client = Motya
    },
    Good = hotDog,
    HowMany = 10
};
dbContext.Add(orderHotDog);

dbContext.SaveChanges();


//
// ВЫВОД ДАННЫХ
//
var orders = dbContext.Orders
    .Include(x => x.Client)
    .Include(x => x.Shop)
    .Include(x => x.Goods)
    .ToList();

Console.WriteLine("======= ORDERS =========");
Console.WriteLine(new string('-', 80));
foreach (Order order in orders)
{
    Console.WriteLine("Заказ номер " + order.Id + ". Дата: " + order.Date);
    Console.WriteLine("Заказал " + order.Client.Nickname + " в магазине " + order.Shop.Name + ".");
    Console.WriteLine("Товары:");
    foreach (GoodOrder goodOrder in order.Goods)
    {
        Console.WriteLine($"=== {goodOrder.Good.Name}, кол-во: {goodOrder.HowMany}");
    }
    Console.WriteLine(new string('-', 80));
}

var shopsQ = dbContext.Shops
    .SelectMany(
    shop => dbContext.GoodsShops.Where(x => x.ShopId == shop.Id),
    (shop, goodShop) => shop);

var shops = shopsQ.ToList();


Console.WriteLine("======== SHOPS ==========");
Console.WriteLine(new string('-', 80));
foreach (var shop in shops)
{
    Console.WriteLine($"Магазин {shop.Name}.");
    Console.WriteLine("Товары:");

    foreach (GoodShop gs in shop.ShopGoods)
    {
        Console.WriteLine($"=== {gs.Good.Name}, кол-во: {gs.HowMany}");
    }
    Console.WriteLine(new string('-', 80));

}
//
// ОЮНОВЛЕНИЕ ДАННЫХ
//


