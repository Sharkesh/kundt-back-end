using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kundt_back_end.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace kundt_back_end.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        public static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.Substring(System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.IndexOf("\"") + 1, 156);

        /// <summary>
        /// Connection
        /// </summary>
        public static SqlConnection con = new SqlConnection(connString);

        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [RequireHttps]
        public ActionResult Index()
        {
            if (User.IsInRole("A") || User.IsInRole("M"))
                return RedirectToAction("Index", "Home", null);
            return View();
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [RequireHttps]
        public ActionResult Index(UserLogin user)
        {
            user.Password = Logic.Helper.PasswordConverter(user.Password);
            using (SqlCommand cmd = new SqlCommand("Validate_User"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Passwort", user.Password);
                cmd.Connection = con;
                con.Open();
                //Aufruf der Prozedur
                //Rückabe Werte: -3: Acc Deaktiviert; -2: Acc nicht Aktivert; -1: Eingegebene Daten falsch; >0: Die zugehörige UserId
                ViewBag.loginResult = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (ViewBag.loginResult > 0)
            {
                using (SqlCommand cmd = new SqlCommand("Get_Role"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Connection = con;
                    con.Open();
                    user.Role = Convert.ToChar(cmd.ExecuteScalar());
                    con.Close();
                }
                //Session Variablen werden je nach Rolle gesetzt.
                if (user.Role == 'M' || user.Role == 'A')
                {
                    var authTicket = new FormsAuthenticationTicket(
                    1,                              //Ticketversion
                    user.Email,                     //Useridentifizierung
                    DateTime.Now,                   //Zeitpunkt der Erstellung
                    DateTime.Now.AddMinutes(20),    //Gültigkeitsdauer
                    true,                           //Persistentes Ticket ueber Sessions hinweg
                    user.Role.ToString()            //Userrolle(n)
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    ViewBag.loginResult = -1;
                }
            }
            return View();
        }

        /// <summary>
        /// GET: Login/Logout
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "M,A")]
        [RequireHttps]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}

namespace kundt_back_end.Logic
{
    public class Helper
    {
        /// <summary>
        /// Wandelt den Mitgelieferten String in einen SHA256 Code als String um.
        /// </summary>
        /// <param name="originalPassword">Ein String der in SHA256 konvertiert werden soll.</param>
        /// <returns>Die Zeichenfolge in SHA256 Format als String</returns>
        public static string PasswordConverter(string originalPassword)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            Byte[] encodedBytes = sha256.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}