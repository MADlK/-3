using System;
using CinemaTicketSystem;
using Xunit;
namespace CinemaTicketSystemTests
{
    public class CinemaTicketSystemTests
    {
        [Fact]
        public void CalculatePrice_ShouldReturnZero_Under6()
        {
            const decimal Result = 0;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 5;
            //TRequest.IsStudent = false;
            //TRequest.SessionTime = new TimeSpan(9,30,0);
            //TRequest.IsVip = false;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }

        [Fact]
        public void CalculatePrice_ShouldReturn300_NoDiscounts()
        {
            const decimal Result = 300;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 55;
            TRequest.IsStudent = false;
            TRequest.SessionTime = new System.TimeSpan(12, 30, 0);
            TRequest.IsVip = false;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }



        [Fact]
        public void CalculatePrice_ShouldReturn240_IsStudent()
        {
            const decimal Result = 240;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 18;
            TRequest.IsStudent = true;
            TRequest.SessionTime = new TimeSpan(12, 30, 0);
            TRequest.IsVip = false;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }

        //багрепорт
        [Fact]
        public void CalculatePrice_ShouldReturn180_IsChildOver5()
        {
            const decimal Result = 180;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 6;
            TRequest.IsStudent = false;
            TRequest.SessionTime = new TimeSpan(12, 30, 0);
            TRequest.IsVip = false;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }




        [Fact]
        public void CalculatePrice_ShouldReturn210_OnWednesday()
        {
            const decimal Result = 210;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 55;
            TRequest.IsStudent = false;
            TRequest.SessionTime = new TimeSpan(12, 30, 0);
            TRequest.IsVip = false;
            TRequest.Day = DayOfWeek.Wednesday;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }




        [Fact]
        public void CalculatePrice_ShouldReturn255_Under12am()
        {
            const decimal Result = 255;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 55;
            TRequest.IsStudent = false;
            TRequest.SessionTime = new TimeSpan(11, 30, 0);
            TRequest.IsVip = false;
            
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }



        //багрепорт
        [Fact]
        public void CalculatePrice_ShouldReturn150_IsPensioner()
        {
            const decimal Result = 150;
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = 100;
            TRequest.IsStudent = false;
            TRequest.SessionTime = new TimeSpan(12, 30, 0);
            TRequest.IsVip = false;
            TRequest.Day = DayOfWeek.Monday;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }







        [Theory]
        [InlineData(600,55,false,DayOfWeek.Monday,12,30,0 )]
        [InlineData(480,20,true,DayOfWeek.Monday,11,30,0 )]
        [InlineData(420,20,true,DayOfWeek.Wednesday,11,30,0 )]
        [InlineData(480,20,true,DayOfWeek.Monday,12,30,0 )]
        [InlineData(510,55,false,DayOfWeek.Monday,11,30,0 )]
        [InlineData(0,3,false,DayOfWeek.Monday,12,30,0 )]
        [InlineData(0,3,false,DayOfWeek.Monday,11,30,0 )]
        [InlineData(0,3,false,DayOfWeek.Wednesday,11,30,0 )]
        [InlineData(420,55,false,DayOfWeek.Wednesday,12,30,0 )]
        [InlineData(420,55,false,DayOfWeek.Wednesday,11,30,0 )]
        [InlineData(300,66,false,DayOfWeek.Monday,12,30,0 )]//Уточнить (Скидка песионерам не работает в целом)

        public void CalculatePrice_IsVIP_AllOptions(decimal Result,  int age, bool IsStudent,  DayOfWeek day, int hours, int minutes, int seconds)
        {
            
            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = age;
            TRequest.IsStudent = IsStudent;
            TRequest.SessionTime = new TimeSpan(hours, minutes, seconds);
            TRequest.IsVip = true;
            TRequest.Day = day;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }






        [Theory]
        [InlineData(150, 66, false, DayOfWeek.Wednesday, 11, 30, 0)]
        [InlineData(210, 20, true, DayOfWeek.Wednesday, 11, 30, 0)]//Уточнить (Скидка песионерам не работает в целом)
        [InlineData(0, 4, true, DayOfWeek.Wednesday, 11, 30, 0)]
        [InlineData(0, 3, false, DayOfWeek.Monday, 11, 30, 0)]
        

        public void CalculatePrice_ShouldChooseMaxDiscounts_AllOptions(decimal Result, int age, bool IsStudent, DayOfWeek day, int hours, int minutes, int seconds)
        {

            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new TicketRequest();
            TRequest.Age = age;
            TRequest.IsStudent = IsStudent;
            TRequest.SessionTime = new TimeSpan(hours, minutes, seconds);
            TRequest.Day = day;
            decimal ReturnedValue = tpc.CalculatePrice(TRequest);
            Assert.Equal(Result, ReturnedValue);
        }


        [Fact]
        public void CalculatePrice_ShouldReturnArgumentNullException()
        {
            
            TicketPriceCalculator tpc = new TicketPriceCalculator();
           
           

            Assert.Throws<ArgumentNullException>(() =>tpc.CalculatePrice(null));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]//так вроде не должно быть
        [InlineData(120)]//так вроде не должно быть
        [InlineData(121)]
        public void CalculatePrice_ShouldReturnArgumentOutOfRangeException(int age)
        {

            TicketPriceCalculator tpc = new TicketPriceCalculator();
            TicketRequest TRequest = new();
            TRequest.Age = age;


            Assert.Throws<ArgumentNullException>(() => tpc.CalculatePrice(null));
        }
    }
}