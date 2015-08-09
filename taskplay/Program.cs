using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace taskplay
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            dosome();//This task will run async will not influence time on bottom
            Task<int> carTask = Task.Factory.StartNew<int>(BookCar);
            Task<int> hotelTask = Task.Factory.StartNew<int>(BookHotel);
            Task<int> planeTask = Task.Factory.StartNew<int>(BookPlane);

            Task hotelFollowupTask = hotelTask.ContinueWith(
                taskPrevious => Console.WriteLine("adding view note for booking {0}", hotelTask.Result));

            hotelFollowupTask.Wait();
            //Even though values not available still waiting for thme to finnish
            Console.WriteLine("Finished booking: carID = {0}, hotelid = {1}, planeID = {2}",
                carTask.Result, hotelTask.Result, planeTask.Result);//Will wait here thilll all is done

            ///Waits for all the task to finnish
            //Task.WaitAll(carTask, hotelTask, planeTask);

            Console.WriteLine("Finished in {0} sec.", sw.ElapsedMilliseconds / 1000);//Not do this before all not finished

           // int carid = BookCar();
          //  int planeid = BookPlane();
         //   int hotelid = BookHotel();

           // Console.WriteLine("Finished in {0} sec.", sw.ElapsedMilliseconds / 1000);

/*            //This is the serial way
            Stopwatch sw = Stopwatch.StartNew();
            int carid = Bookcar();
            int planeid = Bookplane();
            int hotelid = Bookhotel();

            Console.WriteLine("Finished in {0} sec.", sw.ElapsedMilliseconds / 1000);
*/

            Task TaskA = new Task(() => dostuff());//Start another task here without return value
            TaskA.Start();
            Console.WriteLine("TaskA is doing stuff");
            TaskA.Wait();
            
            Console.WriteLine("Did I wait at wait");


            Console.ReadLine();
        }

        //async and await keyword, good for GUI
        private static async void dosome()
        {
            //string v = await Task.Run(() => dostuffasync());
            await Task.Run(() => dostuffasync());
        }
        //if this does not teturn a value then i can not write to console
        private static void dostuffasync()
        {
            Debug.WriteLine("Async task running");
            Thread.Sleep(3000);
            Debug.WriteLine("Done waitign async");
            //return "done";
        }

        private static void dostuff()
        {
            Console.WriteLine("Im here doing stuff!!!!!!");
            Thread.Sleep(3000);
            Console.WriteLine("Done doing stuff");
        }

        static Random rand = new Random();

        private static int BookHotel()
        {
            Console.WriteLine("Booking hotel...");
            Thread.Sleep(4000);
            Console.WriteLine("Done booking the hotel...");
            return rand.Next(100);
        }

        private static int BookPlane()
        {
            Console.WriteLine("Booking plane...");
            Thread.Sleep(2000);
            Console.WriteLine("Done booking the plane...");
            return rand.Next(100);
        }

        private static int BookCar()
        {
            Console.WriteLine("Booking car...");
            Thread.Sleep(3000);
            Console.WriteLine("Done booking the car...");
            return rand.Next(100);
        }

        
    }
}
