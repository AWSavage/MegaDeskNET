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
        //public RushShippingChoice ShippingSpeed { get; set; }
        public string ShippingSpeed { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Total { get; set; }

        // methods
        public decimal CalculateQuote()
        {
            var totalPrice = BasePrice;
            totalPrice += CalculateSurfaceAreaPrice();
            totalPrice += CalculatePriceOfDrawers();
            //totalPrice += GetSurfaceMaterialPrice();
            //totalPrice += GetShippingPrice();

            Total = totalPrice;

            return totalPrice;
        }

        //private decimal GetSurfaceMaterialPrice()
        //{
        //    var surfaceMaterialPrice = 0.00M;

        //    switch (Desk.SurfaceMaterial)
        //    {
        //        case Desk.DesktopSurfaceMaterial.Laminate:
        //            surfaceMaterialPrice = SurfaceMaterialPriceLaminate;
        //            break;
        //        case Desk.DesktopSurfaceMaterial.Oak:
        //            surfaceMaterialPrice = SurfaceMaterialPriceOak;
        //            break;
        //        case Desk.DesktopSurfaceMaterial.Pine:
        //            surfaceMaterialPrice = SurfaceMaterialPricePine;
        //            break;
        //        case Desk.DesktopSurfaceMaterial.Rosewood:
        //            surfaceMaterialPrice = SurfaceMaterialPriceRosewood;
        //            break;
        //        case Desk.DesktopSurfaceMaterial.Veneer:
        //            surfaceMaterialPrice = SurfaceMaterialPriceVeneer;
        //            break;
        //    }

        //    return surfaceMaterialPrice;
        //}

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

        //public decimal GetShippingPrice()
        //{
        //    decimal shippingPrice = 0.00M;

        //    // check desk size
        //    var deskSize = GetDeskSizeIndex();

        //    int[,] ShipTable = readTxtToArray();

        //    // check shipping speed
        //    switch (ShippingSpeed)
        //    {
        //        case RushShippingChoice.Rush3Days:
        //            if (deskSize < 1000) { shippingPrice = ShipTable[0, 0]; }
        //            else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = ShipTable[0, 1]; }
        //            else { shippingPrice = ShipTable[0, 2]; }
        //            break;
        //        case RushShippingChoice.Rush5Days:
        //            if (deskSize < 1000) { shippingPrice = ShipTable[1, 0]; }
        //            else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = ShipTable[1, 1]; }
        //            else { shippingPrice = ShipTable[1, 2]; }
        //            break;
        //        case RushShippingChoice.Rush7Days:
        //            if (deskSize < 1000) { shippingPrice = ShipTable[2, 0]; }
        //            else if (deskSize >= 1000 && deskSize < 2001) { shippingPrice = ShipTable[2, 1]; }
        //            else { shippingPrice = ShipTable[2, 2]; }
        //            break;
        //        case RushShippingChoice.Standard14Days:
        //            shippingPrice = ShippingPriceStandard;
        //            break;
        //    }

        //    return shippingPrice;
        //}


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
