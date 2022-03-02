using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsContacts
{
    public partial class Contacts : Form
    {
        private BusinessLogicLayer _bussinessLogicLayer;
        public Contacts()
        {            
            InitializeComponent();
            _bussinessLogicLayer = new BusinessLogicLayer();
        }

        #region Envets
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OpenDetails();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(TxtSearch.Text);
            TxtSearch.Text = String.Empty;
        }

        private void GridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)GridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact()
                {
                    Id = int.Parse(GridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = GridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = GridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Email = GridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Phone = GridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Address = GridContacts.Rows[e.RowIndex].Cells[5].Value.ToString()
                });
                contactDetails.ShowDialog(this.Owner);

            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(GridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }
        #endregion

        #region Private Methods
        private void OpenDetails()
        {
            ContactDetails contact = new ContactDetails();
            contact.ShowDialog(this);
        }

        private void DeleteContact(int id)
        {
            _bussinessLogicLayer.DeleteContact(id);
        }

        #endregion

        #region Public Methods

        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _bussinessLogicLayer.GetContacts(searchText);
            GridContacts.DataSource = contacts;
        }
        #endregion
    }
}
