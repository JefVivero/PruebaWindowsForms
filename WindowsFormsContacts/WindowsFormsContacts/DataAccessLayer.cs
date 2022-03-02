using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsContacts
{
    public class DataAccessLayer
    {
        
        private SqlConnection _conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WindowsFormsContacts;Data Source=ASUS");

        public void InsertContact(Contact contact)
        {
            try
            {
                _conn.Open();
                string query = @"Insert Into contacts (FirstName, LastName, Email, Phone, Address) 
                                values(@FirstName, @LastName, @Email, @Phone, @Address)";

                /*SqlParameter firstname = new SqlParameter();  //Metodo largo
                firstname.ParameterName = "@FirstName";
                firstname.Value = contact.FirstName;
                firstname.DbType = System.Data.DbType.String;*/

                SqlParameter firstname = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastname = new SqlParameter("@LastName", contact.LastName);
                SqlParameter email = new SqlParameter("@Email", contact.Email);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query,_conn);

                command.Parameters.Add(firstname);
                command.Parameters.Add(lastname);
                command.Parameters.Add(email);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            List<Contact> contact = new List<Contact>();
            try
            {
                _conn.Open();
                string query = @"select Id, Firstname, Lastname, Email, Phone, Address from Contacts";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += $" WHERE Firstname like @searchText or" +
                             $" Lastname like @searchText or" +
                             $" Email like @searchText or" +
                             $" Phone like @searchText or" +
                             $" Address like @searchText";
                    command.Parameters.Add(new SqlParameter("@searchText", $"%{searchText}%"));
                }
                command.CommandText = query;
                command.Connection = _conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contact.Add(new Contact()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    });
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _conn.Close();
            }
            return contact;
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                _conn.Open();
                string query = @"UPDATE CONTACTS 
                                SET FirstName=@FirstName, 
                                    LastName=@LastName,
                                    Email=@Email,
                                    Phone=@Phone, 
                                    Address=@Address
                                WHERE ID=@ID";

                SqlParameter id = new SqlParameter("@ID", contact.Id);
                SqlParameter firstname = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastname = new SqlParameter("@LastName", contact.LastName);
                SqlParameter email = new SqlParameter("@Email", contact.Email);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstname);
                command.Parameters.Add(lastname);
                command.Parameters.Add(email);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteContact(int id)
        {
            try
            {
                _conn.Open();
                string query = $"DELETE FROM CONTACTS WHERE ID=@ID";                

                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.Add(new SqlParameter("@ID", id));

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
