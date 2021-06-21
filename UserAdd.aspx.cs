using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud_application
{
    public partial class UserAdd : System.Web.UI.Page
    {
        DataTable dt;
        bool status = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvDisplay.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Submit")
                {
                    InsertData();
                }
                else if (btnSave.Text == "Update")
                {
                    int Id = int.Parse(gvDisplay.Rows[gvDisplay.SelectedIndex].Cells[0].Text);
                    UpdateData(Id);
                }               
                gvDisplay.Visible = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateData(int Id)
        {
            Crud_application.ApplicationLayer ObjectAL = new Crud_application.ApplicationLayer();
            if (rbActive.Checked == true)
            {
                status = true;
            }

            ObjectAL.Name = txtName.Text;
            ObjectAL.PhoneNo = txtPhoneNo.Text;
            ObjectAL.JobRole = txtJobRole.Text;
            ObjectAL.CurrentStatus = status;
            ObjectAL.Location = txtLocation.Text;
            Crud_application.Business_Logic_Layer ObjectBAL = new Crud_application.Business_Logic_Layer();
            int result = ObjectBAL.Update(ObjectAL, Id);
            if (result > 0)
            {
                Response.Write("<script>alert(Data Updated Successfully')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Updated Successfully')", true);
            }
            btnSave.Text = "Submit";
            BindGrid();
            Clear();
        }
        public void InsertData()
        {
            Crud_application.ApplicationLayer ObjectAL = new Crud_application.ApplicationLayer();
            
            if(rbActive.Checked == true)
            {
                status = true;
            }

            ObjectAL.Name = txtName.Text;           
            ObjectAL.PhoneNo = txtPhoneNo.Text;
            ObjectAL.JobRole = txtJobRole.Text;
            ObjectAL.CurrentStatus = status;
            ObjectAL.Location = txtLocation.Text;
            Crud_application.Business_Logic_Layer ObjectBAL = new Crud_application.Business_Logic_Layer();
            int result = ObjectBAL.Insert(ObjectAL);
            if (result > 0)
            {
                Response.Write("<script>alert(Data Inserted Successfully')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully')", true);
            }
            BindGrid();
            Clear();
        }
        private void Clear()
        {
            txtName.Text = "";
            txtPhoneNo.Text = "";
            txtJobRole.Text = "";
            txtLocation.Text = "";
        }

        private void BindGrid()
        {
            try
            {
                Crud_application.Business_Logic_Layer ObjectBAL = new Crud_application.Business_Logic_Layer();
                Crud_application.ApplicationLayer ALObject = new Crud_application.ApplicationLayer();
                gvDisplay.Columns[0].Visible = true;
                gvDisplay.DataSource = ObjectBAL.BindGrid(ALObject);
                gvDisplay.DataBind();
                gvDisplay.Columns[0].Visible = false;
                gvDisplay.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDisplay_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
     
                Crud_application.Business_Logic_Layer ObjectBAL = new Crud_application.Business_Logic_Layer();                   
                int Id = int.Parse(gvDisplay.Rows[e.NewSelectedIndex].Cells[0].Text);
                dt = new DataTable();
                dt = ObjectBAL.GetById(Id);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                    txtJobRole.Text = dt.Rows[0]["JobRole"].ToString();
                    status = Convert.ToBoolean(dt.Rows[0]["CurrentStatus"].ToString());                    
                    txtLocation.Text = dt.Rows[0]["Location"].ToString();

                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                int Id = int.Parse(e.CommandArgument.ToString());
                DeleteRecord(Id);
            }
        }

        private void DeleteRecord(int Id)
        {
            try
            {
                Crud_application.Business_Logic_Layer ObjectBAL = new Crud_application.Business_Logic_Layer();
                int Result = ObjectBAL.Delete(Id);
                if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Deleted Successfully')", true);
                }
                BindGrid();
                Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDisplay_RowDeleting(object sender, GridViewDeleteEventArgs e) { }
        protected void gvDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDisplay.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}