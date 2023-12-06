using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        using (BakeryDbContext ctx = new BakeryDbContext())
        {
            // Выполняем разные методы, содержащие операции выборки и изменения данных
            Console.WriteLine("==== Будут выполнены выборки данных (нажмите любую клавишу) ====");
            Console.ReadKey();
            Select(ctx);

            Console.WriteLine("==== Будут выполнены вставки записей (нажмите любую клавишу) ====\n");
            Console.ReadKey();
            Insert(ctx);

            Console.WriteLine("==== Будет выполнено удаление данных (нажмите любую клавишу) ====");
            Console.ReadKey();
            Delete(ctx);

            Console.WriteLine("====== Будет выполнено обновление данных (нажмите любую клавишу) ========");
            Console.ReadKey();
            Update(ctx);
        }
    }

    // Явное указание типа элемента коллекции
    static void Print<T>(string sqltext, IEnumerable<T> items)
    {
        Console.WriteLine(sqltext);

        if (!items.Any())
        {
            Console.WriteLine("Нет записей\n");
            Console.ReadKey();
            return;
        }

        var properties = typeof(T).GetProperties();

        foreach (var item in items)
        {
            foreach (var prop in properties)
            {
                Console.Write($"{prop.GetValue(item),-20} | ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nНажмите любую клавишу\n");
        Console.ReadKey();
    }


    // Выборки данных | Задания 1-5
    static void Select(BakeryDbContext db)
    {
        // Задание 1
        var supplies = db.Supplies.Take(5);
        string comment = "1. Результат выполнения запроса на выборку записей из таблицы Supplies: \r\n";
        Print(comment, supplies);

        // Задание 2
        var filteredOrders = db.Orders.Where(o => o.Quantity > 15000);
        comment = "2. Результат выполнения запроса на выборку заказов с количеством > 15000 из таблицы Orders: \r\n";
        Print(comment, filteredOrders);

        // Задание 3: Выборка данных из таблицы заказов, сгруппированных по типу продукта,
        // с выводом минимального и максимального количества продукта в каждой группе
        var ordersByProductType = from o in db.Orders
                                  group o.Quantity by o.ProductType into gr
                                  select new
                                  {
                                      ProductType = gr.Key,
                                      MinQuantity = gr.Min(),
                                      MaxQuantity = gr.Max()
                                  };

         comment = "3. Результат выполнения запроса на выборку данных из таблицы Orders,\n" +
                         "записи сгруппированы по типу продукта с выводом минимального и максимального количества: \r\n";
        Print(comment, ordersByProductType.ToList());


        // Задание 4
        var productOrderDetails = db.OrderDetails
            .Join(db.BakeryProducts, od => od.ProductName, p => p.Name, (od, p) => new
            {
                od.CustomerName,
                p.Name,
                p.Type,
                p.Description
            });
        comment = "4. Результат выполнения запроса на выборку данных из таблиц OrderDetails и BakeryProducts: \r\n";
        Print(comment, productOrderDetails.Take(5));

        // Задание 5
        var filteredProductsWithIngredients = db.ProductsWithIngredients
            .Where(pwi => pwi.IngredientQuantity > 10)
            .Join(db.BakeryProducts, pwi => pwi.ProductId, p => p.ProductId, (pwi, p) => new
            {
                p.Name,
                p.Type,
                p.Description,
                pwi.IngredientName,
                pwi.IngredientType,
                pwi.IngredientQuantity
            });
        comment = "5. Результат выполнения запроса на выборку данных из таблиц BakeryProducts и ProductsWithIngredients " +
                  "c IngredientQuantity > 10, с использованием Join: \r\n";
        Print(comment, filteredProductsWithIngredients.Take(5));
    }

    // Вставка данных
    static void Insert(BakeryDbContext db)
    {
        // Задание 6
        var newProduct = new BakeryProduct
        {
            Name = "Новый хлеб",
            Type = "Белый",
            Description = "Очень вкусный",
            // Другие свойства...
        };

        db.BakeryProducts.Add(newProduct);
        db.SaveChanges();

        Console.WriteLine($"6. В таблицу BakeryProducts успешно добавлена запись:\n\tName:{newProduct.Name}\n\tType:{newProduct.Type}\n");

        // Задание 7
        var newSupply = new Supply
        {
            
            Supplier = "Новый поставщик",
            ProductName = "Новый продукт",
            Quantity = 20,
            Price = 15,
            SupplyDate = DateTime.Now
            // Другие свойства...
        };

        db.Supplies.Add(newSupply);
        db.SaveChanges();

        Console.WriteLine($"7. В таблицу Supplies успешно добавлена запись:\n\tSupplier:{newSupply.Supplier}\n\tQuantity:{newSupply.Quantity}\n");
    }

    // Удаление данных
    static void Delete(BakeryDbContext db)
    {
        // Задание 8
        var productToDelete = db.BakeryProducts.FirstOrDefault();

        if (productToDelete != null)
        {
            db.BakeryProducts.Remove(productToDelete);
            db.SaveChanges();
            Console.WriteLine("\n8. Из таблицы BakeryProducts была успешно удалена первая запись\n");
        }
        else
        {
            Console.WriteLine("В таблице BakeryProducts нет такой записи!");
        }

        // Задание 9
        var supplyToDelete = db.Supplies.FirstOrDefault();

        if (supplyToDelete != null)
        {
            db.Supplies.Remove(supplyToDelete);
            db.SaveChanges();
            Console.WriteLine($"\n9. Из таблицы Supplies была успешно удалена первая запись с id={supplyToDelete.SupplyId}\n");
        }
        else
        {
            Console.WriteLine("В таблице Supplies нет такой записи!");
        }
    }

    // Обновление данных
    static void Update(BakeryDbContext db)
    {
        // Задание 10
        var orderToUpdate = db.Orders.FirstOrDefault();

        if (orderToUpdate != null)
        {
            orderToUpdate.Quantity = 10;
            db.SaveChanges();
            Console.WriteLine($"\n10. В таблице Orders успешно обновлена запись с id={orderToUpdate.OrderId}\n");
        }
        else
        {
            Console.WriteLine("В таблице Orders нет такой записи!");
        }
    }
}
