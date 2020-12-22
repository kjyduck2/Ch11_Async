#define ASYNC

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBreakFast
{
	class Program
	{
		static void Main(string[] args)
		{
            Stopwatch sw = new Stopwatch();

#if SYNC
            sw.Start();

            Coffee cup = Coffee.PourCoffee();
            Console.WriteLine("coffee is ready");

            Egg eggs = Egg.FryEggs(2);
            Console.WriteLine("eggs are ready");

            Bacon bacon = Bacon.FryBacon(3);
            Console.WriteLine("bacon is ready");

            Toast toast = Toast.ToastBread(2);
            Toast.ApplyButter(toast);
            Toast.ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Juice oj = Juice.PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");

            sw.Stop();
            Console.WriteLine("END TIME = " + sw.ElapsedMilliseconds.ToString() + " msec");
#endif

#if ASYNC
            sw.Start();

            MainAsync(args).GetAwaiter().GetResult();

            sw.Stop();
            Console.WriteLine("END TIME = " + sw.ElapsedMilliseconds.ToString() + " msec");
#endif

            Console.ReadKey();
        }

        static async Task MainAsync(string[] args)
        {
            Coffee cup = Coffee.PourCoffee();
            Console.WriteLine("coffee is ready");

            var eggsTask = Egg.FryEggsAsync(2);
            var baconTask = Bacon.FryBaconAsync(3);
            var toastTask = Toast.MakeToast_withButterAndJam_Async(2);

            //Egg eggs = await Egg.FryEggs(2);
            var eggs = await eggsTask;
            Console.WriteLine("eggs are ready");

            //Bacon bacon = await Bacon.FryBacon(3);
            var bacon = await baconTask;
            Console.WriteLine("bacon is ready");

            //Toast toast = Toast.ToastBread(2);
            var toast = await toastTask;
            Console.WriteLine("toast is ready");

            Juice oj = Juice.PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        //public async Task<string> ReadyPowder()
        //{
        //    Console.WriteLine("[P-1] 밀가루에 물을 붓는다.");

        //    Console.WriteLine("[P-2] 반죽하다가 후추,소금을 넣는다.");
        //    await Task.Delay(2000);

        //    Console.WriteLine("[P-3] 밀가루 반죽을 완성한다.");

        //    return "밀가루 반죽";
        //}

        //public async Task<string> FryChicken()
        //{
        //    var powder = ReadyPowder();

        //    Console.WriteLine("[C-1] 닭을 손질한다.");

        //    var powderAsync = await powder;
        //    Console.WriteLine("[C-1] 닭은 튀겨야지. {0}에 묻힌다.", powderAsync);

        //    Console.WriteLine("[C-1] 기름에 튀겨 완성한다.");

        //    return "바사삭 오리지널 치킨 완성!";
        //}

    }
    class Juice
    {
        public static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }
    }
    class Toast
    {
        public static async Task<Toast> MakeToast_withButterAndJam_Async(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        public static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");

            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public static Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        public static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");
    }
    class Bacon
    {
        public static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        public static Bacon FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }
    }
    class Egg
    {
        public static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        public static Egg FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }
    }
    class Coffee
    {
        public static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }

}
