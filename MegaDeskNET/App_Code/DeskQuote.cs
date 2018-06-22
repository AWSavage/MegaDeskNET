using System;
using System.IO;

namespace MegaDesk
{
    public class DeskQuote
    {
        //public int[,] readTxtToArray()
        //{
        //    int[,] shippingArray = new int[3, 3];
        //    string[] file = File.ReadAllLines(@"rushOrderPrices.txt");
        //    int temp = 0;
        //    for (int r = 0; r < 3; r++)
        //    {
        //        for (int c = 0; c < 3; c++)
        //        {
        //            shippingArray[r, c] = Int32.Parse(file[temp++]);
        //        }
        //    }
        //    return shippingArray;
        //}

        //public enum RushShippingChoice
        //{
        //    Rush3Days,
        //    Rush5Days,
        //    Rush7Days,
        //    Standard14Days
        //}

        // shipping speed strings
        //public static readonly string[] ShippingSpeeds = {
        //    "Rush - 3 Days", // 0
        //    "Rush - 5 Days", // 1
        //    "Rush - 7 Days", // 2
        //    "Standard - 14 Days" }; // 3

        //var db = Database.Open("QuoteDB");

        // price constants
        private const decimal BasePrice = 200.00M;
        private const decimal SurfaceAreaPrice = 1.00M; // for every sq. in. > 1000
        private const decimal DrawerPriceEach = 50.00M;
        private const decimal SurfaceMaterialPriceOak = 200.00M;
        private const decimal SurfaceMaterialPriceLaminate = 100.00M;
        private const decimal SurfaceMaterialPricePine = 50.00M;
        private const decimal SurfaceMaterialPriceRosewood = 300.00M;
        private const decimal SurfaceMaterialPriceVeneer = 125.00M;
        private const decimal ShippingPriceStandard = 0.00M;

        // shipping price index order: {small desk, medium desk, large desk}
        private const int MediumDesk = 1000;
        private const int LargeDesk = 2001;

        // properties
        public Desk Desk { get; set; }
        public string CustomerName { get; set; }
        public string ShippingSpeed { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Total { get; set; }

        // methods
        public decimal CalculateQuote()
        {
            var totalPrice = BasePrice;
            totalPrice += CalculateSurfaceAreaPrice();
            totalPrice += CalculatePriceOfDrawers();
            totalPrice += GetSurfaceMaterialPrice();
            totalPrice += GetShippingPrice();

            Total = totalPrice;

            return totalPrice;
        }

        private decimal GetSurfaceMaterialPrice()
        {
            var surfaceMaterialPrice = 0.00M;

            switch (Desk.SurfaceMaterial)
            {
                case "Laminate":
                    surfaceMaterialPrice = SurfaceMaterialPriceLaminate;
                    break;
                case "Oak":
                    surfaceMaterialPrice = SurfaceMaterialPriceOak;
                    break;
                case "Pine":
                    surfaceMaterialPrice = SurfaceMaterialPricePine;
                    break;
                case "Rosewood":
                    surfaceMaterialPrice = SurfaceMaterialPriceRosewood;
                    break;
                case "Veneer":
                    surfaceMaterialPrice = SurfaceMaterialPriceVeneer;
                    break;
            }

            return surfaceMaterialPrice;
        }

        private decimal CalculatePriceOfDrawers()
        {
            return DrawerPriceEach * Desk.Drawers;
        }

        private decimal CalculateSurfaceAreaPrice()
        {
            if (CalculateSurfaceArea() > 1000)
            {
                return (CalculateSurfaceArea() - 1000) * SurfaceAreaPrice;
            }

            return 0.00M; // price if surface area <= 1000
        }

        public decimal GetShippingPrice()
        {
            decimal shippingPrice = 0.00M;

            // check desk size
            var deskSize = GetDeskSizeIndex();

            //int[,] ShipTable = readTxtToArray();

            // check shipping speed
            switch (ShippingSpeed)
            {
                case "Rush: 3 Days":
                    if (deskSize < 1000) { shippingPrice = 60; }
                    else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = 70; }
                    else { shippingPrice = 80; }
                    break;
                case "Rush: 5 Days":
                    if (deskSize < 1000) { shippingPrice = 40; }
                    else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = 50; }
                    else { shippingPrice = 60; }
                    break;
                case "Rush: 7 Days":
                    if (deskSize < 1000) { shippingPrice = 30; }
                    else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = 35; }
                    else { shippingPrice = 40; }
                    break;
                case "Standard: 14 Days":
                    shippingPrice = ShippingPriceStandard;
                    break;
            }

            return shippingPrice;
        }


        private int CalculateSurfaceArea()
        {
            return Desk.Width * Desk.Depth;
        }

        private int GetDeskSizeIndex()
        {
            var surfaceArea = CalculateSurfaceArea();
            if (surfaceArea < MediumDesk)
            {
                return 0; // small desk
            }
            return surfaceArea < LargeDesk ? 1 : 2; // medium desk if true, otherwise large desk
        }
    }
}
