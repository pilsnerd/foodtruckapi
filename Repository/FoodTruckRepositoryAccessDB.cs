using ServiceModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace Repository
{
    public class FoodTruckRepositoryAccessDB : IFoodTruckRepository
    {
        private string _connString;

        public FoodTruckRepositoryAccessDB()
        {
            _connString = ConfigurationManager.ConnectionStrings["FoodTruckAccessDB"].ToString();
        }

        public Truck CreateTruck(string name, string imageUrl)
        {
            var truck = new Truck();

            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "CreateFoodTruck";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("@TruckName", name));
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@ImageUrl", imageUrl));
                    }
                    var newId = cmd.ExecuteScalar();
                    truck.TruckId = int.Parse(newId.ToString());
                    truck.Name = name;
                    truck.ImageUrl = imageUrl;
                }
            }
            return truck;
        }

        public List<Truck> ReadFoodTrucksWithRatings()
        {
            var trucks = new List<Truck>();

            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "ReadFoodTrucksWithRatings";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var truck = new Truck();
                        truck.TruckId = dr.GetInt32(dr.GetOrdinal("TruckId"));
                        truck.Name = dr.GetString(dr.GetOrdinal("TruckName"));
                        if (dr.GetValue(dr.GetOrdinal("ImageUrl")) != DBNull.Value)
                        {
                            truck.ImageUrl = dr.GetString(dr.GetOrdinal("ImageUrl"));
                        }
                        if (dr.GetValue(dr.GetOrdinal("AvgRating")) != DBNull.Value)
                        {
                            truck.AverageRating = decimal.Parse(dr.GetValue(dr.GetOrdinal("AvgRating")).ToString());
                        }
                        trucks.Add(truck);
                    }
                }
            }
            return trucks;
        }

        public Truck UpdateTruck(int truckId, string name, string imageUrl)
        {
            var truck = new Truck();

            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "UpdateFoodTruck";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("@TruckId", truckId));
                    cmd.Parameters.Add(new OleDbParameter("@TruckName", name));
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@ImageUrl", imageUrl));
                    }
                    cmd.ExecuteNonQuery();
                    truck.TruckId = truckId;
                    truck.Name = name;
                    truck.ImageUrl = imageUrl;
                }
            }
            return truck;
        }

        public void DeleteTruck(int truckId)
        {
            throw new NotImplementedException();
        }


        public FoodItem CreateFoodItem(int truckId, DateTime date, string foodName, string personName, decimal price, int rating, string comments)
        {
            var item = new FoodItem();

            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "CreateFoodItem";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("@TruckId", truckId));
                    cmd.Parameters.Add(new OleDbParameter("@ConsumptionDate", date));
                    cmd.Parameters.Add(new OleDbParameter("@FoodName", foodName));
                    cmd.Parameters.Add(new OleDbParameter("@PersonName", personName));
                    if (price > 0)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@Price", price));
                    }
                    cmd.Parameters.Add(new OleDbParameter("@Rating", rating));
                    if (!string.IsNullOrWhiteSpace(comments))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@Comments", comments));
                    }
                    var newId = cmd.ExecuteScalar();
                    item.FoodItemId = int.Parse(newId.ToString());
                    item.TruckId = truckId;
                    item.Date = date;
                    item.FoodName = foodName;
                    item.PersonName = personName;
                    item.Price = price;
                    item.Rating = rating;
                    item.Comments = comments;
                }
            }
            return item;
        }

        public List<FoodItem> ReadFoodItemsByTruck(int truckId)
        {
            var items = new List<FoodItem>();

            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "ReadFoodItemsByTruck";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("@TruckId", truckId));
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var item = new FoodItem();
                        item.FoodItemId = dr.GetInt32(dr.GetOrdinal("FoodItemId"));
                        item.TruckId = dr.GetInt32(dr.GetOrdinal("TruckId"));
                        item.Date = dr.GetDateTime(dr.GetOrdinal("ConsumptionDate"));
                        item.FoodName = dr.GetString(dr.GetOrdinal("FoodName"));
                        item.PersonName = dr.GetString(dr.GetOrdinal("PersonName"));
                        if (dr.GetValue(dr.GetOrdinal("Price")) != DBNull.Value)
                        {
                            item.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                        }
                        item.Rating = dr.GetInt32(dr.GetOrdinal("Rating"));
                        if (dr.GetValue(dr.GetOrdinal("Comments")) != DBNull.Value)
                        {
                            item.Comments = dr.GetString(dr.GetOrdinal("Comments"));
                        }
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public FoodItem UpdateFoodItem(int foodItemId, int truckId, DateTime date, string foodName, string personName, decimal price, int rating, string comments)
        {
            throw new NotImplementedException();
        }

        public void DeleteFoodItem(int foodItemId)
        {
            using (OleDbConnection conn = new OleDbConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "DeleteFoodItem";
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OleDbParameter("@FoodItemId", foodItemId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
