using Repository;
using ServiceModels.Messages;
using ServiceModels.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDefinitions
{
    public class TruckService : Service
    {
        private IFoodTruckRepository _repo;

        public TruckService()
        {
            _repo = new FoodTruckRepository();
            //_repo = new FoodTruckRepositoryAccessDB();
        }

        public object Get(GetTrucks request)
        {
            return _repo.ReadFoodTrucksWithRatings();
        }

        public object Post(PostTruck request)
        {
            return _repo.CreateTruck(request.Name, request.ImageUrl);
        }

        public object Post(UpdateTruck request)
        {
            return _repo.UpdateTruck(request.TruckId, request.Name, request.ImageUrl);
        }

        public void Delete(DeleteTruck request)
        {
            _repo.DeleteTruck(request.TruckId);
        }

        public object Get(GetFoodItems request)
        {
            return _repo.ReadFoodItemsByTruck(request.TruckId);
        }

        public object Post(PostFoodItem request)
        {
            return _repo.CreateFoodItem(request.TruckId, request.Date, request.FoodName, request.PersonName, request.Price, request.Rating, request.Comments);
        }

        public object Post(UpdateFoodItem request)
        {
            return _repo.UpdateFoodItem(request.FoodItemId, request.TruckId, request.Date, request.FoodName, request.PersonName, request.Price, request.Rating, request.Comments);
        }

        public void Delete(DeleteFoodItem request)
        {
            _repo.DeleteFoodItem(request.FoodItemId);
        }

        
    }
}
