using ServiceModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Repository
{
    public class FoodTruckRepository : IFoodTruckRepository
    {
        private string _connString;

        public FoodTruckRepository()
        {
            _connString = ConfigurationManager.ConnectionStrings["WebDB"].ToString();
        }

        public Truck CreateTruck(string name, string imageUrl)
        {
            var truck = new Truck();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "CreateFoodTruck";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", name));
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
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

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "ReadFoodTrucksWithRatings";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var truck = new Truck();
                        truck.TruckId = dr.GetInt32(dr.GetOrdinal("TruckId"));
                        truck.Name = dr.GetString(dr.GetOrdinal("Name"));
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

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "UpdateFoodTruck";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TruckId", truckId));
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Name", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Name", name));
                    }
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ImageUrl", imageUrl));
                    }
                    cmd.Parameters.Add(new SqlParameter("@IsDeleted", false));
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
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "DeleteTruck";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TruckId", truckId));
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public FoodItem CreateFoodItem(int truckId, DateTime date, string foodName, string personName, decimal price, int rating, string comments)
        {
            var item = new FoodItem();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "CreateFoodItem";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TruckId", truckId));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    cmd.Parameters.Add(new SqlParameter("@FoodName", foodName));
                    cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
                    if (price > 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Price", price));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Rating", rating));
                    if (!string.IsNullOrWhiteSpace(comments))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Comments", comments));
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

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "ReadFoodItemsByTruck";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TruckId", truckId));
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var item = new FoodItem();
                        item.FoodItemId = dr.GetInt32(dr.GetOrdinal("FoodItemId"));
                        item.TruckId = dr.GetInt32(dr.GetOrdinal("TruckId"));
                        item.Date = dr.GetDateTime(dr.GetOrdinal("Date"));
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
            var item = new FoodItem();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "UpdateFoodItem";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FoodItemId", foodItemId));
                    cmd.Parameters.Add(new SqlParameter("@TruckId", truckId));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    cmd.Parameters.Add(new SqlParameter("@FoodName", foodName));
                    cmd.Parameters.Add(new SqlParameter("@PersonName", personName));
                    if (price > 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Price", price));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Rating", rating));
                    if (!string.IsNullOrWhiteSpace(comments))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Comments", comments));
                    }
                    cmd.Parameters.Add(new SqlParameter("@IsDeleted", false));
                    var newId = cmd.ExecuteScalar();
                    item.FoodItemId = foodItemId;
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

        public void DeleteFoodItem(int foodItemId)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var sqlQuery = "DeleteFoodItem";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FoodItemId", foodItemId));
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
