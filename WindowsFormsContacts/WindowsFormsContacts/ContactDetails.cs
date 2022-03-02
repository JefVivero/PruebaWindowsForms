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
    public partial class ContactDetails : Form
    {
        private Contact _contact;
        private BusinessLogicLayer _businessLogicLayer;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Contacts)this.Owner).PopulateContacts();
        }

        private void SaveContact()
        {
            Contact contact = new Contact()
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Address = TxtAddress.Text,
                Email = TxtEmail.Text,
                Phone = TxtPhone.Text,
                Id = _contact != null ? _contact.Id : 0
            };

            _businessLogicLayer.SaveContact(contact);
        }

        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();
                TxtFirstName.Text = contact.FirstName;
                TxtLastName.Text = contact.LastName;
                TxtEmail.Text = contact.Email;
                TxtPhone.Text = contact.Phone;
                TxtAddress.Text = contact.Address;
            }
        }

        private void ClearForm()
        {
            TxtFirstName.Text = string.Empty;
            TxtLastName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtAddress.Text = string.Empty;
        }
    }
}
