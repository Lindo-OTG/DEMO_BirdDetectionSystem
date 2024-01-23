using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BirdDetectionSystem
{
    class DataHandler
    {
        static SqlConnection connect = new SqlConnection(@"Data Source= DESKTOP-V35D6DH\SQLEXPRESS; Initial Catalog= BirdDetectionDB; Integrated Security = SSPI");
        string query;
        List<Users> Users = new List<Users>();
        public List<Users> GetUsers()
        {
            try
            {
                SqlDataReader Reader = null;
                connect.Open();
                query = " Select * from tblUsers";
                SqlCommand command = new SqlCommand(query, connect);
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Users.Add(new Users(Reader[0].ToString(), Reader[1].ToString()));
                }

                connect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Information", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Users;
        }
    }
}
