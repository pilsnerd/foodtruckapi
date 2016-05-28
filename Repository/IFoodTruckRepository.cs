using ServiceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IFoodTruckRepository
    {
        Truck CreateTruck(string name, string imageUrl);
        List<Truck> ReadFoodTrucksWithRatings();
        Truck UpdateTruck(int truckId, string name, string imageUrl);
        void DeleteTruck(int truckId);
        FoodItem CreateFoodItem(int truckId, DateTime date, string foodName, string personName, decimal price, int rating, string comments);
        List<FoodItem> ReadFoodItemsByTruck(int truckId);
        FoodItem UpdateFoodItem(int foodItemId, int truckId, DateTime date, string foodName, string personName, decimal price, int rating, string comments);
        void DeleteFoodItem(int foodItemId);

    }
}
