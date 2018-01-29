using My_Shop_MVC.App_Code;
using My_Shop_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace My_Shop_MVC.Controllers
{
    public class UsersController : Controller
    {
        public List<SelectListItem> GetUserTypes()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"SELECT TypeID, UserType FROM TypesTable ORDER BY UserType";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new SelectListItem
                            {
                                Value = data["TypeID"].ToString(),
                                Text = data["UserType"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
        public ActionResult Add()
        {
            UsersModel record = new UsersModel();
            record.UserTypes = GetUserTypes();
            return View(record);
        }

        [HttpPost]
        public ActionResult Add(UsersModel record)
        {
            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"INSERT INTO UsersTable VALUES(@TypeID, @Email, @Password, @LastName, @FirstName, @Street, @Municipality, @City, @Phone, @Mobile, @Status, @DateAdded, @DateModified)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TypeID", record.TypeID);
                    cmd.Parameters.AddWithValue("@Email", record.Email);
                    cmd.Parameters.AddWithValue("@Password", record.Password);
                    cmd.Parameters.AddWithValue("@LastName", record.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", record.FirstName);
                    cmd.Parameters.AddWithValue("@Street", record.Street);
                    cmd.Parameters.AddWithValue("@Municipality", record.Municipality);
                    cmd.Parameters.AddWithValue("@City", record.City);
                    cmd.Parameters.AddWithValue("@Phone", record.Phone);
                    cmd.Parameters.AddWithValue("@Mobile", record.MobilePhone);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }


        // GET: Users
        public ActionResult Index()
        {
            var list = new List<UsersModel>();
            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"SELECT u.UserID, t.UserType, u.Email, u.LastName, u.FirstName, u.Street, u.Municipality, u.City, u.Phone, u.Mobile, u.Status 
                                 FROM UsersTable u INNER JOIN TypesTable t ON u.TypeID = t.TypeID WHERE u.Status!=@Status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");

                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new UsersModel
                            {
                                UsersID = int.Parse(data["UserID"].ToString()),
                                UserType = data["UserType"].ToString(),
                                Email = data["Email"].ToString(),
                                LastName = data["LastName"].ToString(),
                                FirstName = data["FirstName"].ToString(),
                                Street = data["Street"].ToString(),
                                Municipality = data["Municipality"].ToString(),
                                City = data["Street"].ToString(),
                                Phone = data["Phone"].ToString(),
                                MobilePhone = data["Mobile"].ToString(),
                                Status = data["Status"].ToString(),
                            });
                        }
                        return View(list);
                    }
                }
            }
        }
    

        public ActionResult Edit(int? id)
        {
            if (id == null) //if id not selected, page goes back to index page
            {
                return RedirectToAction("Index");
            }
            UsersModel record = new UsersModel();
            record.UserTypes = GetUserTypes();
            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"SELECT TypeId, Email, LastName, FirstName, Street, Municipality, City, Phone, Mobile FROM UsersTable WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", id);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows) // record is existing
                        {
                            while (data.Read())
                            {
                                record.TypeID = int.Parse(data["TypeID"].ToString());
                                record.Email = data["Email"].ToString();
                                record.LastName = data["LastName"].ToString();
                                record.FirstName = data["FirstName"].ToString();
                                record.Street = data["Street"].ToString();
                                record.Municipality = data["Municipality"].ToString();
                                record.City = data["City"].ToString();
                                record.Phone = data["Phone"].ToString();
                                record.MobilePhone = data["Mobile"].ToString();
                            }
                            return View(record);
                        }
                        else
                        {
                            return RedirectToAction("Idex");
                        }
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(int? id, UsersModel record)
        {
            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE UsersTable SET TypeID=@TypeID, Email=@Email, Password=@Password, LastName=@LastName, FirstName=@FirstName, Street=@Street, 
                                Municipality=@Municipality, City=@City, Phone=@Phone, Mobile=@Mobile, DateModified=@DateModified WHERE UserID=@UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TypeID", record.TypeID);
                    cmd.Parameters.AddWithValue("@Email", record.Email);
                    cmd.Parameters.AddWithValue("@Password", record.Password);
                    cmd.Parameters.AddWithValue("@LastName", record.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", record.FirstName);
                    cmd.Parameters.AddWithValue("@Street", record.Street);
                    cmd.Parameters.AddWithValue("@Municipality", record.Municipality);
                    cmd.Parameters.AddWithValue("@City", record.City);
                    cmd.Parameters.AddWithValue("@Phone", record.Phone);
                    cmd.Parameters.AddWithValue("@Mobile", record.MobilePhone);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserID", id);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }

            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            using (SqlConnection con = new SqlConnection(JesusChrist.GetConnection()))
            {
                con.Open();
                string query = @"UPDATE UsersTable SET Status=@Status, DateModified=@DateModified WHERE UserID=@UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}